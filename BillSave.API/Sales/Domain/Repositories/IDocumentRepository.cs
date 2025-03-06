using System.Collections;
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
    
    /// <summary>
    /// Find By Portfolio Id And Date Range. 
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <param name="startDate">
    /// The start date.
    /// </param>
    /// <param name="endDate">
    /// The end date.
    /// </param>
    /// <returns>
    /// The document found.
    /// </returns>
    Task<IEnumerable<Document>> FindByPortfolioIdAndDateRangeAsync(int portfolioId, DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Find Document by Date Range.
    /// </summary>
    /// <param name="startDate">
    /// The start date.
    /// </param>
    /// <param name="endDate">
    /// The end date.
    /// </param>
    /// <returns>
    /// The Documents found.
    /// </returns>
    Task<IEnumerable<Document>> FindByDateRangeAsync(DateTime startDate, DateTime endDate);
    
    /// <summary>
    /// Find By Rate Type.
    /// </summary>
    /// <param name="rateType">
    /// The rate type.
    /// </param>
    /// <returns>
    /// The Documents found.
    /// </returns>
    Task<IEnumerable<Document>> FinByRateTypeAsync(string rateType);
    
    /// Exists By Code And Portfolio Id
    /// <summary>
    /// This method is used to check if the document exists by the code and portfolio id.
    /// </summary>
    /// <param name="code">
    /// The document code.
    /// </param>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <returns>
    /// True if the document exists, false otherwise.
    /// </returns>
    Task<bool> ExistsByCodeAndPortfolioIdAsync(string code, int portfolioId);
    
    /// Delete By Portfolio Id
    /// <summary>
    /// This method is used to delete the documents by the portfolio id.
    /// </summary>
    /// <param name="portfolioId">
    /// The portfolio id.
    /// </param>
    /// <returns>
    /// The task.
    /// </returns>
    Task DeleteDocumentsByPortfolioIdAsync(int portfolioId);
}