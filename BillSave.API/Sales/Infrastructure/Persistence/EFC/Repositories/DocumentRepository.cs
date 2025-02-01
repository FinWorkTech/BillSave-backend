using Microsoft.EntityFrameworkCore;
using BillSave.API.Sales.Domain.Repositories;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BillSave.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace BillSave.API.Sales.Infrastructure.Persistence.EFC.Repositories;

/// Document repository.
/// <summary>
/// The <see cref="IDocumentRepository"/> repository.
/// </summary>
/// <param name="context">
/// The <see cref="AppDbContext"/> context
/// </param>
public class DocumentRepository(AppDbContext context) : BaseRepository<Document>(context),IDocumentRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Document>> FindByPortfolioIdAsync(int portfolioId)
    {
        return await Context.Set<Document>()
            .Where(d => d.PortfolioId == portfolioId).ToListAsync();
    }
    
    /// <inheritdoc />
    public async Task<bool> ExistsByCode(string code)
    {
        return await Context.Set<Document>().AnyAsync(d => d.Code == code);
    }
}