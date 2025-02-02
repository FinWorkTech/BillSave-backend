namespace BillSave.API.Sales.Interfaces.REST.Resources;

/// <summary>
/// Delete Document Resource
/// </summary>
/// <param name="Id">
/// The document id.
/// </param>
/// <param name="PortfolioId">
/// The portfolio id.
/// </param>
public record DeleteDocumentResource(int Id, int PortfolioId);