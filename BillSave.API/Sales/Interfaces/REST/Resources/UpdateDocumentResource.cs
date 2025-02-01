namespace BillSave.API.Sales.Interfaces.REST.Resources;

/// Update Document Resource
/// <summary>
/// This class represents the Update Document Resource. It is used to encapsulate the data access logic of the Document entity.
/// </summary>
/// <param name="Id">
/// The document code.
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
/// <param name="Currency">
/// The document currency.
/// </param>
public record UpdateDocumentResource(
    int Id,
    string Code,
    DateTime IssueDate,
    DateTime DueDate,
    string RateType,
    decimal RateValue,
    string Currency
    );