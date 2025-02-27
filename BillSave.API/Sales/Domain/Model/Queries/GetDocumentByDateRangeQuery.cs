namespace BillSave.API.Sales.Domain.Model.Queries;

/// Get Document by date range query.
/// <summary>
/// This class represents the query to get documents by date range.
/// </summary>
/// <param name="StartDate">
/// The start date of the range.
/// </param>
/// <param name="EndDate">
/// The end date of the range.
/// </param>
public record GetDocumentByDateRangeQuery(DateTime StartDate, DateTime EndDate);