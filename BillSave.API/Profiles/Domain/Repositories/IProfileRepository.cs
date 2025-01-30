using BillSave.API.Shared.Domain.Repositories;
using BillSave.API.Profiles.Domain.Model.Aggregates;

namespace BillSave.API.Profiles.Domain.Repositories;

/// <summary>
/// Profile repository interface.
/// </summary>
/// <remarks>
/// This interface defines the methods that are used to manage the Profile information in the database.
/// </remarks>
public interface IProfileRepository : IBaseRepository<Profile>
{
    /// <summary>
    /// Find a profile by id.
    /// </summary>
    /// <param name="id">
    /// The profile id.
    /// </param>
    /// <returns>
    /// The profile with the given id.
    /// </returns>
    Task<Profile?> FindProfileByIdAsync(int id);
}