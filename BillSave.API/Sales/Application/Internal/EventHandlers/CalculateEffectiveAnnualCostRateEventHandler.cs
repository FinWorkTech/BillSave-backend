using MediatR;
using BillSave.API.Sales.Domain.Model.Events;
using BillSave.API.Sales.Domain.Model.Queries;
using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Domain.Contracts.Services;

namespace BillSave.API.Sales.Application.Internal.EventHandlers;

/// Calculate effective annual cost rate event handler
/// <summary>
/// The <see cref="INotificationHandler{TNotification}"/> implementation.
/// </summary>
/// <param name="documentQueryService">
/// The <see cref="IDocumentQueryService"/> query service.
/// </param>
/// <param name="documentCommandService">
/// The <see cref="IDocumentCommandService"/> command service.
/// </param>
public class CalculateEffectiveAnnualCostRateEventHandler 
    (IDocumentQueryService documentQueryService, IDocumentCommandService documentCommandService) 
        : INotificationHandler<EffectiveAnnualCostRateCalculatedEvent>
{
    public async Task Handle(EffectiveAnnualCostRateCalculatedEvent notification, CancellationToken cancellationToken)
    {
        var getDocumentByIdQuery = new GetDocumentByIdQuery(notification.DocumentId);

        var document = await documentQueryService.Handle(getDocumentByIdQuery);

        if (document != null)
        {
            var calculatedEffectiveAnnualCostRate = CalculateEffectiveAnnualCostRate(
                document.IssueDate.Value,
                document.DueDate.Value,
                document.Rate.Type,
                document.Rate.Value,
                document.NominalAmount
            );
            
            var updateDocumentCommand = new UpdateEffectiveAnnualCostRateCommand(
                document!.Id,
                calculatedEffectiveAnnualCostRate
            );

            await documentCommandService.Handle(updateDocumentCommand);
        }
    }
    
    private static decimal CalculateEffectiveAnnualCostRate(DateTime issueDate, DateTime dueDate, 
        string rateType, decimal rateValue, decimal nominalAmount)
    {
        ValidateDocumentDates(issueDate, dueDate);

        var effectiveRate = CalculateEffectiveRate(rateType, rateValue);
        var days = CalculateDaysBetweenDates(issueDate, dueDate);
        var presentValue = CalculatePresentValue(nominalAmount, effectiveRate, days);

        return CalculateEffectiveAnnualCostRate(nominalAmount, presentValue, days);
    }
    
    private static decimal CalculateCompoundRate(decimal periodicRate, int periods)
    {
        var result = 1m;
        
        for (var i = 0; i < periods; i++)
            result *= (1m + periodicRate);
        
        return result - 1m;
    }
    
    private static decimal CalculateEffectiveRate(string rateType, decimal rateValue)
    {
        const int capitalizationPeriods = 12;
        
        if (rateType != "Nominal") 
            return rateValue;
        
        var periodicRate = rateValue / capitalizationPeriods;
        
        return CalculateCompoundRate(periodicRate, capitalizationPeriods);
    }

    private static int CalculateDaysBetweenDates(DateTime startDate, DateTime endDate)
    {
        return (endDate - startDate).Days;
    }

    private static decimal CalculatePresentValue(decimal nominalAmount, decimal effectiveRate, int days)
    {
        var exponent = days / 360.0;
        
        return nominalAmount / Power(1 + effectiveRate, exponent);
    }

    private static decimal CalculateEffectiveAnnualCostRate(decimal nominalAmount, decimal presentValue, int days)
    {
        var ratio = nominalAmount / presentValue;
        var exponent = 360.0 / days;

        return Power(ratio, exponent) - 1;
    }
    
    private static void ValidateDocumentDates(DateTime issueDate, DateTime dueDate)
    {
        if (issueDate == default || dueDate == default)
            throw new ArgumentException("The dates must be specified");
        
        if (dueDate <= issueDate) 
            throw new ArgumentException("Due date must be greater than the issue date.");
    }

    private static decimal Power(decimal baseValue, double exponent)
    {
        return (decimal)Math.Pow((double)baseValue, exponent);
    }
}