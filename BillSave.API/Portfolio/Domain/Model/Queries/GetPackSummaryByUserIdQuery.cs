namespace BillSave.API.Portfolio.Domain.Model.Queries;

/// Get Pack Summary By User Id Query
/// <summary>
/// This query is used to get the pack summary by the user id.
/// </summary>
/// <param name="UserId">
/// The user id.
/// </param>
public record GetPackSummaryByUserIdQuery(int UserId);