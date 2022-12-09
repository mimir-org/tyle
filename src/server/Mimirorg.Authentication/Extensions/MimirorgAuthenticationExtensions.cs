using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Factories;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Authentication.Repositories;
using Mimirorg.Authentication.Services;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;

namespace Mimirorg.Authentication.Extensions
{
    public static class MimirorgAuthenticationExtensions
    {
        public static IServiceCollection AddMimirorgAuthenticationModule(this IServiceCollection serviceCollection)
        {
            // Dependency injection
            serviceCollection.AddInjectableHostedService<ITimedHookService, TimedHookService>();

            serviceCollection.AddScoped<IMimirorgTokenRepository, MimirorgTokenRepository>();
            serviceCollection.AddScoped<IMimirorgCompanyRepository, MimirorgCompanyRepository>();
            serviceCollection.AddScoped<IMimirorgHookRepository, MimirorgHookRepository>();
            serviceCollection.AddScoped<IDynamicLogoDataProvider, MimirorgCompanyRepository>();

            serviceCollection.AddScoped<IMimirorgAuthService, MimirorgAuthService>();
            serviceCollection.AddScoped<IMimirorgUserService, MimirorgUserService>();
            serviceCollection.AddScoped<IMimirorgCompanyService, MimirorgCompanyService>();

            serviceCollection.AddScoped<IMimirorgTemplateRepository, MimirorgTemplateRepository>();
            serviceCollection.AddSingleton<IMimirorgAuthFactory, MimirorgAuthFactory>();

            serviceCollection.AddHttpContextAccessor();
            serviceCollection.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            // Get current environment
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = !string.IsNullOrWhiteSpace(environment) && environment.ToLower() == "development";

            if (isDevelopment)
                serviceCollection.AddScoped<IMimirorgEmailRepository, SendLocalRepository>();
            else
                serviceCollection.AddScoped<IMimirorgEmailRepository, SendGridRepository>();

            // Configuration files
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{environment}.json", true)
                .AddJsonFile("appsettings.local.json", true)
                .AddEnvironmentVariables();
            var config = builder.Build();

            // Authentication settings
            var authSettings = new MimirorgAuthSettings();
            config.GetSection("MimirorgAuthSettings").Bind(authSettings);


            serviceCollection.AddSingleton(Options.Create(authSettings));

            // Authentication Database configuration

            var dbConfig = authSettings.DatabaseConfiguration;

            // Entity framework
            var connectionString = dbConfig.ConnectionString;

            if (connectionString != null)
            {
                serviceCollection.AddDbContext<MimirorgAuthenticationContext>(options =>
                    options.UseSqlServer(dbConfig.ConnectionString, sqlOptions =>
                        sqlOptions.MigrationsAssembly("Mimirorg.Authentication")), ServiceLifetime.Transient);
            }
            else
            {
                serviceCollection.AddDbContext<MimirorgAuthenticationContext>(options => options.UseInMemoryDatabase("TestDBAuth"), ServiceLifetime.Transient);
            }

            // Auth options
            serviceCollection.AddIdentity<MimirorgUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = authSettings.RequireNonAlphanumeric;
                    options.Password.RequiredLength = authSettings.RequiredLength;
                    options.Password.RequireDigit = authSettings.RequireDigit;
                    options.Password.RequireUppercase = authSettings.RequireUppercase;
                    options.SignIn = new SignInOptions { RequireConfirmedAccount = authSettings.RequireConfirmedAccount };

                    if (authSettings.MaxFailedAccessAttempts > 0)
                        options.Lockout = new LockoutOptions { DefaultLockoutTimeSpan = TimeSpan.FromMinutes(authSettings.DefaultLockoutMinutes), MaxFailedAccessAttempts = authSettings.MaxFailedAccessAttempts };
                })
                .AddEntityFrameworkStores<MimirorgAuthenticationContext>()
                .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            if (authSettings.JwtKey != null)
            {
                _ = serviceCollection.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = authSettings.JwtIssuer,
                            ValidAudience = authSettings.JwtAudience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey)),
                            ClockSkew = TimeSpan.Zero
                        };
                    });
            }

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

        public static void UseMimirorgAuthenticationModule(this IApplicationBuilder app)
        {
            app.UseSwagger(c => { c.RouteTemplate = "/swagger/{documentName}/swagger.json"; });

            using var serviceScope = app.ApplicationServices.CreateScope();

            // Use swagger
            var service = serviceScope.ServiceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwaggerUI(c =>
            {
                foreach (var description in service.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

                c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();
            app.UseAuthorization();

            // Migrate database
            var context = serviceScope.ServiceProvider.GetRequiredService<MimirorgAuthenticationContext>();
            if (context.Database.IsRelational())
            {
                context.Database.Migrate();
            }

            var timedHookService = serviceScope.ServiceProvider.GetRequiredService<ITimedHookService>();
            timedHookService.IsMigrationFinished = true;
        }
    }
}