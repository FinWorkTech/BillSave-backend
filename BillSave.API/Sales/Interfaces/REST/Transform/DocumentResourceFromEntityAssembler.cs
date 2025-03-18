using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Interfaces.REST.Resources;

namespace BillSave.API.Sales.Interfaces.REST.Transform;

/// Document Resource From Entity Assembler
/// <summary>
/// This class is used to assemble the Document Resource from the Document entity.
/// </summary>
public class DocumentResourceFromEntityAssembler
{
    /// Document Resource
    /// <summary>
    /// This method is used to assemble the Document Resource from the Document entity.
    /// </summary>
    /// <param name="entity">
    /// The <see cref="Document"/> entity.
    /// </param>
    /// <returns>
    /// The <see cref="DocumentResource"/> object.
    /// </returns>
    public static DocumentResource ToResourceFromEntity(Document entity)
    {
        return new DocumentResource(
            entity.Id,
            entity.PortfolioId,
            entity.Code.Value,
            entity.IssueDate.ToString(),
            entity.DueDate.ToString(),
            entity.Rate.Type,
            entity.Rate.Value,
            entity.Currency.Code,
            entity.NominalAmount,
            entity.EffectiveAnnualCostRate
            );
    }
}