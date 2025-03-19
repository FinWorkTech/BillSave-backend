
namespace BillSave.API.Sales.Interfaces.ACL;

/// Sales context facade interface
/// <summary>
/// The sales context facade interface.
/// </summary>
public interface ISalesContextFacade
{
    /// <summary>
    /// This method is used to delete all documents of a portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task DeleteDocumentsByPortfolioIdAsync(int portfolioId);
    
    /// <summary>
    /// This method is used to get the document nominal amounts by portfolio id.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <returns>
    /// The document nominal amounts.
    /// </returns>
    Task<List<decimal>> GetDocumentNominalAmountsByPortfolioIdAsync(int portfolioId);
    
    /// <summary>
    /// This method is used to get the document effective annual cost rates by portfolio id.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <returns>
    /// The document effective annual cost rates.
    /// </returns>
    Task<List<decimal>> GetDocumentEffectiveAnnualCostRatesByPortfolioIdAsync(int portfolioId);
}