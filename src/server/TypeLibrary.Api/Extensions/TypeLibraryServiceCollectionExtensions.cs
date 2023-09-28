using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace TypeLibrary.Api.Extensions;

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

    /*public static IServiceCollection AddAutoMapperConfigurations(this IServiceCollection serviceCollection)
    {
        var provider = serviceCollection.BuildServiceProvider();
        var cfg = new MapperConfigurationExpression();
        cfg.AddProfile(new AttributeProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
        cfg.AddProfile(new BlockProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>(), provider.GetService<ICompanyFactory>()));
        cfg.AddProfile(new TerminalProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>()));
        cfg.AddProfile(new BlockTerminalProfile());
        cfg.AddProfile(new BlockAttributeProfile());
        cfg.AddProfile(new TerminalAttributeProfile());
        cfg.AddProfile(new ValueConstraintProfile());
        cfg.AddProfile(new LogProfile());
        cfg.AddProfile(new SymbolProfile(provider.GetService<IApplicationSettingsRepository>(), provider.GetService<IHttpContextAccessor>(), provider.GetService<IOptions<ApplicationSettings>>()));

        var mapperConfig = new MapperConfiguration(cfg);
        mapperConfig.AssertConfigurationIsValid();
        serviceCollection.AddSingleton(_ => mapperConfig.CreateMapper());
        return serviceCollection;
    }*/

    /*public static IServiceCollection AddDatabaseConfigurations(this IServiceCollection serviceCollection, IConfigurationRoot config)
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
    }*/

    /*public static IServiceCollection AddApplicationSettings(this IServiceCollection serviceCollection, IConfigurationRoot config)
    {
        var appSettings = new ApplicationSettings();
        var appSettingsSection = config.GetSection("ApplicationSettings");
        appSettingsSection.Bind(appSettings);

        serviceCollection.AddSingleton(Options.Create(appSettings));

        return serviceCollection;
    }*/

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

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection serviceCollection, IConfiguration config)
    {
        // Swagger configurations
        var swaggerConfigurationSection = config.GetSection(nameof(SwaggerConfiguration));
        var swaggerConfiguration = new SwaggerConfiguration();
        swaggerConfigurationSection.Bind(swaggerConfiguration);
        serviceCollection.Configure<SwaggerConfiguration>(swaggerConfigurationSection.Bind);

        serviceCollection.AddSwaggerGen(c =>
        {
            var provider = serviceCollection.BuildServiceProvider();
            var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in service.ApiVersionDescriptions)
            {
                c.SwaggerDoc(description.GroupName,
                    new OpenApiInfo
                    {
                        Title = swaggerConfiguration.Title,
                        Version = description.ApiVersion.ToString(),
                        Description = swaggerConfiguration.Description,
                        Contact = new OpenApiContact { Name = swaggerConfiguration.Contact?.Name, Email = swaggerConfiguration.Contact?.Email }
                    });
            }

            var xmlPath = Path.Combine(AppContext.BaseDirectory, "swagger.xml");

            c.IncludeXmlComments(xmlPath, true);
            c.CustomSchemaIds(x => x.FullName);
            c.EnableAnnotations();

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        serviceCollection.AddSwaggerGenNewtonsoftSupport();

        return serviceCollection;
    }
}