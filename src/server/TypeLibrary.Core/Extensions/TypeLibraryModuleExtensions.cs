using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Mimirorg.Common.Abstract;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Factories;
using TypeLibrary.Data.Repositories;
using TypeLibrary.Data.Repositories.Ef;
using TypeLibrary.Services.Contracts;
using TypeLibrary.Services.Services;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace TypeLibrary.Core.Extensions
{
    public static class TypeLibraryModuleExtensions
    {
        public static IServiceCollection AddTypeLibraryModule(this IServiceCollection services, IConfiguration configuration)
        {
            // Add configurations files
            var builder = services.AddConfigurationFiles();

            // Dependency Injection - Repositories
            services.AddSingleton<IApplicationSettingsRepository, ApplicationSettingsRepository>();
            services.AddScoped<IEfAttributeRepository, EfAttributeRepository>();
            services.AddScoped<IEfSimpleRepository, EfSimpleRepository>();
            services.AddScoped<IEfInterfaceRepository, EfInterfaceRepository>();
            services.AddScoped<IEfNodeRepository, EfNodeRepository>();
            services.AddScoped<IEfNodeTerminalRepository, EfNodeTerminalRepository>();
            services.AddScoped<IEfTerminalRepository, EfTerminalRepository>();
            services.AddScoped<IEfTransportRepository, EfTransportRepository>();
            services.AddScoped<ILibraryTypeItemRepository, LibraryTypeItemRepository>();
            services.AddScoped<IEfAttributePredefinedRepository, EfAttributePredefinedRepository>();
            services.AddScoped<IEfRdsRepository, EfRdsRepository>();
            services.AddSingleton<IFileRepository, JsonFileRepository>();
            services.AddScoped<IEfSymbolRepository, EfSymbolRepository>();
            services.AddScoped<IEfAttributeConditionRepository, EfAttributeConditionRepository>();
            services.AddScoped<IEfAttributeFormatRepository, EfAttributeFormatRepository>();
            services.AddScoped<IEfAttributeQualifierRepository, EfAttributeQualifierRepository>();
            services.AddScoped<IEfAttributeSourceRepository, EfAttributeSourceRepository>();
            services.AddScoped<IEfAttributeAspectRepository, EfAttributeAspectRepository>();
            services.AddScoped<IEfPurposeRepository, EfPurposeRepository>();
            services.AddScoped<IEfUnitRepository, EfUnitRepository>();
            services.AddScoped<IDynamicImageDataProvider, EfSymbolRepository>();

            services.AddScoped<IAttributeRepository, DbAttributeRepository>();
            services.AddScoped<IInterfaceRepository, DbInterfaceRepository>();
            services.AddScoped<ITransportRepository, DbTransportRepository>();
            services.AddScoped<INodeRepository, DbNodeRepository>();
            services.AddScoped<IPurposeRepository, DbPurposeRepository>();
            services.AddScoped<IRdsRepository, DbRdsRepository>();
            services.AddScoped<ISimpleRepository, DbSimpleRepository>();
            services.AddScoped<ISymbolRepository, DbSymbolRepository>();

            // Dependency Injection - Services
            services.AddScoped<ITerminalService, TerminalService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAttributeService, AttributeService>();
            services.AddScoped<IRdsService, RdsService>();
            services.AddScoped<ISeedingService, SeedingService>();
            services.AddScoped<ISymbolService, SymbolService>();
            services.AddScoped<IPurposeService, PurposeService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<ITransportService, TransportService>();
            services.AddScoped<INodeService, NodeService>();
            services.AddScoped<IInterfaceService, InterfaceService>();
            services.AddScoped<ISimpleService, SimpleService>();
            services.AddScoped<IVersionService, VersionService>();

            // Factories
            services.AddScoped<IUnitFactory, UnitFactory>();
            services.AddScoped<IAttributeFactory, AttributeFactory>();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Build configuration
            var config = builder.Build();

            // Add database-configuration
            services.AddDatabaseConfigurations(config);

            // Add application-settings
            services.AddApplicationSettings(config);

            // Add auto-mapper configurations
            services.AddAutoMapperConfigurations();

            // Add API version
            services.AddApiVersion();

            // Add authentication

            // Add swagger documentation
            return services;
        }

        public static IApplicationBuilder UseTypeLibraryModule(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<TypeLibraryDbContext>();
            var seedingService = serviceScope.ServiceProvider.GetRequiredService<ISeedingService>();
            var seedingServiceLogger = serviceScope.ServiceProvider.GetRequiredService<ILogger<ISeedingService>>();
            context.Database.Migrate();


            var awaiter = seedingService.LoadDataFromFiles().ConfigureAwait(true).GetAwaiter();
            while (!awaiter.IsCompleted)
            {
                seedingServiceLogger.LogInformation("Starting initialize db");
                Thread.Sleep(2000);
            }
            return app;
        }
    }
}