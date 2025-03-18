using BillSave.API.IAM.Application.ACL.InboundServices;
using BillSave.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using BillSave.API.IAM.Infrastructure.Tokens.JWT.Services;

namespace BillSave.API.IAM.Infrastructure.Config;

public static class TokenDependencyInjection
{
    public static IServiceCollection AddTokenServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IHashingService, HashingService>();

        return services;
    }
}