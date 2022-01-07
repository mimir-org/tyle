using System;
using System.IO;
using System.Threading;
using Mb.Models.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TypeLibrary.Core.Models;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Repositories;
using TypeLibrary.Services.Contracts;
using TypeLibrary.Services.Services;

namespace TypeLibrary.Core.Extensions
{
    public static class TypeLibraryModuleExtensions
    {
        public static IServiceCollection AddTypeLibraryModule(this IServiceCollection services, IConfiguration configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");
            builder.AddJsonFile($"appsettings.{environment}.json", true);
            builder.AddJsonFile("appsettings.local.json", true);
            builder.AddEnvironmentVariables();

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
            services.AddScoped<IEnumBaseRepository, EnumBaseRepository>();
            services.AddScoped<IRdsRepository, RdsRepository>();
            services.AddSingleton<IFileRepository, JsonFileRepository>();
            services.AddScoped<IBlobDataRepository, BlobDataRepository>();
            services.AddScoped<ITerminalTypeService, TerminalTypeService>();
            services.AddScoped<ILibraryTypeService, LibraryTypeService>();
            services.AddScoped<ILibraryTypeFileService, LibraryTypeFileService>();
            services.AddScoped<IAttributeTypeService, AttributeTypeService>();
            services.AddScoped<IEnumService, EnumService>();
            services.AddScoped<IRdsService, RdsService>();
            services.AddScoped<ISeedingService, SeedingService>();
            services.AddScoped<IBlobDataService, BlobDataService>();

            //Database configuration
            var config = builder.Build();

            var dbConfig = new DatabaseConfiguration();
            var databaseConfigSection = config.GetSection("DatabaseConfiguration");
            databaseConfigSection.Bind(dbConfig);

            var dataSource = Environment.GetEnvironmentVariable("DatabaseConfiguration_DataSource");
            var port = Environment.GetEnvironmentVariable("DatabaseConfiguration_Port");
            var initialCatalog = Environment.GetEnvironmentVariable("DatabaseConfiguration_InitialCatalog");
            var dbUser = Environment.GetEnvironmentVariable("DatabaseConfiguration_DbUser");
            var password = Environment.GetEnvironmentVariable("DatabaseConfiguration_Password");

            if (!string.IsNullOrEmpty(dataSource))
                dbConfig.DataSource = dataSource.Trim();

            if (!string.IsNullOrEmpty(port) && int.TryParse(port.Trim(), out var portAsInt))
                dbConfig.Port = portAsInt;

            if (!string.IsNullOrEmpty(initialCatalog))
                dbConfig.InitialCatalog = initialCatalog.Trim();

            if (!string.IsNullOrEmpty(dbUser))
                dbConfig.DbUser = dbUser.Trim();

            if (!string.IsNullOrEmpty(password))
                dbConfig.Password = password.Trim();

            services.AddSingleton(Options.Create(dbConfig));

            var connectionString = $@"Data Source={dbConfig.DataSource},{dbConfig.Port};Initial Catalog={dbConfig.InitialCatalog};Integrated Security=False;User ID={dbConfig.DbUser};Password='{dbConfig.Password}';TrustServerCertificate=True;MultipleActiveResultSets=True";

            services.AddDbContext<TypeLibraryDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly("TypeLibrary.Core"));
            });


            return services;
        }

        public static IApplicationBuilder UseTypeLibraryModule(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<ModelBuilderDbContext>();
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
