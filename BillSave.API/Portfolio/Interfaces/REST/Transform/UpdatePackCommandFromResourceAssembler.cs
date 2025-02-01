using BillSave.API.Portfolio.Domain.Model.Commands;
using BillSave.API.Portfolio.Interfaces.REST.Resources;

namespace BillSave.API.Portfolio.Interfaces.REST.Transform;

/// <summary>
/// The update pack command from resource assembler.
/// </summary>
public class UpdatePackCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="UpdatePackResource"/> to a <see cref="UpdatePackCommand"/>.
    /// </summary>
    /// <param name="resource">
    /// The <see cref="UpdatePackResource"/> resource.
    /// </param>
    /// <returns>
    /// The <see cref="UpdatePackCommand"/> command.
    /// </returns>
    public static UpdatePackCommand ToCommandFromResource(UpdatePackResource resource)
    {
        return new UpdatePackCommand(
            resource.Id,
            resource.Name,
            resource.DiscountDate
            );
    }
}