using BillSave.API.Portfolio.Domain.Model.Aggregates;
using BillSave.API.Portfolio.Domain.Repositories;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BillSave.API.Portfolio.Infrastructure.Persistence.EFC.Repositories;

/// Portfolio repository.
/// <summary>
/// This class represents the Portfolio repository. It is used to encapsulate the data access logic of the Portfolio entity.
/// </summary>
/// <param name="context">
/// The <see cref="AppDbContext"/> context
/// </param>
public class PackRepository(AppDbContext context) : BaseRepository<Pack>(context), IPackRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Pack>> FindByUserIdAsync(int userId)
    {
       return await Context.Set<Pack>().Where(p => p.UserId == userId).ToListAsync();
    }
    
    // <inheritdoc />
    public async Task<Pack?> FindByNameAsync(string name)
    {
        return await Context.Set<Pack>().FirstOrDefaultAsync(p => p.Name == name);
    }
    /// <inheritdoc/>
    public Task<bool> ExistsByNameAndUserIdAsync(string name, int userId)
    {
        return Context.Set<Pack>().AnyAsync(p => p.Name == name && p.UserId == userId);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Pack>> FindByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
       return await Context.Set<Pack>()
           .Where(p => p.DiscountDate.Value >= startDate && p.DiscountDate.Value <= endDate).ToListAsync();
    }
}