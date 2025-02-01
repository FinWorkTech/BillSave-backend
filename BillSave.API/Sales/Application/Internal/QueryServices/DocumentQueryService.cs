using System.Collections;
using BillSave.API.Sales.Domain.Model.Aggregates;
using BillSave.API.Sales.Domain.Model.Queries;
using BillSave.API.Sales.Domain.Repositories;
using BillSave.API.Sales.Domain.Services;

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
        return await documentRepository.FindByDateRangeAsync(query.StartDate.Value, query.EndDate.Value);
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
}