namespace BillSave.API.Profiles.Domain.Model.Commands;

/// <summary>
/// Command to create a new profile
/// </summary>
/// <param name="FullName">
/// The full name of the profile
/// </param>
public record CreateProfileCommand(string FullName);