namespace BillSave.API.Sales.Domain.Model.Queries;

/// Get document by portfolio id query
/// <summary>
/// This class represents the get document by portfolio id query. It is used to get a document by portfolio id.
/// </summary>
/// <param name="PortfolioId">
/// The document portfolio id.
/// </param>
public record GetDocumentByPortfolioIdQuery(int PortfolioId);