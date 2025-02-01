namespace BillSave.API.Portfolio.Domain.Model.Queries;

/// <summary>
/// The query to get a pack by its identifier.
/// </summary>
/// <param name="StartDate">
/// The start date of the date range.
/// </param>
/// <param name="EndDate">
/// The end date of the date range.
/// </param>
public record GetPackByDateRange(DateTime StartDate, DateTime EndDate);