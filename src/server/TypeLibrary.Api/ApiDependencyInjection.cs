using TypeLibrary.Api.Attributes;
using TypeLibrary.Api.Common;
using TypeLibrary.Services.Common;

namespace TypeLibrary.Api;

public static class ApiDependencyInjection
{
    public static IServiceCollection AddDomainToViewMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AttributeTypeViewProfile());
        });

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUserInformationService, UserInformationService>();

        return services;
    }
}
