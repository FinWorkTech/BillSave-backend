using System.Net.Mime;
using BillSave.API.Sales.Domain.Services;
using BillSave.API.Sales.Interfaces.REST.Resources;
using BillSave.API.Sales.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BillSave.API.Sales.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Documents")]
public class DocumentsController(IDocumentCommandService documentCommandService, IDocumentQueryService documentQueryService)
    : ControllerBase
{
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new document",
        Description = "Create a new document",
        OperationId = "CreateDocument")]
    [SwaggerResponse(StatusCodes.Status201Created, "The dcument was created", typeof(DocumentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The document could not be created")]
    public async Task<ActionResult> CreateProduct([FromBody] CreateDocumentResource resource)
    {
        var createDocumentCommand = CreateDocumentCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var result = await documentCommandService.Handle(createDocumentCommand);
        
        if (result is null)
            return BadRequest(); 
        
        return Created(string.Empty, DocumentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
}