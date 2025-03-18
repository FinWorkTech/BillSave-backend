using BillSave.API.Sales.Domain.Model.Aggregates;

namespace BillSave.API.Sales.Domain.Services;

/// Effective Annual Cost Rate Calculation Service.
/// <summary>
/// The <see cref="IPortfolioEacrCalculationService"/> implementation.
/// </summary>
public class PortfolioEacrCalculationService : IPortfolioEacrCalculationService
{
    public async Task<decimal> CalculateEffectiveAnnualCostRate(IEnumerable<Document> documents)
    {
        var enumerable = documents.ToList();
        
        if (documents == null || enumerable.Count == 0)
            throw new ArgumentException("Documents cannot be null or empty.");

        var documentList = enumerable.ToList();

        decimal totalNominalValue = 0;
        decimal totalWeightedEffectiveAnnualCostRate = 0;

        var tasks = documentList.Select(async document =>
        {
            var documentEacEffectiveAnnualCostRate = await Task.Run(
                () => CalculateDocumentEffectiveAnnualCostRate(document));
            
            return new { documentEacEffectiveAnnualCostRate, document.NominalAmount };
        });

        var results = await Task.WhenAll(tasks);

        foreach (var result in results)
        {
            totalWeightedEffectiveAnnualCostRate += result.documentEacEffectiveAnnualCostRate * result.NominalAmount;
            totalNominalValue += result.NominalAmount;
        }

        return totalNominalValue == 0 ? 0 : Math.Round(totalWeightedEffectiveAnnualCostRate / totalNominalValue, 6);
    }
    
    private static decimal CalculateDocumentEffectiveAnnualCostRate(Document document)
    {
        ValidateDocumentDates(document.IssueDate.Value, document.DueDate.Value);
        
        var effectiveRate = CalculateEffectiveRate(document.Rate.Type, document.Rate.Value);
        
        var days = CalculateDaysBetweenDates(document.IssueDate.Value, document.DueDate.Value);
        
        var presentValue = CalculatePresentValue(document.NominalAmount, effectiveRate, days);
        
        return CalculateEffectiveAnnualCostRate(document.NominalAmount, presentValue, days);
    }
    
    private static void ValidateDocumentDates(DateTime issueDate, DateTime dueDate)
    {
        if (dueDate == default || issueDate == default)
            throw new ArgumentException("The dates must be specified");

        if (dueDate <= issueDate)
            throw new ArgumentException("Due date must be greater than the issue date");
    }
    
    
    private static decimal CalculateEffectiveRate(string rateType, decimal rateValue)
    {
        const int capitalizationPeriods = 12;
        
        if (rateType != "Nominal") 
            return rateValue;
        
        var periodicRate = rateValue / capitalizationPeriods;
        
        return CalculateCompoundRate(periodicRate, capitalizationPeriods);
    }
    
    private static decimal CalculateCompoundRate(decimal periodicRate, int periods)
    {
        var result = 1m;
        
        for (var i = 0; i < periods; i++)
        {
            result *= (1m + periodicRate);
        }
        
        return result - 1m;
    }
    
    private static int CalculateDaysBetweenDates(DateTime startDate, DateTime endDate)
    {
        return (endDate - startDate).Days;
    }

    private static decimal CalculatePresentValue(decimal nominalAmount, decimal effectiveRate, int days)
    {
        var exponent = days / 365.0;
        return nominalAmount / Power(1 + effectiveRate, exponent);
    }
    
    private static decimal CalculateEffectiveAnnualCostRate(decimal nominalAmount, decimal presentValue, int days)
    {
        var exponent = 365.0 / days;
        return Power(nominalAmount / presentValue, exponent) - 1;
    }
    
    private static decimal Power(decimal baseValue, double exponent)
    {
        return (decimal)Math.Pow((double)baseValue, exponent);
    }
}