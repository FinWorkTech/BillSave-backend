using BillSave.API.Portfolio.Domain.Model.Aggregates;
using BillSave.API.Portfolio.Interfaces.REST.Resources;

namespace BillSave.API.Portfolio.Interfaces.REST.Transform;

/// <summary>
/// The pack resource from entity assembler.
/// </summary>
public class PackResourceFromEntityAssembler
{
    /// <summary>
    /// Convert a <see cref="Pack"/> entity to a <see cref="PackResource"/>.
    /// </summary>
    /// <param name="pack">
    /// The <see cref="Pack"/> entity.
    /// </param>
    /// <returns>
    /// The <see cref="PackResource"/>.
    /// </returns>
    public static PackResource ToResourceFromEntity(Pack pack)
    {
        return new PackResource(
            pack.Id,
            pack.UserId,
            pack.Name,
            pack.DiscountDate,
            pack.TotalDocuments
            );
    }
}