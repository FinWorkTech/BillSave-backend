using BillSave.API.Portfolio.Domain.Model.Aggregates;
using BillSave.API.Portfolio.Domain.Model.Queries;
using BillSave.API.Portfolio.Domain.Model.ValueObjects;

namespace BillSave.API.Portfolio.Domain.Services;

/// <summary>
/// The service to query packs.
/// </summary>
public interface IPackQueryService
{
    /// <summary>
    /// Get a pack by its identifier.
    /// </summary>
    /// <param name="query">
    /// The query to get a pack by its identifier.
    /// </param>
    /// <returns>
    /// The pack.
    /// </returns>
    Task<IEnumerable<Pack>> Handle(GetPackByUserIdQuery query);
    
    /// <summary>
    /// Get a pack by date range.
    /// </summary>
    /// <param name="query">
    /// The query to get a pack by date range.
    /// </param>
    /// <returns>
    /// The pack.
    /// </returns>
    Task<IEnumerable<Pack>> Handle(GetPackByDateRange query);
    
    /// <summary>
    /// Get a pack summary by user id.
    /// </summary>
    /// <param name="query">
    /// The query to get a pack summary by user id.
    /// </param>
    /// <returns>
    /// The pack.
    /// </returns>
    Task<PackSummary> Handle(GetPackSummaryByUserIdQuery query);
}