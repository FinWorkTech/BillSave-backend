using System.Net.Mime;
using BillSave.API.Sales.Domain.Model.Commands;
using BillSave.API.Sales.Domain.Model.Queries;
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
    [SwaggerResponse(StatusCodes.Status201Created, "The document was created", typeof(DocumentResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The document could not be created")]
    public async Task<ActionResult> CreateDocument([FromBody] CreateDocumentResource resource)
    {
        var createDocumentCommand = CreateDocumentCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var result = await documentCommandService.Handle(createDocumentCommand);
        
        if (result is null)
            return BadRequest(); 
        
        return Created(string.Empty, DocumentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update the document",
        Description = "Update the specified document",
        OperationId = "UpdateDocument")]
    [SwaggerResponse(StatusCodes.Status200OK, "The document was updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data provided")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The document was not found")]
    public async Task<ActionResult> UpdateDocument([FromBody] UpdateDocumentResource resource)
    {
        var updateDocumentCommand = new UpdateDocumentCommand(resource.Id, resource.Code, resource.IssueDate,
            resource.DueDate, resource.RateType, resource.RateValue, resource.Currency);

        var result = await documentCommandService.Handle(updateDocumentCommand);

        if (result is null)
            return NotFound("The document was not found");

        return Ok("Document updated successfully");
    }
    
    [HttpGet("portfolios/{portfolioId}/documents")]
    [SwaggerOperation(
        Summary = "Get documents by Portfolio Id",
        Description = "Get all documents associated with the specified Portfolio Id",
        OperationId = "GetDocumentsByPortfolioId")]
    [SwaggerResponse(StatusCodes.Status200OK, 
        "The documents were found", typeof(IEnumerable<DocumentResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No documents found for the given Portfolio Id")]
    public async Task<ActionResult> GetDocumentsByPortfolioId(int portfolioId)
    {
        var getDocumentsByPortfolioIdQuery = new GetDocumentByPortfolioIdQuery(portfolioId);
        
        var result = await documentQueryService.Handle(getDocumentsByPortfolioIdQuery);
        
        var resources = result.Select(DocumentResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(resources);
    }
}