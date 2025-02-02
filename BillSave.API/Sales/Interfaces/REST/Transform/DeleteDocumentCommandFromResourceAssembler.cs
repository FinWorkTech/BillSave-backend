using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Interfaces.REST.Resources;

namespace BillSave.API.Sales.Interfaces.REST.Transform;

public class DeleteDocumentCommandFromResourceAssembler
{
    public static DeleteDocumentCommand ToCommandFromResource(DeleteDocumentResource resource)
    {
        return new DeleteDocumentCommand(
            resource.Id,
            resource.PortfolioId
        );
    }
}