using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// ReSharper disable InvalidXmlDocComment

namespace Mimirorg.Authentication.Extensions;

public static class HostedServiceExtensions
{
    /// <summary>
    /// Instead of calling service.AddHostedService<T> you call this make sure that you can also access the hosted service by interface TImplementation
    /// https://stackoverflow.com/a/64689263/619465
    /// </summary>
    /// <param name="services">The service collection</param>
    public static void AddInjectableHostedService<TService, TImplementation>(this IServiceCollection services) where TService : class where TImplementation : class, IHostedService, TService
    {
        services.AddSingleton<TImplementation>();
        services.AddSingleton<IHostedService>(provider => provider.GetRequiredService<TImplementation>());
        services.AddSingleton<TService>(provider => provider.GetRequiredService<TImplementation>());
    }
}