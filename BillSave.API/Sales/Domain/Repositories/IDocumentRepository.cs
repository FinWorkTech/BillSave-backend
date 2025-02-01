using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Shared.Domain.Repositories;

namespace BillSave.API.Sales.Domain.Repositories;

/// Document repository interface
/// <summary>
/// This interface is used to define the methods that will be used to interact with the database.
/// </summary>
public interface IDocumentRepository : IBaseRepository<Document>
{
    /// Find By Portfolio Id
    /// <summary>
    /// This method is used to find the documents by the portfolio id.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <returns>
    /// The list of documents.
    /// </returns>
    Task<IEnumerable<Document>> FindByPortfolioIdAsync(int portfolioId);
    
    /// Exists By Code
    /// <summary>
    /// This method is used to check if a document exists by its code.
    /// </summary>
    /// <param name="code">
    /// The document code.
    /// </param>
    /// <returns>
    /// True if the document exists, false otherwise.
    /// </returns>
    Task<bool> ExistsByCode(string code);
}