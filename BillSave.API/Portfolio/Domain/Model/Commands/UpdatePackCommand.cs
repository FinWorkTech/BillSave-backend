namespace BillSave.API.Portfolio.Domain.Model.Commands;

/// Update Pack Command.
/// <summary>
/// This class represents the command to update a Pack.
/// </summary>
/// <param name="Id">
/// The identifier of the Pack to update.
/// </param>
/// <param name="Name">
/// The name of the Pack.
/// </param>
/// <param name="DiscountDate">
/// The discount date of the Pack.
/// </param>
public record UpdatePackCommand(int Id, string Name, DateTime DiscountDate);
