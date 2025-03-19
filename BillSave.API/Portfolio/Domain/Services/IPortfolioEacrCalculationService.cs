namespace BillSave.API.Portfolio.Domain.Services;

public interface IPortfolioEacrCalculationService
{
    Task<decimal> CalculateEffectiveAnnualCostRate(List<decimal> nominalAmount, List<decimal> effectiveAnnualCostRates);
}   