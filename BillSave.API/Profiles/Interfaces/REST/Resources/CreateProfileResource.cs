namespace BillSave.API.Profiles.Interfaces.REST.Resources;

/// <summary>
/// Create profile resource.
/// </summary>
/// <remarks>
/// This resource is used to create a new profile. 
/// </remarks>
/// <param name="FullName">
/// The full name of the profile.
/// </param>
public record CreateProfileResource(string FullName);