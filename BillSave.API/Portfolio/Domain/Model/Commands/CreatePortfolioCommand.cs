namespace BillSave.API.Portfolio.Domain.Model.Commands;

/// Create Portfolio Command.
/// <summary>
/// This class represents the command to create a new Portfolio.
/// </summary>
/// <param name="Name">
/// The name of the Portfolio.
/// </param>
/// <param name="DiscountDate">
/// The discount date of the Portfolio.
/// </param>
/// <param name="TotalDocuments">
/// The total number of documents in the Portfolio.
/// </param>
public record CreatePortfolioCommand(string Name, DateTime DiscountDate, int TotalDocuments);