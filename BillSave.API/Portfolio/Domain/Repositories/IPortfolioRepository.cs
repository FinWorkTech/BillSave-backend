using BillSave.API.Shared.Domain.Repositories;
using BillSave.API.Portfolio.Domain.Model.Aggregates;

namespace BillSave.API.Portfolio.Domain.Repositories;

/// Pack Repository Interface.
/// <summary>
/// This interface represents the repository for the Pack entity.
/// </summary>
public interface IPackRepository : IBaseRepository<Pack>
{
    /// <summary>
    /// Find Packs by User Id.
    /// </summary>
    /// <param name="userId">
    /// The User Id.
    /// </param>
    /// <returns>
    /// The Packs found.
    /// </returns>
    Task<IEnumerable<Pack>> FindByUserIdAsync(int userId);
    
    /// <summary>
    /// Find Packs by Date.
    /// </summary>
    /// <param name="date">
    /// The Date.
    /// </param>
    /// <returns>
    /// The Packs found.
    /// </returns>
    Task<IEnumerable<Pack>> FindByDateAsync(DateTime date);
}