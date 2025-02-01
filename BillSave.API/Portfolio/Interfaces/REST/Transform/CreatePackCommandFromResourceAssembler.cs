using BillSave.API.Portfolio.Domain.Model.Commands;
using BillSave.API.Portfolio.Interfaces.REST.Resources;

namespace BillSave.API.Portfolio.Interfaces.REST.Transform;

/// <summary>
/// Create Pack Command From Resource Assembler.
/// </summary>
public class CreatePackCommandFromResourceAssembler
{
    /// <summary>
    /// Convert <see cref="CreatePackResource"/> to <see cref="CreatePackCommand"/>.
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreatePackResource"/> resource.
    /// </param>
    /// <returns>
    /// The <see cref="CreatePackCommand"/> command.
    /// </returns>
    public static CreatePackCommand ToCommandFromResource(CreatePackResource resource)
    {
        return new CreatePackCommand(
            resource.Name,
            resource.DiscountDate,
            resource.UserId
            );
    }
}