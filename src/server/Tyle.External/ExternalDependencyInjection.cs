using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Tyle.External;

public static class ExternalDependencyInjection
{
    public static IServiceCollection AddSyncingServices(this IServiceCollection services, IConfiguration config)
    {
        if (config.GetValue<bool>("UseCommonLib"))
        {
            services.AddHostedService<CommonLibSyncingService>();
        }

        return services;
    }
}