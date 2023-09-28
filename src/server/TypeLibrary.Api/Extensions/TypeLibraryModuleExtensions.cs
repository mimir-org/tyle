using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TypeLibrary.Data;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace TypeLibrary.Api.Extensions;

public static class TypeLibraryModuleExtensions
{
    public static IServiceCollection AddTypeLibraryModule(this IServiceCollection services, IConfiguration configuration)
    {
        // Add configurations files
        var builder = services.AddConfigurationFiles();

        // Cache
        services.AddMemoryCache();
        //services.AddSingleton<ICacheRepository, InMemoryCacheRepository>();

        // Common
        //services.AddSingleton<ISparQlWebClient, SparQlWebClient>();

        // Dependency Injection - Repositories
        //services.AddSingleton<IApplicationSettingsRepository, ApplicationSettingsRepository>();
        //services.AddScoped<IEfBlockRepository, EfBlockRepository>();
        //services.AddScoped<IEfTerminalRepository, EfTerminalRepository>();
        //services.AddScoped<IEfSymbolRepository, EfSymbolRepository>();
        //services.AddScoped<IDynamicSymbolDataProvider, EfSymbolRepository>();
        //services.AddScoped<IEfLogRepository, EfLogRepository>();
        //services.AddScoped<IEfAttributeRepository, EfAttributeRepository>();
        //services.AddScoped<IEfBlockTerminalRepository, EfBlockTerminalRepository>();
        //services.AddScoped<IEfBlockAttributeRepository, EfBlockAttributeRepository>();
        //services.AddScoped<IEfTerminalAttributeRepository, EfTerminalAttributeRepository>();
        //services.AddScoped<IEfClassifierRepository, EfClassifierRepository>();
        //services.AddScoped<IEfMediumRepository, EfMediumRepository>();
        //services.AddScoped<IEfPredicateRepository, EfPredicateRepository>();
        //services.AddScoped<IEfPurposeRepository, EfPurposeRepository>();
        //services.AddScoped<IEfUnitRepository, EfUnitRepository>();
        //services.AddScoped<IEfBlockClassifierRepository, EfBlockClassifierRepository>();
        //services.AddScoped<IEfTerminalClassifierRepository, EfTerminalClassifierRepository>();
        //services.AddScoped<IEfAttributeUnitRepository, EfAttributeUnitRepository>();

        //services.AddScoped<IAttributeRepository, EfAttributeRepository>();
        //services.AddScoped<IBlockRepository, EfBlockRepository>();
        //services.AddScoped<ITerminalRepository, EfTerminalRepository>();
        //services.AddScoped<ISymbolRepository, EfSymbolRepository>();
        //services.AddScoped<IValueConstraintRepository, EfValueConstraintRepository>();
        //services.AddScoped<ILogRepository, EfLogRepository>();

        // Dependency Injection - Services
        //services.AddScoped<ITerminalService, TerminalService>();
        //services.AddScoped<IAttributeService, AttributeService>();
        //services.AddScoped<ISeedingService, SeedingService>();
        //services.AddScoped<ISymbolService, SymbolService>();
        //services.AddScoped<IBlockService, BlockService>();
        //services.AddScoped<IModuleService, ModuleService>();
        //services.AddScoped<ILogService, LogService>();
        ////services.AddScoped<IApprovalService, ApprovalService>();
        //services.AddScoped<IEmailService, EmailService>();
        //services.AddScoped<IReferenceService, ReferenceService>();

        // Hosted services
        //services.AddHostedService<TimedPcaSyncingService>();

        // Factories
        //services.AddScoped<ICompanyFactory, CompanyFactory>();

        services.AddDatabaseConfiguration(configuration)
            .AddRequestToDomainMapping()
            .AddRepositories();

        services.AddHttpContextAccessor();
        services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        // Build configuration
        //var config = builder.Build();

        // Add database-configuration
        //services.AddDatabaseConfigurations(config);

        // Add application-settings
        //services.AddApplicationSettings(config);

        // Add auto-mapper configurations
        //services.AddAutoMapperConfigurations();

        // Add API version
        services.AddApiVersion();

        // Add Application Insights
        //services.AddApplicationInsightsTelemetry();

        // Add authentication

        // Add swagger documentation
        return services;
    }

    public static IApplicationBuilder UseTypeLibraryModule(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        //var context = serviceScope.ServiceProvider.GetRequiredService<TypeLibraryDbContext>();
        //var seedingService = serviceScope.ServiceProvider.GetRequiredService<ISeedingService>();
        //var seedingServiceLogger = serviceScope.ServiceProvider.GetRequiredService<ILogger<ISeedingService>>();
        //var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<IModuleService>>();

        //var applicationSettings = serviceScope.ServiceProvider.GetRequiredService<IOptions<ApplicationSettings>>();
        //logger.LogInformation(applicationSettings?.Value.ToString());

        //var databaseConfigurations = serviceScope.ServiceProvider.GetRequiredService<IOptions<DatabaseConfiguration>>();
        //logger.LogInformation(databaseConfigurations?.Value.ToString());

        //var authSettings = serviceScope.ServiceProvider.GetRequiredService<IOptions<MimirorgAuthSettings>>();
        //logger.LogInformation(authSettings?.Value.ToString());

        //if (RelationalDatabaseFacadeExtensions.IsRelational(context.Database))
        //    RelationalDatabaseFacadeExtensions.Migrate(context.Database);

        /*var awaiter = seedingService.LoadDataFromFiles().ConfigureAwait(true).GetAwaiter();
        while (!awaiter.IsCompleted)
        {
            seedingServiceLogger.LogInformation("Starting initialize db");
            Thread.Sleep(2000);
        }*/
        return app;
    }
}