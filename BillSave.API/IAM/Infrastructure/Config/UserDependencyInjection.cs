using BillSave.API.IAM.Application.ACL.OutboundServices;
using BillSave.API.IAM.Application.Interfaces.CommandServices;
using BillSave.API.IAM.Application.Interfaces.QueryServices;
using BillSave.API.IAM.Application.Internal.CommandServices;
using BillSave.API.IAM.Application.Internal.QueryServices;
using BillSave.API.IAM.Domain.Repositories;
using BillSave.API.IAM.Infrastructure.Persistence.EFC.Repositories;

namespace BillSave.API.IAM.Infrastructure.Config;

public static class UserDependencyInjection
{
    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IUserCommandService, UserCommandService>();

        services.AddScoped<ExternalProfileService>();

        return services;
    }
}