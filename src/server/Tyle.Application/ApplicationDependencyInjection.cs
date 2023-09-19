using Microsoft.Extensions.DependencyInjection;
using Tyle.Application.Attributes;
using Tyle.Application.Common;

namespace Tyle.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<IAttributeService, AttributeService>();
        services.AddScoped<IReferenceService, ReferenceService>();

        return services;
    }
}
