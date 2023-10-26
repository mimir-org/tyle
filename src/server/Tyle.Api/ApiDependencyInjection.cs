using Tyle.Api.Attributes;
using Tyle.Api.Blocks;
using Tyle.Api.Common;
using Tyle.Api.Terminals;
using Tyle.Application.Common;

namespace Tyle.Api;

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

            config.AddProfile(new AttributeGroupViewProfile());
        });

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IUserInformationService, UserInformationService>();

        return services;
    }
}