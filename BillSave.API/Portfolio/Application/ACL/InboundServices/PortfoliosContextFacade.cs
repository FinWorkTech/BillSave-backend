using BillSave.API.Portfolio.Interfaces.ACL;
using BillSave.API.Portfolio.Application.Interfaces.CommandServices;
using BillSave.API.Portfolio.Domain.Model.Commands;

namespace BillSave.API.Portfolio.Application.ACL.InboundServices;

/// Portfolios context facade.
/// <summary>
/// The <see cref="IPortfoliosContextFacade"/> context facade.
/// </summary>
/// <param name="packCommandService">
/// The <see cref="IPackCommandService"/> pack command service.
/// </param>
public class PortfoliosContextFacade(IPackCommandService packCommandService) 
    : IPortfoliosContextFacade
{
    /// Increment total documents.
    /// <summary>
    /// Increment the total documents of a Portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The Portfolio id.
    /// </param>
    public async Task IncrementTotalDocumentsAsync(int portfolioId)
    {
        var command = new UpdateQuantityOfDocumentsCommand(portfolioId, "increment");
        await packCommandService.Handle(command);
    }
    
    /// Decrement total documents.
    /// <summary>
    /// Decrement the total documents of a Portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The Portfolio id.
    /// </param>
    public async Task DecrementTotalDocumentsAsync(int portfolioId)
    {
        var command = new UpdateQuantityOfDocumentsCommand(portfolioId, "decrement");
        await packCommandService.Handle(command);
    }
    
    /// <summary>
    /// Update the effective annual cost rate of a Portfolio.
    /// </summary>
    /// <param name="portfolioId">
    /// The Portfolio id.
    /// </param>
    /// <param name="effectiveAnnualCostRate">
    /// The effective annual cost rate.
    /// </param>
    public async Task UpdateEffectiveAnnualCostRateAsync(int portfolioId, decimal effectiveAnnualCostRate)
    {
        var command = new UpdateEffectiveAnnualCostRateCommand(portfolioId, effectiveAnnualCostRate);
        await packCommandService.Handle(command);
    }
}