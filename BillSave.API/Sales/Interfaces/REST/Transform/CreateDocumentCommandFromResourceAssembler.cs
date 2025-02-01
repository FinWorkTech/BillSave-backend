using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Interfaces.REST.Resources;

namespace BillSave.API.Sales.Interfaces.REST.Transform;

/// Create Document Command From Resource Assembler
/// <summary>
/// This class represents the Create Document Command From Resource Assembler. It is used to transform a Create Document Resource into a Create Document Command.
/// </summary>
public class CreateDocumentCommandFromResourceAssembler
{
    /// <summary>
    /// This method is used to transform a <see cref="CreateDocumentResource"/> into a <see cref="CreateDocumentCommand"/>.
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreateDocumentResource"/> resource.
    /// </param>
    /// <returns>
    /// The <see cref="CreateDocumentCommand"/> command.
    /// </returns>
    public static CreateDocumentCommand ToCommandFromResource(CreateDocumentResource resource)
    {
        return new CreateDocumentCommand(
            resource.Code,
            resource.IssueDate,
            resource.DueDate,
            resource.RateType,
            resource.RateValue,
            resource.Currency,
            resource.PortfolioId
        );
    }
}