using System.Net.Mime;
using BillSave.API.IAM.Application.Interfaces.QueryServices;
using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;
using BillSave.API.IAM.Domain.Model.Queries;
using BillSave.API.IAM.Interfaces.REST.Resources;
using BillSave.API.IAM.Interfaces.REST.Transform;

namespace BillSave.API.IAM.Interfaces.REST;

/// <summary>
/// User Controller 
/// </summary>
/// <param name="userQueryService">
/// The <see cref="IUserQueryService"/> user query service
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User Endpoints")]
public class UserController(IUserQueryService userQueryService) : ControllerBase
{
    /// <summary>
    /// Get User by its Id 
    /// </summary>
    /// <param name="id">
    /// The User Id
    /// </param>
    /// <returns>
    /// The <see cref="Task{IActionResult}"/> containing the <see cref="UserResource"/> if found, otherwise NotFound
    /// </returns>
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get User by its Id",
        Description = "Get User by Id",
        OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "User found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        
        var user = await userQueryService.Handle(getUserByIdQuery);
        
        if (user is null) return NotFound();
        
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        
        return Ok(userResource);
    }
}