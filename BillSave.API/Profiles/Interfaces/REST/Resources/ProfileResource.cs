namespace BillSave.API.Profiles.Interfaces.REST.Resources;

/// <summary>
/// Profile resource.
/// </summary>
/// <remarks>
/// This resource is used to represent a profile.
/// </remarks>
/// <param name="Id">
/// The profile identifier.
/// </param>
/// <param name="FullName">
/// The full name of the profile.
/// </param>
public record ProfileResource(int Id, string FullName);