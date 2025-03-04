using BillSave.API.Portfolio.Domain.Model.Aggregates;
using BillSave.API.Portfolio.Domain.Model.Queries;
using BillSave.API.Portfolio.Domain.Model.ValueObjects;
using BillSave.API.Portfolio.Domain.Repositories;
using BillSave.API.Portfolio.Domain.Services;

namespace BillSave.API.Portfolio.Application.Internal.QueryServices;

/// <summary>
/// The pack query service.
/// </summary>
/// <param name="packRepository">
/// The <see cref="IPackRepository"/> repository.
/// </param>
public class PackQueryService(IPackRepository packRepository) : IPackQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Pack>> Handle(GetPackByUserIdQuery query)
    {
        return await packRepository.FindByUserIdAsync(query.UserId);
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<Pack>> Handle(GetPackByDateRange query)
    {
        return await packRepository.FindByDateRangeAsync(query.StartDate, query.EndDate);
    }
    
    // <inheritdoc />
    public async Task<Pack?> Handle(GetPackByNameQuery query)
    {
        return await packRepository.FindByNameAsync(query.Name);
    }

    public async Task<PackSummary> Handle(GetPackSummaryByUserIdQuery query)
    {
        var packs = await packRepository.FindByUserIdAsync(query.UserId);

        var activePacks = packs.Count();
        var totalDocuments = packs.Sum(p => p.TotalDocuments);
        var averageEffectiveAnnualCostRate = packs.Average(p => p.EffectiveAnnualCostRate.Value);

        return new PackSummary(activePacks, totalDocuments, averageEffectiveAnnualCostRate);
    }
}