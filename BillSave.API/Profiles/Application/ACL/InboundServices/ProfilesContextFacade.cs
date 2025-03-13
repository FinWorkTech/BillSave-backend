using BillSave.API.Profiles.Interfaces.ACL;
using BillSave.API.Profiles.Application.Contracts;
using BillSave.API.Profiles.Domain.Model.Commands;

namespace BillSave.API.Profiles.Application.ACL.InboundServices;

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