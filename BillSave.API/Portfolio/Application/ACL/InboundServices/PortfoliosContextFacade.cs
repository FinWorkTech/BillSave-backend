using BillSave.API.Portfolio.Interfaces.ACL;
using BillSave.API.Portfolio.Domain.Repositories;

namespace BillSave.API.Portfolio.Application.ACL.InboundServices;

/// Portfolios context facade.
/// <summary>
/// The <see cref="IPortfoliosContextFacade"/> context facade.
/// </summary>
/// <param name="packRepository">
/// The <see cref="IPackRepository"/> repository.
/// </param>
public class PortfoliosContextFacade(IPackRepository packRepository) : IPortfoliosContextFacade
{
    /// Increment total documents.
    /// <summary>
    /// Increment the total documents of a Portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The Portfolio id.
    /// </param>
    public async Task IncrementTotalDocumentsAsync(int portfolioId)
    {
        var pack = await packRepository.FindByIdAsync(portfolioId);
        
        if (pack == null)
            throw new Exception("Portfolio not found");

        try
        {
            pack.UpdateTotalDocuments(pack.TotalDocuments + 1);
            packRepository.Update(pack);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating Portfolio", ex);
        }
    }
    
    /// Decrement total documents.
    /// <summary>
    /// Decrement the total documents of a Portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The Portfolio id.
    /// </param>
    public async Task DecrementTotalDocumentsAsync(int portfolioId)
    {
        var pack = await packRepository.FindByIdAsync(portfolioId);
        
        if (pack == null)
            throw new Exception("Portfolio not found");

        try
        {
            pack.UpdateTotalDocuments(pack.TotalDocuments - 1);
            packRepository.Update(pack);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating Portfolio", ex);
        }
    }

    /// Update effective annual cost rate.
    /// <summary>
    /// Update the effective annual cost rate of a Portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The Portfolio id.
    /// </param>
    /// <param name="effectiveAnnualCostRate">
    /// The effective annual cost rate.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    public async Task UpdateEffectiveAnnualCostRateAsync(int portfolioId, decimal effectiveAnnualCostRate)
    {
        var pack = await packRepository.FindByIdAsync(portfolioId);
        
        if (pack == null)
            throw new Exception("Portfolio not found");

        try
        {
            pack.UpdateEffectiveAnnualCostRate(effectiveAnnualCostRate);
            packRepository.Update(pack);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating Portfolio", ex);
        }
    }
}