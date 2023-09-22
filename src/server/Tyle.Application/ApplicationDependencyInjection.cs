using Microsoft.Extensions.DependencyInjection;
using Tyle.Application.Attributes;
using Tyle.Application.Common;
using Tyle.Application.Terminals;

namespace Tyle.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAttributeService, AttributeService>();
        services.AddScoped<IReferenceService, ReferenceService>();
        services.AddScoped<ITerminalService, TerminalService>();

        return services;
    }
}
