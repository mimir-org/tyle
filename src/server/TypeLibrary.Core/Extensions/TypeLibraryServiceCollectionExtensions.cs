using System;
using System.IO;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mimirorg.Common.Models;
using TypeLibrary.Core.Profiles;
using TypeLibrary.Data;

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
            var cfg = new MapperConfigurationExpression();
            cfg.AddProfile(new AttributeProfile());
            cfg.AddProfile<BlobProfile>();
            cfg.AddProfile(new LibraryTypeProfile());
            cfg.AddProfile<RdsProfile>();
            cfg.AddProfile(new TerminalProfile());
            cfg.AddProfile(new AttributeConditionProfile());
            cfg.AddProfile(new AttributeFormatProfile());
            cfg.AddProfile(new AttributeQualifierProfile());
            cfg.AddProfile(new AttributeSourceProfile());
            cfg.AddProfile(new AttributeAspectProfile());
            cfg.AddProfile(new PurposeProfile());
            cfg.AddProfile(new RdsCategoryProfile());
            cfg.AddProfile(new UnitProfile());
            cfg.AddProfile(new SimpleProfile());
            cfg.AddProfile(new AttributeCollectionProfile());

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