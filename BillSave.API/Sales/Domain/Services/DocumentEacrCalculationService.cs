using BillSave.API.Sales.Domain.Model.Aggregates;

namespace BillSave.API.Sales.Domain.Services;

/// <summary>
/// Document Eacr Calculation Service
/// </summary>
public class DocumentEacrCalculationService : IDocumentEacrCalculationService
{
       public async Task<decimal> CalculateEffectiveAnnualCostRate(Document document)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document), "Document cannot be null.");

        ValidateDocumentDates(document.IssueDate!.Value, document.DueDate!.Value);

        return await Task.Run(() => CalculateDocumentEacr(document));
    }

    private static decimal CalculateDocumentEacr(Document document)
    {
        var effectiveRate = ConvertToEffectiveRate(document.Rate.Type, document.Rate.Value);
        
        var years = CalculateYearsBetweenDates(document.IssueDate!.Value, document.DueDate!.Value);
        
        var presentValue = CalculatePresentValue(document.NominalAmount, effectiveRate, years);
        
        return CalculateTcea(document.NominalAmount, presentValue, years);
    }

    private static void ValidateDocumentDates(DateTime issueDate, DateTime dueDate)
    {
        if (dueDate <= issueDate)
            throw new ArgumentException("Due date must be greater than the issue date.");
    }

    private static decimal ConvertToEffectiveRate(string rateType, decimal rateValue)
    {
        const int capitalizationPeriods = 12; 

        if (rateType.Equals("Efectiva", StringComparison.OrdinalIgnoreCase))
            return rateValue; 

        if (!rateType.Equals("Nominal", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Rate type must be either 'Nominal' or 'Efectiva'.");
        
        return (decimal)(Math.Pow((double)(1 + (rateValue / capitalizationPeriods)), capitalizationPeriods) - 1);
    }

    private static double CalculateYearsBetweenDates(DateTime startDate, DateTime endDate)
    {
        return (endDate - startDate).TotalDays / 365.0;
    }

    private static decimal CalculatePresentValue(decimal nominalAmount, decimal effectiveRate, double years)
    {
        return nominalAmount / (decimal)Math.Pow(1 + (double)effectiveRate, years);
    }

    private static decimal CalculateTcea(decimal nominalAmount, decimal presentValue, double years)
    {
        return (decimal)(Math.Pow((double)(nominalAmount / presentValue), 1 / years) - 1);
    }
}