namespace BillSave.API.Portfolio.Domain.Model.Commands;

/// Update Effective Annual Cost Rate Command.
/// <summary>
/// This command is used to update the effective annual cost rate of a pack.
/// </summary>
/// <param name="PackId">
/// The pack id.
/// </param>
/// <param name="EffectiveAnnualCostRate">
/// The effective annual cost rate.
/// </param>
public record UpdateEffectiveAnnualCostRateCommand(int PackId, decimal EffectiveAnnualCostRate);