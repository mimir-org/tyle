using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;
using System;
using System.IO;
using TypeLibrary.Core.Factories;
using TypeLibrary.Core.Profiles;
using TypeLibrary.Data;
using TypeLibrary.Data.Configurations;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Core.Extensions;

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
        cfg.AddProfile(new AttributeProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
        cfg.AddProfile(new BlockProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>(), provider.GetService<ICompanyFactory>()));
        cfg.AddProfile(new TerminalProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
        cfg.AddProfile(new BlockTerminalProfile());
        cfg.AddProfile(new BlockAttributeProfile());
        cfg.AddProfile(new TerminalAttributeProfile());
        cfg.AddProfile(new ClassifierProfile());
        cfg.AddProfile(new MediumProfile());
        cfg.AddProfile(new PredicateProfile());
        cfg.AddProfile(new PurposeProfile());
        cfg.AddProfile(new UnitProfile());
        cfg.AddProfile(new ValueConstraintProfile());
        cfg.AddProfile(new LogProfile());
        cfg.AddProfile(new SymbolProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>(), provider.GetService<IOptions<ApplicationSettings>>()));

        var mapperConfig = new MapperConfiguration(cfg);
        mapperConfig.AssertConfigurationIsValid();
        serviceCollection.AddSingleton(_ => mapperConfig.CreateMapper());
        return serviceCollection;
    }

    public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection serviceCollection, IConfigurationRoot config)
    {
        var dbConfig = new DatabaseConfiguration();
        var databaseConfigSection = config.GetSection("DatabaseConfiguration");
        databaseConfigSection.Bind(dbConfig);

        serviceCollection.AddSingleton(Options.Create(dbConfig));

        var connectionString = dbConfig.ConnectionString;

        if (connectionString != null)
        {
            serviceCollection.AddDbContext<TypeLibraryDbContext>(options =>
            {
                options.UseSqlServer(dbConfig.ConnectionString,
                    sqlOptions => sqlOptions.MigrationsAssembly("TypeLibrary.Core"));
            }, ServiceLifetime.Transient);
        }
        else
        {
            serviceCollection.AddDbContext<TypeLibraryDbContext>(options => options.UseInMemoryDatabase("TestDB"), ServiceLifetime.Transient);
        }

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