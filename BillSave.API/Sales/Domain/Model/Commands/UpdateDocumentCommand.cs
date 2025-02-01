namespace BillSave.API.Sales.Domain.Model.Commands;


/// Update document command
/// <summary>
/// This class represents the update document command. It is used to update an existing document.
/// </summary>
/// <param name="Id">
/// The document id.
/// </param>
/// <param name="Code">
/// The document code.
/// </param>
/// <param name="IssueDate">
/// The document issue date.
/// </param>
/// <param name="DueDate">
/// The document due date.
/// </param>
/// <param name="RateType">
/// The document rate type.
/// </param>
/// <param name="RateValue">
/// The document rate value.
/// </param>
/// <param name="PortfolioId">
/// The document portfolio id.
/// </param>
public record UpdateDocumentCommand(int Id, string Code, DateTime IssueDate, 
    DateTime DueDate, string RateType, decimal RateValue, string Currency, int PortfolioId);