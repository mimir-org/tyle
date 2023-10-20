using Microsoft.Extensions.DependencyInjection;
using Tyle.Application.Common;

namespace Tyle.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IApprovalService, ApprovalService>();

        return services;
    }
}
