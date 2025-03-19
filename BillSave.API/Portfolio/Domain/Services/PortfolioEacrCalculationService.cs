namespace BillSave.API.Portfolio.Domain.Services;

public class PortfolioEacrCalculationService : IPortfolioEacrCalculationService
{
    public async Task<decimal> CalculateEffectiveAnnualCostRate(List<decimal> nominalAmounts, List<decimal> effectiveAnnualCostRates)
    {
        if (nominalAmounts.Count != effectiveAnnualCostRates.Count)
            throw new ArgumentException(
                "Nominal Amounts and Effective Annual Cost Rates must have the same number of elements");
    
        var totalNominalAmount = nominalAmounts.Sum();
    
        var totalWeightEffectiveAnnualCostRate = 
            nominalAmounts.Select((t, i) => t * effectiveAnnualCostRates[i]).Sum();
        
        var effectiveAnnualCostRate = 
            totalNominalAmount > 0 ? totalWeightEffectiveAnnualCostRate / totalNominalAmount : 0;
        
        return await Task.FromResult(effectiveAnnualCostRate);
    }
}