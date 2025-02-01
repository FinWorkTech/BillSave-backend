using BillSave.API.Portfolio.Domain.Model.Aggregates;
using BillSave.API.Portfolio.Domain.Model.Queries;
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
}