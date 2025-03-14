using BillSave.API.Portfolio.Domain.Model.Aggregates;
using BillSave.API.Portfolio.Domain.Model.Commands;

namespace BillSave.API.Portfolio.Application.Contracts;

/// <summary>
/// The service that handles the commands related to the packs.
/// </summary>
public interface IPackCommandService
{
    /// <summary>
    /// Handles the creation of a new pack.
    /// </summary>
    /// <param name="command">
    /// The command containing the data to create the pack.
    /// </param>
    /// <returns>
    /// The created pack.
    /// </returns>
    Task<Pack?> Handle(CreatePackCommand command);
    
    /// <summary>
    /// Handles the deletion of a pack.
    /// </summary>
    /// <param name="command">
    /// The command containing the data to delete the pack.
    /// </param>
    /// <returns>
    /// The deleted pack.
    /// </returns>
    Task<Pack?> Handle(DeletePackCommand command);
    
    /// <summary>
    /// Handles the update of a pack.
    /// </summary>
    /// <param name="command">
    /// The command containing the data to update the pack.
    /// </param>
    /// <returns>
    /// The updated pack.
    /// </returns>
    Task<Pack?> Handle(UpdatePackCommand command);

    /// <summary>
    /// Handles the increment of the total documents of a pack.
    /// </summary>
    /// <param name="command">
    /// The command containing the data to increment the total documents of the pack.
    /// </param>
    /// <returns>
    /// The updated pack.
    /// </returns>
    Task Handle(UpdateQuantityOfDocumentsCommand command);

    /// <summary>
    /// Handles the update of the effective annual cost rate of a pack.
    /// </summary>
    /// <param name="command">
    /// The command containing the data to update the effective annual cost rate of the pack.
    /// </param>
    /// <returns>
    /// The updated pack.
    /// </returns>
    Task Handle(UpdateEffectiveAnnualCostRateCommand command);
}