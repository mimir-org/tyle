using System;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;
using TypeLibrary.Core.Profiles;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Core.Extensions
{
    public static class TypeLibraryServiceCollectionExtensions
    {
        public static IConfigurationBuilder AddConfigurationFiles(this IServiceCollection serviceCollection)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");
            builder.AddJsonFile($"appsettings.{environment}.json", true);
            builder.AddJsonFile("appsettings.local.json", true);
            builder.AddEnvironmentVariables();
            return builder;
        }

        public static IServiceCollection AddAutoMapperConfigurations(this IServiceCollection serviceCollection)
        {
            var provider = serviceCollection.BuildServiceProvider();
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile(new AttributeProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IUnitFactory>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new SymbolProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>(), provider.GetService<IOptions<ApplicationSettings>>()));
            cfg.AddProfile(new NodeProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new InterfaceProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new TransportProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new RdsProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new TerminalProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IAttributeFactory>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new AttributeConditionProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new AttributeFormatProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new AttributeQualifierProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new AttributeSourceProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new AttributeAspectProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new AttributePredefinedProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new PurposeProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new UnitProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new SimpleProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
            cfg.AddProfile(new SelectedAttributePredefinedProfile(provider.GetService<IApplicationSettingsRepository>()));
            cfg.AddProfile(new NodeTerminalProfile());

            var mapperConfig = new MapperConfiguration(cfg);
            serviceCollection.AddSingleton(_ => mapperConfig.CreateMapper());
            return serviceCollection;
        }

        public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection serviceCollection, IConfigurationRoot config)
        {
            var dbConfig = new DatabaseConfiguration();
            var databaseConfigSection = config.GetSection("DatabaseConfiguration");
            databaseConfigSection.Bind(dbConfig);

            serviceCollection.AddSingleton(Options.Create(dbConfig));

            serviceCollection.AddDbContext<TypeLibraryDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(dbConfig.ConnectionString, sqlOptions => sqlOptions.MigrationsAssembly("TypeLibrary.Core"));
            });

            return serviceCollection;
        }

        public static IServiceCollection AddApplicationSettings(this IServiceCollection serviceCollection, IConfigurationRoot config)
        {
            var appSettings = new ApplicationSettings();
            var appSettingsSection = config.GetSection("ApplicationSettings");
            appSettingsSection.Bind(appSettings);

            serviceCollection.AddSingleton(Options.Create(appSettings));

            return serviceCollection;
        }

        public static IServiceCollection AddApiVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = false;
                o.DefaultApiVersion = new ApiVersion(0, 1);
                o.ReportApiVersions = true;
            });

            serviceCollection.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });

            return serviceCollection;
        }
    }
}