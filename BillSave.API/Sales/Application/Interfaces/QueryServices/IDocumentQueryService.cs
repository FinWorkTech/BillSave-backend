using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Model.Queries;

namespace BillSave.API.Sales.Application.Interfaces.QueryServices;

/// Document Query Service Interface
/// <summary>
/// This interface defines the methods that the Document Query Service must implement.
/// </summary>
public interface IDocumentQueryService
{
    /// <summary>
    /// This method is used to get a document by its id.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetDocumentByDateRangeQuery"/> query.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/> object.
    /// </returns>
    Task<IEnumerable<Document>> Handle(GetDocumentByDateRangeQuery query);
    
    /// <summary>
    /// This method is used to get a document by its id.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetDocumentByPortfolioIdQuery"/> query.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/> object.
    /// </returns>
    Task<IEnumerable<Document>> Handle(GetDocumentByPortfolioIdQuery query);
    
    /// <summary>
    /// This method is used to get a document by its id.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetDocumentByIdQuery"/> query.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/> object.
    /// </returns>
    Task<Document?> Handle(GetDocumentByIdQuery query);

    /// <summary>
    /// This method is used to get a document by its id.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetDocumentByPortfolioIdAndDateRangeQuery"/> query.
    /// </param>
    /// <returns>
    /// The <see cref="Document"/> object.
    /// </returns>
    Task<IEnumerable<Document>> Handle(GetDocumentByPortfolioIdAndDateRangeQuery query);
}