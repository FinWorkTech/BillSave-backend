using BillSave.API.Profiles.Domain.Model.Aggregates;
using BillSave.API.Profiles.Domain.Repositories;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BillSave.API.Profiles.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// Profile repository implementation.
/// </summary>
/// <param name="context">
/// The database context.
/// </param>
public class ProfileRepository(AppDbContext context)
    : BaseRepository<Profile>(context), IProfileRepository
{
    /// <inheritdoc/>
    public Task<Profile?> FindProfileByIdAsync(int id)
    {
        return Context.Set<Profile>().FirstOrDefaultAsync(p => p.Id == id);
    }
}