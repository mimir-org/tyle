using Microsoft.Extensions.DependencyInjection;
using Tyle.Application.Attributes;
using Tyle.Application.Blocks;
using Tyle.Application.Common;
using Tyle.Application.Terminals;

namespace Tyle.Converters;

public static class ConvertersDependencyInjection
{
    public static IServiceCollection AddConversionServices(this IServiceCollection services)
    {
        services.AddScoped<IAttributeTypeConverter, AttributeTypeConverter>();
        services.AddScoped<ITerminalTypeConverter, TerminalTypeConverter>();
        services.AddScoped<IBlockTypeConverter, BlockTypeConverter>();
        services.AddScoped<IJsonLdConversionService, JsonLdConversionService>();

        return services;
    }
}