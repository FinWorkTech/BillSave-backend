using BillSave.API.Sales.Application.ACL.OutboundServices;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Domain.Model.Events;
using BillSave.API.Sales.Domain.Repositories;
using BillSave.API.Sales.Domain.Services;
using BillSave.API.Shared.Domain.Repositories;
using MediatR;

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
        var exists = await documentRepository.ExistsByCode(command.Code);
        
        if (exists)
        {
            throw new Exception("Document with the same code already exists.");
        }

        var document = new Document(command);

        try
        {
            await externalPortfolioService.IncrementTotalDocumentsAsync(command.PortfolioId);
            await documentRepository.AddAsync(document);
            await unitOfWork.CompleteAsync();
            
            await mediator.Publish(new DocumentChangedEvent(command.PortfolioId));
            await unitOfWork.CompleteAsync();
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
        }
        catch (System.Exception e)
        {
            throw new System.Exception("An error occurred while updating the document.", e);
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