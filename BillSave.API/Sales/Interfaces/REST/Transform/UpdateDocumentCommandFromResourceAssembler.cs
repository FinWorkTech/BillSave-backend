using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Interfaces.REST.Resources;

namespace BillSave.API.Sales.Interfaces.REST.Transform;

public class UpdateDocumentCommandFromResourceAssembler
{
    public static UpdateDocumentCommand ToCommandFromResource(UpdateDocumentResource resource, int documentId)
    {
        return new UpdateDocumentCommand(
            documentId,
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