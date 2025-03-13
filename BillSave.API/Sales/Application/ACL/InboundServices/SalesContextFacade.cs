using BillSave.API.Sales.Interfaces.ACL;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Contracts.Repositories;

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
    /// <inheritdoc />
    public async Task<IEnumerable<Document>> GetDocumentsByPortfolioIdAsync(int portfolioId)
    {
        return await documentRepository.FindByPortfolioIdAsync(portfolioId);
    }
    
    public async Task DeleteDocumentsByPortfolioIdAsync(int portfolioId)
    {
        await documentRepository.DeleteDocumentsByPortfolioIdAsync(portfolioId);
    }

    public Task<decimal> GetEffectiveAnnualCostRateForPack(int portfolioId)
    {
        throw new NotImplementedException();
    }
}