using BillSave.API.Sales.Domain.Model.Aggregates;

namespace BillSave.API.Sales.Domain.Services.Contracts;

/// Effective Annual Cost Rate Calculation Service Interface
/// <summary>
/// This interface defines the contract for the Effective Annual Cost Rate Calculation Service.
/// </summary>
public interface IEacrCalculationService
{
    /// <summary>
    /// Calculate the Effective Annual Cost Rate for the given documents.
    /// </summary>
    /// <param name="documents">
    /// The documents for which the Effective Annual Cost Rate should be calculated.
    /// </param>
    /// <returns>
    /// The calculated Effective Annual Cost Rate.
    /// </returns>
    Task<decimal> CalculateEffectiveAnnualCostRate(IEnumerable<Document> documents);
}