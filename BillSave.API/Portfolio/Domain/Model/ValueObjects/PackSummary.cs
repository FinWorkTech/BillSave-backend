namespace BillSave.API.Portfolio.Domain.Model.ValueObjects;

/// Pack summary.
/// <summary>
/// Represents a summary of packs.
/// </summary>
public record PackSummary(int ActivePacks, int TotalDocuments, decimal AverageEffectiveAnnualCostRate);
