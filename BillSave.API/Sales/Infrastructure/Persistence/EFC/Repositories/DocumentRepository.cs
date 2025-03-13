using BillSave.API.Sales.Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace BillSave.API.Sales.Infrastructure.Persistence.EFC.Repositories;

/// Document repository.
/// <summary>
/// The <see cref="IDocumentRepository"/> repository.
/// </summary>
/// <param name="context">
/// The <see cref="AppDbContext"/> context
/// </param>
public class DocumentRepository(AppDbContext context) : BaseRepository<Document>(context), IDocumentRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Document>> FindByPortfolioIdAsync(int portfolioId)
    {
        return await Context.Set<Document>()
            .Where(d => d.PortfolioId == portfolioId).ToListAsync();
    }

    public async Task<IEnumerable<Document>> FindByPortfolioIdAndDateRangeAsync(int portfolioId, DateTime startDate, DateTime endDate)
    {
        return await Context.Set<Document>()
            .Where(d => d.PortfolioId == portfolioId && 
                        d.IssueDate.Value >= startDate && d.IssueDate.Value <= endDate).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Document>> FindByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await Context.Set<Document>()
            .Where(d => d.IssueDate.Value >= startDate && d.IssueDate.Value <= endDate).ToListAsync();
    }

    public async Task<IEnumerable<Document>> FinByRateTypeAsync(string rateType)
    {
        return await Context.Set<Document>()
            .Where(d => d.Rate.Type == rateType).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsByCodeAndPortfolioIdAsync(string code, int portfolioId)
    {
        return await Context.Set<Document>().AnyAsync(d => d.Code.Value == code && d.PortfolioId == portfolioId);
    }
    
    /// <inheritdoc />
    public async Task DeleteDocumentsByPortfolioIdAsync(int portfolioId)
    {
        await Context.Set<Document>().Where(d => d.PortfolioId == portfolioId).ExecuteDeleteAsync();
    }
}