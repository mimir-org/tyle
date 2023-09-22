using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tyle.Application.Attributes;
using Tyle.Application.Common;
using Tyle.Application.Terminals;
using Tyle.Persistence.Attributes;
using Tyle.Persistence.Common;
using Tyle.Persistence.Terminals;

namespace Tyle.Persistence;

public static class PersistenceDependencyInjection
{
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration config)
    {
        var dbConfigOptions = new DatabaseConfigurationOptions();
        config.GetSection(DatabaseConfigurationOptions.DatabaseConfiguration).Bind(dbConfigOptions);

        if (dbConfigOptions.ConnectionString != null)
        {
            services.AddDbContext<TyleDbContext>(options =>
            {
                options.UseSqlServer(dbConfigOptions.ConnectionString);
            }, ServiceLifetime.Transient);
        }
        else
        {
            services.AddDbContext<TyleDbContext>(options => options.UseInMemoryDatabase("TestDB"), ServiceLifetime.Transient);
        }

        return services;
    }

    public static IServiceCollection AddDaoMapping(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();

        services.AddAutoMapper(config =>
        {
            config.AddProfile(new ClassifierProfile());
            config.AddProfile(new PurposeProfile());

            config.AddProfile(new AttributeProfile());
            config.AddProfile(new PredicateProfile());
            config.AddProfile(new UnitProfile());
            config.AddProfile(new ValueConstraintProfile());

            config.AddProfile(new MediumProfile());
            config.AddProfile(new TerminalProfile(provider.GetService<IAttributeService>()!));
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAttributeRepository, AttributeRepository>();
        services.AddScoped<ITerminalRepository, TerminalRepository>();

        services.AddScoped<IClassifierRepository, ClassifierRepository>();
        services.AddScoped<IMediumRepository, MediumRepository>();
        services.AddScoped<IPredicateRepository, PredicateRepository>();
        services.AddScoped<IPurposeRepository, PurposeRepository>();
        services.AddScoped<IUnitRepository, UnitRepository>();

        return services;
    }
}

public class DatabaseConfigurationOptions
{
    public const string DatabaseConfiguration = "DatabaseConfiguration";

    public string? DataSource { get; set; }
    public int Port { get; set; }
    public string? InitialCatalog { get; set; }
    public string? DbUser { get; set; }
    public string? Password { get; set; }
    public string? ConnectionString => CreateConnectionString();

    private string? CreateConnectionString()
    {
        if (string.IsNullOrWhiteSpace(DataSource) || string.IsNullOrWhiteSpace(InitialCatalog) || string.IsNullOrWhiteSpace(DbUser) || string.IsNullOrWhiteSpace(Password))
        {
            return null;
        }

        var portString = string.Empty;
        if (Port > 0)
        {
            portString = $",{Port}";
        }

        return $@"Data Source={DataSource}{portString};Initial Catalog={InitialCatalog};Integrated Security=False;User ID={DbUser};Password='{Password}';TrustServerCertificate=True;MultipleActiveResultSets=True";
    }
}
