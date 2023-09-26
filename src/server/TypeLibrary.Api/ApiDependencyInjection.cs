using TypeLibrary.Api.Attributes;

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
}
