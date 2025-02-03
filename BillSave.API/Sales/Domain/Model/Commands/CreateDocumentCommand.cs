namespace BillSave.API.Sales.Domain.Model.Commands;

/// Create document command
/// <summary>
/// This class represents the create document command. It is used to create a new document.
/// </summary>
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
/// <param name="NominalAmount">
/// The document nominal amount.
/// </param>
public record CreateDocumentCommand(string Code, DateTime IssueDate, 
    DateTime DueDate, string RateType, decimal RateValue, string Currency, decimal NominalAmount, int PortfolioId);