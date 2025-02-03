using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Interfaces.REST.Resources;

namespace BillSave.API.Sales.Interfaces.REST.Transform;

public class UpdateDocumentCommandFromResourceAssembler
{
    public static UpdateDocumentCommand ToCommandFromResource(UpdateDocumentResource resource)
    {
        return new UpdateDocumentCommand(
            resource.Id,
            resource.Code,
            resource.IssueDate,
            resource.DueDate,
            resource.RateType,
            resource.RateValue,
            resource.Currency,
            resource.NominalAmount
            );
    }
}