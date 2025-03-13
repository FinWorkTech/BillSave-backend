using BillSave.API.Profiles.Interfaces.ACL;

namespace BillSave.API.IAM.Application.ACL.OutboundServices;

/// <summary>
/// External profile service.
/// </summary>
/// <param name="profileContextFacade">
/// Profile context facade.
/// </param>
public class ExternalProfileService (IProfilesContextFacade profileContextFacade)
{
    /// <summary>
    /// Creates a new profile.
    /// </summary>
    /// <param name="fullName">
    /// Full name.
    /// </param>
    /// <returns>
    /// The profile identifier.
    /// </returns>
    public async Task<int> CreateProfile(string fullName)
    {
        return await profileContextFacade.CreateProfileAsync(fullName);
    }
}