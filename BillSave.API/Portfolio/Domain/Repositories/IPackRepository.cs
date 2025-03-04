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
    /// Find Packs by Name.
    /// </summary>
    /// <param name="name">
    ///     The name.
    /// </param>
    /// <returns>
    /// The Packs found.
    /// </returns>
    Task<Pack?> FindByNameAsync(string name);
    
    /// <summary>
    /// Find Packs by Name and User Id.
    /// </summary>
    /// <param name="name">
    /// The username.
    /// </param>
    /// <param name="userId">
    /// The username.
    /// </param>
    /// <returns>
    /// The Packs found.
    /// </returns>
    Task<bool> ExistsByNameAndUserIdAsync(string name, int userId);
    
    /// <summary>
    /// Find Packs by Date Range.
    /// </summary>
    /// <param name="startDate">
    /// The start date.
    /// </param>
    /// <param name="endDate">
    /// The end date.
    /// </param>
    /// <returns>
    /// The Packs found.
    /// </returns>
    Task<IEnumerable<Pack>> FindByDateRangeAsync(DateTime startDate, DateTime endDate);
}