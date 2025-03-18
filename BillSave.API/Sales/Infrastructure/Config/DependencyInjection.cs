using BillSave.API.Sales.Interfaces.ACL;
using BillSave.API.Sales.Domain.Services;
using BillSave.API.Sales.Domain.Repositories;
using BillSave.API.Sales.Application.ACL.InboundServices;
using BillSave.API.Sales.Application.ACL.OutboundServices;
using BillSave.API.Sales.Application.Internal.QueryServices;
using BillSave.API.Sales.Application.Internal.CommandServices;
using BillSave.API.Sales.Application.Interfaces.QueryServices;
using BillSave.API.Sales.Application.Interfaces.CommandServices;
using BillSave.API.Sales.Infrastructure.Persistence.EFC.Repositories;

namespace BillSave.API.Sales.Infrastructure.Config;

/// <summary>
/// The dependency injection configuration for the sales bounded context.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddSalesServices(this IServiceCollection services)
    {
        services.AddScoped<ExternalPortfolioService>();
        services.AddScoped<ISalesContextFacade, SalesContextFacade>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IDocumentQueryService, DocumentQueryService>();
        services.AddScoped<IDocumentCommandService, DocumentCommandService>();
        services.AddScoped<IDocumentEacrCalculationService, DocumentEacrCalculationService>();
        services.AddScoped<IPortfolioEacrCalculationService, PortfolioEacrCalculationService>();
        
        return services;
    }
}