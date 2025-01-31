namespace BillSave.API.Profiles.Interfaces.ACL;

/// <summary>
/// Facade for the profiles context 
/// </summary>
public interface IProfilesContextFacade
{
    /// <summary>
    /// Create a profile with the given full name.
    /// </summary>
    /// <param name="fullName">
    /// The full name of the profile.
    /// </param>
    /// <returns>
    /// The id of the created profile.
    /// </returns>
    Task<int> CreateProfileAsync(string fullName);
}