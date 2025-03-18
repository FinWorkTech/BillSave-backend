using BillSave.API.Portfolio.Application.ACL.OutboundServices;
using BillSave.API.Profiles.Application.ACL.InboundServices;
using BillSave.API.Profiles.Application.Contracts;
using BillSave.API.Profiles.Application.Internal.CommandServices;
using BillSave.API.Profiles.Application.Internal.QueryServices;
using BillSave.API.Profiles.Domain.Repositories;
using BillSave.API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using BillSave.API.Profiles.Interfaces.ACL;

namespace BillSave.API.Profiles.Infrastructure.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddSProfilesServices(this IServiceCollection services)
    {
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IProfileCommandService, ProfileCommandService>();
        services.AddScoped<IProfileQueryService, ProfileQueryService>();
        services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();
        services.AddScoped<ExternalSalesService>();

        return services;
    }
}