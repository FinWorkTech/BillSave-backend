using MediatR;
using BillSave.API.Sales.Domain.Model.Events;
using BillSave.API.Sales.Domain.Repositories;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Application.ACL.OutboundServices;
using BillSave.API.Sales.Domain.Model.ValueObjects;

namespace BillSave.API.Sales.Application.Internal.EventHandlers;

/// <summary>
/// This class represents the event handler for the DocumentChangedEvent.
/// </summary>
/// <param name="externalPortfolioService">
/// The <see cref="ExternalPortfolioService"/> service.
/// </param>
/// <param name="documentRepository">
/// The <see cref="IDocumentRepository"/> repository.
/// </param>
public class DocumentChangedEventHandler(ExternalPortfolioService externalPortfolioService, 
    IDocumentRepository documentRepository) : INotificationHandler<DocumentChangedEvent>
{   
    public async Task Handle(DocumentChangedEvent notification, CancellationToken cancellationToken)
    {
        var documents = await documentRepository.FindByPortfolioIdAsync(notification.PortfolioId);
        
        var effectiveAnnualCostRate = await CalculatePortfolioEffectiveAnnualCostRate(documents);
            
        await externalPortfolioService.UpdateEffectiveAnnualCostRateAsync(
            notification.PortfolioId, effectiveAnnualCostRate);
    } 
    
    /// <summary>
    /// Calculates the effective annual cost rate for a portfolio based on its documents.
    /// </summary>
    private static async Task<decimal> CalculatePortfolioEffectiveAnnualCostRate(IEnumerable<Document> documents)
    {
        decimal totalNominalValue = 0;
        decimal totalWeightedEffectiveAnnualCostRate = 0;

        // Calculate the effective annual cost rate for each document in parallel
        var tasks = documents.Select(document =>
        {
            try
            {
                var documentEffectiveAnnualCostRate = CalculateDocumentEffectiveAnnualCostRate(document);
                return Task.FromResult(new { documentEffectiveAnnualCostRate, document.NominalAmount });
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while calculating the effective annual cost rate for the document {document.Id}", ex);
            }
        });
        
        var results = await Task.WhenAll(tasks);
        
        foreach (var result in results)
        {
            totalWeightedEffectiveAnnualCostRate += result.documentEffectiveAnnualCostRate * result.NominalAmount;
            totalNominalValue += result.NominalAmount;
        }

        return totalNominalValue == 0 ? 0 : Math.Round(totalWeightedEffectiveAnnualCostRate / totalNominalValue, 6);
    }

    /// <summary>
    /// This method calculates the effective annual cost rate for a document.
    /// </summary>
    private static decimal CalculateDocumentEffectiveAnnualCostRate(Document document)
    {
        ValidateDocumentDates(document);

        var effectiveRate = CalculateEffectiveRate(document.Rate);
        
        var days = CalculateDaysBetweenDates(document.IssueDate.Value, document.DueDate.Value);

        var presentValue = CalculatePresentValue(document.NominalAmount, effectiveRate, days);
        
        return CalculateEffectiveAnnualCostRate(document.NominalAmount, presentValue, days);
    }
    
    /// <summary>
    /// This method validates the dates of a document.
    /// </summary>
    private static void ValidateDocumentDates(Document document)
    {
        if (document.IssueDate == default || document.DueDate == default)
            throw new ArgumentException("The dates must be specified");

        if (document.DueDate.Value <= document.IssueDate.Value)
            throw new ArgumentException("Due date must be greater than the issue date");
    }
    
    /// <summary>
    /// This method calculates the effective rate for a rate.
    /// </summary>
    private static decimal CalculateEffectiveRate(Rate rate)
    {
        const int capitalizationPeriods = 12;

        if (rate.Type != "Nominal") 
            return rate.Value;
        
        var periodicRate = rate.Value / capitalizationPeriods;
        
        return CalculateCompoundRate(periodicRate, capitalizationPeriods);
    }
    
    /// <summary>
    /// This method calculates the compound rate for a periodic rate and number of periods.
    /// </summary>
    private static decimal CalculateCompoundRate(decimal periodicRate, int periods)
    {
        var result = 1m;
        
        for (var i = 0; i < periods; i++)
        {
            result *= (1m + periodicRate);
        }
        return result - 1m;
    }
    
    /// <summary>
    /// This method calculates the number of days between two dates.
    /// </summary>
    private static int CalculateDaysBetweenDates(DateTime startDate, DateTime endDate)
    {
        return (endDate - startDate).Days;
    }
    
    /// <summary>
    /// This method calculates the present value for a nominal amount and effective rate.
    /// </summary>
    private static decimal CalculatePresentValue(decimal nominalAmount, decimal effectiveRate, int days)
    {
        var exponent = days / 365.0;
        
        return nominalAmount / Power(1 + effectiveRate, exponent);
    }
    
    /// <summary>
    /// This method calculates the effective annual cost rate for a nominal amount and present value.
    /// </summary>
    private static decimal CalculateEffectiveAnnualCostRate(decimal nominalAmount, decimal presentValue, int days)
    {
        var ratio = nominalAmount / presentValue;
        
        var exponent = 365.0 / days;
        
        return Power(ratio, exponent) - 1;
    }
    
    /// <summary>
    /// This method calculates the power of a base value and an exponent.
    /// </summary>
    private static decimal Power(decimal baseValue, double exponent)
    {
        return (decimal)Math.Pow((double)baseValue, exponent);
    }
}
