namespace BillSave.API.Portfolio.Interfaces.REST.Resources;

public record PackSummaryResource(int ActivePacks, int TotalDocuments, decimal AverageEffectiveAnnualCostRate);