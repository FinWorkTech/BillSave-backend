using BillSave.API.Portfolio.Domain.Model.Commands;
using BillSave.API.Portfolio.Interfaces.REST.Resources;

namespace BillSave.API.Portfolio.Interfaces.REST.Transform;

/// <summary>
/// The delete pack command from resource assembler.
/// </summary>
public class DeletePackCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a <see cref="DeletePackResource"/> to a <see cref="DeletePackCommand"/>.
    /// </summary>
    /// <param name="resource">
    /// The <see cref="DeletePackResource"/> resource.
    /// </param>
    /// <returns>
    /// The <see cref="DeletePackCommand"/>.
    /// </returns>
    public static DeletePackCommand ToCommandFromResource(DeletePackResource resource)
    {
        return new DeletePackCommand(
            resource.Id
        );
    }
}