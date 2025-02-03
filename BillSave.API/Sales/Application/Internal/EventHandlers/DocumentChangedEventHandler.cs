using MediatR;
using BillSave.API.Sales.Domain.Model.Events;
using BillSave.API.Sales.Domain.Repositories;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Application.ACL.OutboundServices;

namespace BillSave.API.Sales.Application.Internal.EventHandlers;

public class DocumentChangedEventHandler(ExternalPortfolioService externalPortfolioService, 
    IDocumentRepository documentRepository) : INotificationHandler<DocumentChangedEvent>
{
    public async Task Handle(DocumentChangedEvent notification, CancellationToken cancellationToken)
    {
        var documents = await documentRepository.FindByPortfolioIdAsync(notification.PortfolioId);
        
        decimal effectiveAnnualCostRate = CalculatePortfolioEffectiveAnnualCostRate(documents);
            
        await externalPortfolioService.UpdateEffectiveAnnualCostRateAsync(
            notification.PortfolioId, effectiveAnnualCostRate);
    }
    
    /// <summary>
    /// Calculates the effective annual cost rate for a portfolio based on its documents.
    /// </summary>
    private decimal CalculatePortfolioEffectiveAnnualCostRate(IEnumerable<Document> documents)
    {
        decimal totalNominalValue = 0;
        decimal totalWeightedEffectiveAnnualCostRate = 0;

        foreach (var document in documents)
        {
            decimal documentEffectiveAnnualCostRate = CalculateDocumentEffectiveAnnualCostRate(document);
            totalWeightedEffectiveAnnualCostRate += documentEffectiveAnnualCostRate * document.NominalAmount;
            totalNominalValue += document.NominalAmount;
        }

        return totalNominalValue == 0 ? 0 : Math.Round(totalWeightedEffectiveAnnualCostRate / totalNominalValue, 6);
    }

    /// <summary>
    /// Calculates the effective annual cost rate (TCEA) for a single document.
    /// </summary>
    private decimal CalculateDocumentEffectiveAnnualCostRate(Document document)
    {
        if (document.IssueDate == null || document.DueDate == null || document.Rate == null)
            throw new ArgumentException("Document dates and rate must not be null");

        const int capitalizationPeriods = 12;
        decimal effectiveRate;

        if (document.Rate.Type == "Nominal")
        {
            decimal periodicRate = document.Rate.Value / capitalizationPeriods;
            effectiveRate = (decimal)(Math.Pow(1 + (double)periodicRate, capitalizationPeriods) - 1);
        }
        else
        {
            effectiveRate = document.Rate.Value;
        }

        double days = (document.DueDate.Value - document.IssueDate.Value).Days;
        if (days <= 0)
            throw new ArgumentException("Invalid date range: the issue date must be earlier than the due date.");
        
        // Present Value Calculation
        decimal presentValue = document.NominalAmount / (decimal)Math.Pow(1 + (double)effectiveRate, days / 365.0);

        // TCEA Calculation
        decimal effectiveAnnualCostRate = (decimal)(Math.Pow((double)(document.NominalAmount / presentValue), 365.0 / days) - 1);
        
        Console.WriteLine($"Doc {document.Id}: TEA = {effectiveRate}, PV = {presentValue}, TCEA = {effectiveAnnualCostRate}");
        return effectiveAnnualCostRate;
    }
}
