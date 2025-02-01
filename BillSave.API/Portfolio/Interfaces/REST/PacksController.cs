using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using BillSave.API.Portfolio.Domain.Services;
using BillSave.API.Portfolio.Domain.Model.Queries;
using BillSave.API.Portfolio.Domain.Model.Commands;
using BillSave.API.Portfolio.Interfaces.REST.Resources;
using BillSave.API.Portfolio.Interfaces.REST.Transform;

namespace BillSave.API.Portfolio.Interfaces.REST;

[ApiController]
[Route("api/v1/portfolios")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Portfolios")]
public class PacksController(IPackCommandService packCommandService, IPackQueryService packQueryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new portfolio",
        Description = "Create a new portfolio",
        OperationId = "CreatePortfolio")]
    [SwaggerResponse(StatusCodes.Status201Created, "The portfolio was created", typeof(PackResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The portfolio could not be created")]
    public async Task<ActionResult> CreateProduct([FromBody] CreatePackResource resource)
    {
        var createPackCommand = CreatePackCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var result = await packCommandService.Handle(createPackCommand);
        
        if (result is null)
            return BadRequest(); 
        
        return Created(string.Empty, PackResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpPut("{id}")]
    [SwaggerOperation(
        Summary = "Update the portfolio",
        Description = "Update the specified portfolio",
        OperationId = "UpdatePortfolio")]
    [SwaggerResponse(StatusCodes.Status200OK, "The portfolio was updated")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data provided")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The portfolio was not found")]
    public async Task<ActionResult> UpdateProductOwner([FromBody] UpdatePackResource resource)
    {
        var updatePackCommand = new UpdatePackCommand(resource.Id, resource.Name, resource.DiscountDate);

        var result = await packCommandService.Handle(updatePackCommand);

        if (result is null)
            return NotFound("The portfolio was not found");

        return Ok("Portfolio updated successfully");
    }
    
    [HttpGet("users/{userId}/portfolios")]
    [SwaggerOperation(
        Summary = "Get portfolios by UserId",
        Description = "Get all portfolios associated with the specified UserId",
        OperationId = "GetPortfoliosByUserId")]
    [SwaggerResponse(StatusCodes.Status200OK, 
        "The portfolios were found", typeof(IEnumerable<PackResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No portfolios found for the given UserId")]
    public async Task<ActionResult> GetProductsByUserId(int userId)
    {
        var getPacksByUserIdQuery = new GetPackByUserIdQuery(userId);
        
        var result = await packQueryService.Handle(getPacksByUserIdQuery);
        
        var resources = result.Select(PackResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(resources);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(
        Summary = "Delete a portfolio by ID",
        Description = "Delete the portfolio with the specified ID",
        OperationId = "DeletePortfolio")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The portfolio was deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The portfolio was not found")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var resource = new DeletePackResource(id);
        
        var deletePackCommand = DeletePackCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        await packCommandService.Handle(deletePackCommand);
        
        return NoContent();
    }
}