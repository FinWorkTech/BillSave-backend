using System.Reflection.Metadata;
using Document = BillSave.API.Sales.Domain.Model.Aggregates.Document;

namespace BillSave.API.Sales.Interfaces.ACL;

/// Sales context facade interface
/// <summary>
/// The sales context facade interface.
/// </summary>
public interface ISalesContextFacade
{
    /// <summary>
    /// This method is used to increment the total documents of a portfolio.
    /// </summary>
    /// <param name="portfolioId">
    ///     The portfolio id.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task<IEnumerable<Document>> GetDocumentsByPortfolioIdAsync(int portfolioId);
}