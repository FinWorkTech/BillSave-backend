using BillSave.API.Shared.Domain.Model;

namespace BillSave.API.Sales.Interfaces.REST.Resources;

/// Create Document Resource
/// <summary>
/// This class represents the Create Document Resource. It is used to encapsulate the data transfer object of the Document entity.
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
/// <param name="Currency">
/// The document currency.
/// </param>
/// <param name="PortfolioId">
/// The document portfolio id.
/// </param>
public record CreateDocumentResource(
    string Code,
    DateTime IssueDate,
    DateTime DueDate,
    string RateType,
    decimal RateValue,
    string Currency,
    int PortfolioId
    );