using BillSave.API.Sales.Domain.Model.Aggregates;

namespace BillSave.API.Sales.Domain.Services;

/// <summary>
/// Document Eacr Calculation Service Interface
/// </summary>
public interface IDocumentEacrCalculationService
{
    /// <summary>
    /// Calculate the effective annual cost rate of a document
    /// </summary>
    /// <param name="document">
    /// The <see cref="Document"/> document to calculate the effective annual cost rate for
    /// </param>
    /// <returns>
    /// The effective annual cost rate
    /// </returns>
    public Task<decimal> CalculateEffectiveAnnualCostRate(Document document);
}