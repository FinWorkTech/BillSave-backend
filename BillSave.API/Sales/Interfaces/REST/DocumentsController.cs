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
public class DocumentsController(IDocumentCommandService documentCommandService, 
    IDocumentQueryService documentQueryService) : ControllerBase
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
    
    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update the document",
        Description = "Update the specified document",
        OperationId = "UpdateDocument")]
    [SwaggerResponse(StatusCodes.Status200OK, "The document was updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data provided")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The document was not found")]
    public async Task<ActionResult> UpdateDocument([FromBody] UpdateDocumentResource resource, int id)
    {
        var updateDocumentCommand = new UpdateDocumentCommand(id, resource.Code, resource.IssueDate,
            resource.DueDate, resource.RateType, resource.RateValue, resource.Currency, resource.NominalAmount);

        var result = await documentCommandService.Handle(updateDocumentCommand);

        if (result is null)
            return NotFound("The document was not found");

        return Ok("Document updated successfully");
    }
    
    [HttpGet]
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
    
   
    [HttpGet("daterange")]
    [SwaggerOperation(
        Summary = "Get documents by date range",
        Description = "Get all documents within the specified date range",
        OperationId = "GetDocumentsByDateRange")]
    [SwaggerResponse(StatusCodes.Status200OK, "The documents were found", typeof(IEnumerable<DocumentResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No documents found for the given date range")]
    public async Task<ActionResult> GetDocumentsByDateRange(DateTime startDate, DateTime endDate)
    {
        var getDocumentsByDateRangeQuery = new GetDocumentByDateRangeQuery(startDate, endDate);
        
        var result = await documentQueryService.Handle(getDocumentsByDateRangeQuery);
        
        var resources = result.Select(DocumentResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(resources);
    }
    
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get a document by ID",
        Description = "Get the document with the specified ID",
        OperationId = "GetDocumentById")]
    [SwaggerResponse(
        StatusCodes.Status200OK, "The document was found", typeof(DocumentResource))]
    public async Task<ActionResult> GetDocumentById(int id)
    {
        var getDocumentByIdQuery = new GetDocumentByIdQuery(id);
        
        var result = await documentQueryService.Handle(getDocumentByIdQuery);
        
        if (result is null)
            return NotFound("The document was not found");
        
        return Ok(DocumentResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a document by ID",
        Description = "Delete the document with the specified ID",
        OperationId = "DeleteDocument")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The document was deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The document was not found")]
    public async Task<ActionResult> DeletePortfolio(int id, [FromQuery] int portfolioId)
    {
        var resource = new DeleteDocumentResource(id, portfolioId);
        
        var deleteDocumentCommand = DeleteDocumentCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        await documentCommandService.Handle(deleteDocumentCommand);
        
        return NoContent();
    }
}