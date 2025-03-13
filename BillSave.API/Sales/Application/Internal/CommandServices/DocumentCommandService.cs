using MediatR;
using BillSave.API.Sales.Domain.Model.Events;
using BillSave.API.Shared.Domain.Repositories;
using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Contracts.Repositories;
using BillSave.API.Sales.Application.ACL.OutboundServices;
using BillSave.API.Sales.Domain.Services;

namespace BillSave.API.Sales.Application.Internal.CommandServices;

/// Document command service
/// <summary>
/// The <see cref="IDocumentCommandService"/> implementation.
/// </summary>
/// <param name="documentRepository">
/// The <see cref="IDocumentRepository"/> repository.
/// </param>
/// <param name="unitOfWork">
/// The <see cref="IUnitOfWork"/> unit of work.
/// </param>
public class DocumentCommandService(IDocumentRepository documentRepository, IUnitOfWork unitOfWork, 
    ExternalPortfolioService externalPortfolioService, IMediator mediator) : IDocumentCommandService
{
    /// <inheritdoc/>
    public async Task<Document?> Handle(CreateDocumentCommand command)
    {
        if (command.DueDate <= command.IssueDate)
        {
            throw new ArgumentException("Due date must be greater than the issue date.");
        }
        
        var exists = await documentRepository.ExistsByCodeAndPortfolioIdAsync(command.Code, command.PortfolioId);
        
        if (exists)
        {
            throw new Exception("Document already exists.");
        }

        var document = new Document(command);

        try
        {
            await externalPortfolioService.IncrementTotalDocumentsAsync(command.PortfolioId);
            await documentRepository.AddAsync(document);
            await unitOfWork.CompleteAsync();
            
            await mediator.Publish(new DocumentChangedEvent(command.PortfolioId));
            await unitOfWork.CompleteAsync();

            await mediator.Publish(new EffectiveAnnualCostRateCalculatedEvent(document.Id));
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while creating the document.", e);
        }
        
        return document;
    }
    
    /// <inheritdoc/>
   public async Task<Document?> Handle(UpdateDocumentCommand command)
    {
        var document = await documentRepository.FindByIdAsync(command.Id);
        
        if (document == null)
        {
            throw new System.Exception("Document not found.");
        }
        
        document.UpdateDocument(command);
    
        try
        {
            documentRepository.Update(document);
            await unitOfWork.CompleteAsync();
            
            await mediator.Publish(new DocumentChangedEvent(document.PortfolioId));
            await unitOfWork.CompleteAsync();
            
            await mediator.Publish(new EffectiveAnnualCostRateCalculatedEvent(document.Id));
        }
        catch (System.Exception e)
        {
            throw new System.Exception("An error occurred while updating the document.", e);
        }
    
        return document;
    }

    public async Task<Document?> Handle(UpdateEffectiveAnnualCostRateCommand command)
    {
        var document = await documentRepository.FindByIdAsync(command.DocumentId);
        
        if (document == null)
            throw new Exception("Document not found.");
        
        document.UpdateEffectiveAnnualCostRate(command.EffectiveAnnualCostRate);

        try
        {
            documentRepository.Update(document);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while updating the document.", e);
        }

        return document;
    }

    /// <inheritdoc/>
    public async Task<Document?> Handle(DeleteDocumentCommand command)
    {
        var document = await documentRepository.FindByIdAsync(command.Id);

        var portfolioId = document?.PortfolioId ?? 0;
        
        if (document == null)
            throw new Exception("Document not found.");
        
        try
        {
            await externalPortfolioService.DecrementTotalDocumentsAsync(command.PortfolioId);
            documentRepository.Remove(document);
            await unitOfWork.CompleteAsync();
            
            await mediator.Publish(new DocumentChangedEvent(portfolioId));
            await unitOfWork.CompleteAsync();   
        }
        catch (Exception e)
        {
            return null;
        }
        
        return document;
    }
}