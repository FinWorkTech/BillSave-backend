using BillSave.API.Sales.Interfaces.ACL;

namespace BillSave.API.Portfolio.Application.ACL.OutboundServices;

/// <summary>
/// External Sales Service
/// </summary>
/// <param name="salesContextFacade">
/// The <see cref="ISalesContextFacade"/> context facade.
/// </param>
public class ExternalSalesService(ISalesContextFacade salesContextFacade)
{
    /// <summary>
    /// The <see cref="DeleteByPortfolioIdAsync"/> operation.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio identifier.
    /// </param>
    public async Task DeleteByPortfolioIdAsync(int portfolioId)
    {
        await salesContextFacade.DeleteDocumentsByPortfolioIdAsync(portfolioId);
    }
    
    /// <summary>
    /// The <see cref="GetDocumentNominalAmountsByPortfolioIdAsync"/> operation.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio identifier.
    /// </param>
    /// <returns>
    /// List of decimal nominal amounts.
    /// </returns>
    public async Task<List<decimal>> GetDocumentNominalAmountsByPortfolioIdAsync(int portfolioId)
    {
        return await salesContextFacade.GetDocumentNominalAmountsByPortfolioIdAsync(portfolioId);
    }
    
    /// <summary>
    /// The <see cref="GetDocumentEffectiveAnnualCostRatesByPortfolioIdAsync"/> operation.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio identifier.
    /// </param>
    /// <returns>
    /// List of decimal effective annual cost rates.
    /// </returns>
    public async Task<List<decimal>> GetDocumentEffectiveAnnualCostRatesByPortfolioIdAsync(int portfolioId)
    {
        return await salesContextFacade.GetDocumentEffectiveAnnualCostRatesByPortfolioIdAsync(portfolioId);
    }
}