using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Secretaria.Shared.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpContextAccessor();

        return services;
    }
}
