using MediatR;
using BillSave.API.Sales.Domain.Services;
using BillSave.API.Sales.Domain.Model.Events;
using BillSave.API.Sales.Domain.Repositories;
using BillSave.API.Shared.Domain.Repositories;
using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Application.Interfaces.CommandServices;

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
public class DocumentCommandService
    (
        IMediator mediator,
        IUnitOfWork unitOfWork, 
        IDocumentRepository documentRepository,
        IDocumentEacrCalculationService documentEacrCalculationService
    ) 
    : IDocumentCommandService
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
        document.UpdateEffectiveAnnualCostRate
            (await documentEacrCalculationService.CalculateEffectiveAnnualCostRate(document));
        
        try
        {
            await documentRepository.AddAsync(document);
            await unitOfWork.CompleteAsync();
            
            await mediator.Publish(new DocumentCreatedEvent(document.PortfolioId));
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
            throw new Exception("Document not found.");
        }
        
        document.UpdateDocument(command);
        document.UpdateEffectiveAnnualCostRate
            (await documentEacrCalculationService.CalculateEffectiveAnnualCostRate(document));
    
        try
        {
            documentRepository.Update(document);
            await unitOfWork.CompleteAsync();
            
            await mediator.Publish(new DocumentUpdatedEvent(document.PortfolioId));
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
            documentRepository.Remove(document);
            await unitOfWork.CompleteAsync();
            
            await mediator.Publish(new DocumentDeletedEvent(portfolioId));
        }
        catch (Exception e)
        {
            return null;
        }
        
        return document;
    }
}