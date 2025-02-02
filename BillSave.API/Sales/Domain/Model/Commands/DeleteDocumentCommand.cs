namespace BillSave.API.Sales.Domain.Model.Commands;

/// Delete document command
/// <summary>
/// This class represents the delete document command. It is used to delete an existing document.
/// </summary>
/// <param name="Id">
/// The document id.
/// </param>
/// <param name="PortfolioId">
/// The portfolio id.
/// </param>
public record DeleteDocumentCommand(int Id, int PortfolioId);