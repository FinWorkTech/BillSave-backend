namespace BillSave.API.Sales.Interfaces.REST.Resources;

/// Document Resource
/// <summary>
/// This class represents the Document Resource. It is used to encapsulate the data transfer object of the Document entity.
/// </summary>
/// <param name="Id">
/// The document id.
/// </param>
/// <param name="PortfolioId">
/// The document portfolio id.
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
public record DocumentResource(
    int Id,
    int PortfolioId,
    string Code,
    string IssueDate,
    string DueDate,
    string RateType,
    decimal RateValue,
    string Currency,
    decimal NominalAmount,
    decimal EffectiveAnnualCostRate
    );