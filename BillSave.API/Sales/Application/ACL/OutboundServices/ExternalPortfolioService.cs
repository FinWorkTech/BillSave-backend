using BillSave.API.Portfolio.Interfaces.ACL;

namespace BillSave.API.Sales.Application.ACL.OutboundServices;

/// <summary>
/// External Portfolio Service
/// </summary>
/// <param name="portfoliosContextFacade">
/// The <see cref="IPortfoliosContextFacade"/> context facade.
/// </param>
public class ExternalPortfolioService(IPortfoliosContextFacade portfoliosContextFacade)
{
    /// Increments the total documents.
    /// <summary>
    /// The <see cref="IncrementTotalDocumentsAsync"/> operation.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio identifier.
    /// </param>
    public async Task IncrementTotalDocumentsAsync(int portfolioId)
    {
        await portfoliosContextFacade.IncrementTotalDocumentsAsync(portfolioId);
    }
    
    public async Task DecrementTotalDocumentsAsync(int portfolioId)
    {
        await portfoliosContextFacade.DecrementTotalDocumentsAsync(portfolioId);
    }

    public async Task UpdateEffectiveAnnualCostRateAsync(int portfolioId, decimal effectiveAnnualCostRate)
    {
        await portfoliosContextFacade.UpdateEffectiveAnnualCostRateAsync(portfolioId, effectiveAnnualCostRate);
    }
}