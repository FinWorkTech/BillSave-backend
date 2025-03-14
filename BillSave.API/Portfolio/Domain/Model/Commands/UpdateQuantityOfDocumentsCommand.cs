namespace BillSave.API.Portfolio.Domain.Model.Commands;

/// Update Quantity of Documents Command.
/// <summary>
/// This command is used to update the quantity of documents of a pack.
/// </summary>
/// <param name="PackId">
/// The pack id.
/// </param>
/// <param name="Operation">
/// The operation to be performed.
/// </param>
public record UpdateQuantityOfDocumentsCommand(int PackId, string Operation);