namespace BillSave.API.Sales.Domain.Model.Queries;

/// Get document by portfolio id and date range query.
/// <summary>
/// The get document by portfolio id and date range query.
/// </summary>
/// <param name="PortfolioId">
/// The portfolio id.
/// </param>
/// <param name="StartDate">
/// The start date.
/// </param>
/// <param name="EndDate">
/// The end date.
/// </param>
public record GetDocumentByPortfolioIdAndDateRangeQuery(int PortfolioId, DateTime StartDate, DateTime EndDate);