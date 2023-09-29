using TypeLibrary.Api.Attributes;
using TypeLibrary.Api.Blocks;
using TypeLibrary.Api.Common;
using TypeLibrary.Api.Terminals;
using TypeLibrary.Services.Common;

namespace TypeLibrary.Api;

public static class ApiDependencyInjection
{
    public static IServiceCollection AddDomainToViewMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile(new AttributeViewProfile());
            config.AddProfile(new BlockViewProfile());
            config.AddProfile(new TerminalViewProfile());

            config.AddProfile(new AttributeTypeReferenceViewProfile());
            config.AddProfile(new TerminalTypeReferenceViewProfile());
        });

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUserInformationService, UserInformationService>();

        return services;
    }
}