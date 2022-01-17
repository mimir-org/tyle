using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts;
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

            // Dependency Injection
            services.AddScoped<IAttributeTypeRepository, AttributeTypeRepository>();
            services.AddScoped<ISimpleTypeRepository, SimpleTypeRepository>();
            services.AddScoped<IInterfaceTypeRepository, InterfaceTypeRepository>();
            services.AddScoped<ILibraryTypeRepository, LibraryTypeRepository>();
            services.AddScoped<INodeTypeRepository, NodeTypeRepository>();
            services.AddScoped<INodeTypeTerminalTypeRepository, NodeTypeTerminalTypeRepository>();
            services.AddScoped<ITerminalTypeRepository, TerminalTypeRepository>();
            services.AddScoped<ITransportTypeRepository, TransportTypeRepository>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IPredefinedAttributeRepository, PredefinedAttributeRepository>();
            services.AddScoped<IRdsRepository, RdsRepository>();
            services.AddSingleton<IFileRepository, JsonFileRepository>();
            services.AddScoped<IBlobDataRepository, BlobDataRepository>();
            services.AddScoped<ITerminalTypeService, TerminalTypeService>();
            services.AddScoped<ILibraryTypeService, LibraryTypeService>();
            services.AddScoped<ILibraryTypeFileService, LibraryTypeFileService>();
            services.AddScoped<IAttributeTypeService, AttributeTypeService>();
            services.AddScoped<IRdsService, RdsService>();
            services.AddScoped<ISeedingService, SeedingService>();
            services.AddScoped<IBlobDataService, BlobDataService>();
            services.AddHttpContextAccessor();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Add auto-mapper configurations
            services.AddAutoMapperConfigurations();

            // Build configuration
            var config = builder.Build();

            // Add database-configuration
            services.AddDatabaseConfigurations(config);

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