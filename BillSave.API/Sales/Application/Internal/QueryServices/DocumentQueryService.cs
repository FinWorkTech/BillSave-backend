using BillSave.API.Sales.Domain.Model.Queries;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Contracts.Services;
using BillSave.API.Sales.Domain.Contracts.Repositories;

namespace BillSave.API.Sales.Application.Internal.QueryServices;

/// Document Query Service.
/// <summary>
/// The <see cref="IDocumentQueryService"/> service.
/// </summary>
/// <param name="documentRepository">
/// The <see cref="IDocumentRepository"/> repository.
/// </param>
public class DocumentQueryService(IDocumentRepository documentRepository) : IDocumentQueryService
{
    /// <inheritdoc />
    public async Task<IEnumerable<Document>> Handle(GetDocumentByDateRangeQuery query)
    {
        return await documentRepository.FindByDateRangeAsync(query.StartDate, query.EndDate);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Document>> Handle(GetDocumentByPortfolioIdQuery query)
    {
        return await documentRepository.FindByPortfolioIdAsync(query.PortfolioId);
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<Document>> Handle(GetDocumentByRateTypeQuery query)
    {
        return await documentRepository.FinByRateTypeAsync(query.RateType);
    }
    
    /// <inheritdoc />
    public async Task<Document?> Handle(GetDocumentByIdQuery query)
    {
        return await documentRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Document>> Handle(GetDocumentByPortfolioIdAndDateRangeQuery query)
    {
        return await documentRepository.
            FindByPortfolioIdAndDateRangeAsync(query.PortfolioId, query.StartDate, query.EndDate);
    }
}