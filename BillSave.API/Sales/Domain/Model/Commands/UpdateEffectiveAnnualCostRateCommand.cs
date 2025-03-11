namespace BillSave.API.Sales.Domain.Model.Commands;

/// Update effective annual cost rate command
/// <summary>
/// This class represents the update effective annual cost rate command. It is used to update the effective annual cost rate of a document.
/// </summary>
/// <param name="DocumentId">
/// The document id.
/// </param>
/// <param name="EffectiveAnnualCostRate">
/// The effective annual cost rate.
/// </param>
public record UpdateEffectiveAnnualCostRateCommand(int DocumentId, decimal EffectiveAnnualCostRate);