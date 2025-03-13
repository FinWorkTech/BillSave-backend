namespace BillSave.API.Sales.Domain.Model.Commands;

/// <summary>
/// Update effective annual cost rate command
/// </summary>
/// <param name="PortfolioId">
/// The portfolio identifier.
/// </param>
public record UpdateEffectiveAnnualCostRateCommand(int PortfolioId);