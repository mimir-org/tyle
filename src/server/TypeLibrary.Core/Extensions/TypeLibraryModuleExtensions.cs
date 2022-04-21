using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Factories;
using TypeLibrary.Data.Repositories;
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
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<ISimpleRepository, SimpleRepository>();
            services.AddScoped<IInterfaceRepository, InterfaceRepository>();
            services.AddScoped<INodeRepository, NodeRepository>();
            services.AddScoped<INodeTerminalRepository, NodeTerminalRepository>();
            services.AddScoped<ITerminalRepository, TerminalRepository>();
            services.AddScoped<ITransportRepository, TransportRepository>();
            services.AddScoped<ILibraryTypeItemRepository, LibraryTypeItemRepository>();
            services.AddScoped<IAttributePredefinedRepository, AttributePredefinedRepository>();
            services.AddScoped<IRdsRepository, RdsRepository>();
            services.AddSingleton<IFileRepository, JsonFileRepository>();
            services.AddScoped<IBlobDataRepository, BlobRepository>();
            services.AddScoped<IConditionRepository, AttributeConditionRepository>();
            services.AddScoped<IFormatRepository, AttributeFormatRepository>();
            services.AddScoped<IQualifierRepository, AttributeQualifierRepository>();
            services.AddScoped<ISourceRepository, AttributeSourceRepository>();
            services.AddScoped<IAttributeAspectRepository, AttributeAspectRepository>();
            services.AddScoped<IPurposeRepository, PurposeRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();

            // Dependency Injection - Services
            services.AddScoped<ITerminalService, TerminalService>();
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAttributeService, AttributeService>();
            services.AddScoped<IRdsService, RdsService>();
            services.AddScoped<ISeedingService, SeedingService>();
            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IAttributeConditionService, AttributeConditionService>();
            services.AddScoped<IAttributeFormatService, AttributeFormatService>();
            services.AddScoped<IAttributeQualifierService, AttributeQualifierService>();
            services.AddScoped<IAttributeSourceService, AttributeSourceService>();
            services.AddScoped<IAttributeAspectService, AttributeAspectService>();
            services.AddScoped<IPurposeService, PurposeService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<ICollectionService, CollectionService>();

            // Factories
            services.AddScoped<IUnitFactory, UnitFactory>();
            services.AddScoped<IAttributeFactory, AttributeFactory>();

            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Build configuration
            var config = builder.Build();

            // Add database-configuration
            services.AddDatabaseConfigurations(config);

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