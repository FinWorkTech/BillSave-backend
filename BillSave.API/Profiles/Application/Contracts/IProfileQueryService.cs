using BillSave.API.Profiles.Domain.Model.Aggregates;
using BillSave.API.Profiles.Domain.Model.Queries;

namespace BillSave.API.Profiles.Application.Contracts;

/// <summary>
/// Profile query service interface.
/// </summary>
public interface IProfileQueryService
{
    /// <summary>
    /// Handle the GetProfileByIdQuery.
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetProfileByIdQuery"/> query.
    /// </param>
    /// <returns>
    /// The <see cref="Profile"/> object if found; otherwise, null.
    /// </returns>
    Task<Profile?> Handle(GetProfileByIdQuery query);
}