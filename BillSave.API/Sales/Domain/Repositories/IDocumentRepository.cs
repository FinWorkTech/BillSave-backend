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
    
    /// Exists By Id
    /// <summary>
    /// This method is used to check if a document exists by its id.
    /// </summary>
    /// <param name="id">
    /// The id of the document.
    /// </param>
    /// <returns>
    /// True if the document exists, otherwise false.
    /// </returns>
    Task<bool> ExistsById(int id);
}