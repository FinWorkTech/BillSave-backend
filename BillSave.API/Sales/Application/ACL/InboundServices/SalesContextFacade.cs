using BillSave.API.Sales.Interfaces.ACL;
using BillSave.API.Sales.Domain.Repositories;

namespace BillSave.API.Sales.Application.ACL.InboundServices;

/// Sales context facade.
/// <summary>
/// The <see cref="ISalesContextFacade"/> context facade.
/// </summary>
/// <param name="documentRepository">
/// The <see cref="IDocumentRepository"/> repository.
/// </param>
public class SalesContextFacade(IDocumentRepository documentRepository) : ISalesContextFacade
{
    public async Task DeleteDocumentsByPortfolioIdAsync(int portfolioId)
    {
        await documentRepository.DeleteDocumentsByPortfolioIdAsync(portfolioId);
    }
    
    /// <inheridoc/>
    public async Task<List<decimal>> GetDocumentNominalAmountsByPortfolioIdAsync(int portfolioId)
    {
        var documents = await documentRepository.FindByPortfolioIdAsync(portfolioId);
        
        return documents.Select(d => d.NominalAmount).ToList();
    }
    
    /// <inheridoc/>
    public async Task<List<decimal>> GetDocumentEffectiveAnnualCostRatesByPortfolioIdAsync(int portfolioId)
    {
        var documents = await documentRepository.FindByPortfolioIdAsync(portfolioId);
        
        return documents.Select(d => d.EffectiveAnnualCostRate).ToList();
    }
}