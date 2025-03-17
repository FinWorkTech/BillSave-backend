using MediatR;
using BillSave.API.Shared.Domain.Repositories;
using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Contracts.Repositories;
using BillSave.API.Sales.Application.ACL.OutboundServices;
using BillSave.API.Sales.Application.Contracts;
using BillSave.API.Sales.Domain.Services.Contracts;

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
public class DocumentCommandService(
    IUnitOfWork unitOfWork, 
    IDocumentRepository documentRepository,
    IEacrCalculationService effectiveAnnualCostRateCalculationService,
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
            await documentRepository.AddAsync(document);
            await unitOfWork.CompleteAsync();
            
            await UpdatePortfolioEffectiveAnnualCostRate(document.PortfolioId);
            await externalPortfolioService.IncrementTotalDocumentsAsync(command.PortfolioId);
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
    
        try
        {
            documentRepository.Update(document);
            await unitOfWork.CompleteAsync();
            
            await UpdatePortfolioEffectiveAnnualCostRate(document.PortfolioId);
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
            
            await UpdatePortfolioEffectiveAnnualCostRate(portfolioId);
        }
        catch (Exception e)
        {
            return null;
        }
        
        return document;
    }
    
    /// <summary>
    /// Updates the Effective Annual Cost Rate (EACR) for a portfolio.
    /// </summary>
    private async Task UpdatePortfolioEffectiveAnnualCostRate(int portfolioId)
    {
        var documents = await documentRepository.FindByPortfolioIdAsync(portfolioId);
        var documentList = documents.ToList();

        if (documentList.Count == 0)
        {
            await externalPortfolioService.UpdateEffectiveAnnualCostRateAsync(portfolioId, 0);
            return;
        }

        var effectiveAnnualCostRate = 
            await effectiveAnnualCostRateCalculationService.CalculateEffectiveAnnualCostRate(documentList);

        try
        {
            await externalPortfolioService.UpdateEffectiveAnnualCostRateAsync(portfolioId, effectiveAnnualCostRate);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception("An error occurred while updating the effective annual cost rate.", e);
        }
    }
}