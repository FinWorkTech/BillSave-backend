namespace BillSave.API.Portfolio.Domain.Model.Commands;

/// Delete Pack Command.
/// <summary>
/// This class represents the command to delete a Pack.
/// </summary>
/// <param name="Id">
/// The Id of the Pack to delete.
/// </param>
public record DeletePackCommand(int Id);