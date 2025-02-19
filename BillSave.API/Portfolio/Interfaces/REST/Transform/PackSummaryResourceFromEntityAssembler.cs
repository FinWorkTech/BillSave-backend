using BillSave.API.Portfolio.Domain.Model.ValueObjects;
using BillSave.API.Portfolio.Interfaces.REST.Resources;

namespace BillSave.API.Portfolio.Interfaces.REST.Transform;

public class PackSummaryResourceFromEntityAssembler
{
    public static PackSummaryResource ToResourceFromEntity(PackSummary packSummary)
    {
        return new PackSummaryResource(
            packSummary.ActivePacks,
            packSummary.TotalDocuments,
            packSummary.AverageEffectiveAnnualCostRate
            );
    }
}