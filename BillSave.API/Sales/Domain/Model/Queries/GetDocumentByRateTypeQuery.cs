namespace BillSave.API.Sales.Domain.Model.Queries;

/// Get Document By Rate Type Query
/// <summary>
/// This class is used to get document by rate type.
/// </summary>
/// <param name="RateType">
/// The rate type of the document.
/// </param>
public record GetDocumentByRateTypeQuery(string RateType);