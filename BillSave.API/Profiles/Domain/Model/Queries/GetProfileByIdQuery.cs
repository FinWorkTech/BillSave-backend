namespace BillSave.API.Profiles.Domain.Model.Queries;

/// <summary>
/// Query to get a profile by its id
/// </summary>
/// <param name="ProfileId">
/// The id of the profile to get
/// </param>
public record GetProfileByIdQuery(int ProfileId);