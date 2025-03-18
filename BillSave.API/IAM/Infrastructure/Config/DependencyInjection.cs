namespace BillSave.API.IAM.Infrastructure.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddIamServices(this IServiceCollection services)
    {
        services.AddUserServices();
        services.AddTokenServices();
        
        return services;
    }
}