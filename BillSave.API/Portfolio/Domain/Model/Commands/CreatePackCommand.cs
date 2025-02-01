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
/// <param name="UserId">
/// The user id of the Portfolio.
/// </param>
public record CreatePackCommand(string Name, DateTime DiscountDate, int UserId);