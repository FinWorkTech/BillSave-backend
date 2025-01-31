using System.Net.Mime;
using BillSave.API.Profiles.Domain.Model.Queries;
using BillSave.API.Profiles.Domain.Services;
using BillSave.API.Profiles.Interfaces.REST.Resources;
using BillSave.API.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BillSave.API.Profiles.Interfaces.REST;

/// <summary>
/// Profiles controller.
/// </summary>
/// <remarks>
/// This controller provides endpoints to interact with profiles.
/// </remarks>
/// <param name="profileCommandService">
/// The profile command service. 
/// </param>
/// <param name="profileQueryService">
/// The profile query service. 
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profiles Endpoints")]
public class ProfilesController(IProfileCommandService profileCommandService, IProfileQueryService profileQueryService)
    : ControllerBase
{
    [HttpGet("{profileId:int}")]
    [SwaggerOperation("Get Profile by Id", "Get a profile by its unique identifier.", 
        OperationId = "GetProfileById")]
    [SwaggerResponse(200, "The profile was found and returned.", typeof(ProfileResource))]
    [SwaggerResponse(404, "The profile was not found.")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(profileId);
        
        var profile = await profileQueryService.Handle(getProfileByIdQuery);

        if (profile is null) return NotFound();

        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);

        return Ok(profileResource);
    }
    
    [HttpPost]
    [SwaggerOperation("Create Profile", "Create a new profile.", OperationId = "CreateProfile")]
    [SwaggerResponse(201, "The profile was created.", typeof(ProfileResource))]
    [SwaggerResponse(400, "The profile was not created.")]
    public async Task<IActionResult> CreateProfile(CreateProfileResource resource)
    {
        var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var profile = await profileCommandService.Handle(createProfileCommand);
        
        if (profile is null) return BadRequest();
        
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        
        return CreatedAtAction(nameof(GetProfileById), new { profileId = profile.Id }, profileResource);
    }
}