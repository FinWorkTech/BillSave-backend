namespace BillSave.API.Portfolio.Domain.Model.Queries;

/// Get the portfolio by the user id.
/// <summary>
/// This query is used to get the portfolio by the user id.
/// </summary>
/// <param name="UserId">
/// The user id.
/// </param>
public record GetPortfolioByUserId(int UserId);