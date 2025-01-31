using BillSave.API.Profiles.Domain.Model.Aggregates;
using BillSave.API.Profiles.Domain.Model.Queries;
using BillSave.API.Profiles.Domain.Repositories;
using BillSave.API.Profiles.Domain.Services;

namespace BillSave.API.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository profileRepository)
    : IProfileQueryService
{
    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }
}