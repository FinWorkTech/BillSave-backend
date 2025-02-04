namespace BillSave.API.Portfolio.Interfaces.ACL;

/// Portfolios context facade interface
/// <summary>
/// This interface is used to define the methods that will be used to interact with the portfolios context. 
/// </summary>
public interface IPortfoliosContextFacade
{
    /// Increment total documents
    /// <summary>
    /// This method is used to increment the total documents of a portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task IncrementTotalDocumentsAsync(int portfolioId);
    
    /// Decrement total documents
    /// <summary>
    /// This method is used to decrement the total documents of a portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task DecrementTotalDocumentsAsync(int portfolioId);
    
    /// Update effective annual cost rate
    /// <summary>
    /// This method is used to update the effective annual cost rate of a portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <param name="effectiveAnnualCostRate">
    /// The effective annual cost rate.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task UpdateEffectiveAnnualCostRateAsync(int portfolioId, decimal effectiveAnnualCostRate);
}