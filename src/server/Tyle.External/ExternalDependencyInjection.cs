using Microsoft.Extensions.DependencyInjection;

namespace Tyle.External;

public static class ExternalDependencyInjection
{
    public static IServiceCollection AddSyncingServices(this IServiceCollection services)
    {
        services.AddHostedService<CommonLibSyncingService>();

        return services;
    }
}