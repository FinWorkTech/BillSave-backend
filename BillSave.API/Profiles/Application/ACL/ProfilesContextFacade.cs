using BillSave.API.Profiles.Domain.Model.Commands;
using BillSave.API.Profiles.Domain.Services;
using BillSave.API.Profiles.Interfaces.ACL;

namespace BillSave.API.Profiles.Application.ACL;

/// <summary>
/// Profiles context facade.
/// </summary>
/// <param name="profileCommandService">
/// The profile command service.
/// </param>
/// <param name="profileQueryService">
/// The profile query service.
/// </param>
public class ProfilesContextFacade(IProfileCommandService profileCommandService, 
    IProfileQueryService profileQueryService) : IProfilesContextFacade
{
    /// <inheritdoc />
    public async Task<int> CreateProfileAsync(string fullName)
    {
        var createProfileCommand = new CreateProfileCommand(fullName);

        var profile = await profileCommandService.Handle(createProfileCommand);

        return profile?.Id ?? 0;
    }
}