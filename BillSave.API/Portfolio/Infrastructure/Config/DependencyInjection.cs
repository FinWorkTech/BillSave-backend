using BillSave.API.Portfolio.Interfaces.ACL;
using BillSave.API.Portfolio.Domain.Repositories;
using BillSave.API.Portfolio.Application.ACL.InboundServices;
using BillSave.API.Portfolio.Application.ACL.OutboundServices;
using BillSave.API.Portfolio.Application.Internal.QueryServices;
using BillSave.API.Portfolio.Application.Interfaces.QueryServices;
using BillSave.API.Portfolio.Application.Internal.CommandServices;
using BillSave.API.Portfolio.Application.Interfaces.CommandServices;
using BillSave.API.Portfolio.Infrastructure.Persistence.EFC.Repositories;

namespace BillSave.API.Portfolio.Infrastructure.Config;

/// <summary>
/// The dependency injection configuration for the portfolio bounded context.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddPortfolioServices(this IServiceCollection services)
    {
        services.AddScoped<ExternalSalesService>();
        services.AddScoped<IPackRepository, PackRepository>();
        services.AddScoped<IPackQueryService, PackQueryService>();
        services.AddScoped<IPackCommandService, PackCommandService>();
        services.AddScoped<IPortfoliosContextFacade, PortfoliosContextFacade>();
        
        return services;
    }
}