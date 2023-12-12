using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Factories;
using Mimirorg.Authentication.Models;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Authentication.Repositories;
using Mimirorg.Authentication.Services;

namespace Mimirorg.Authentication.Extensions;

public static class MimirorgAuthenticationExtensions
{
    public static IServiceCollection AddMimirorgAuthenticationModule(this IServiceCollection serviceCollection, IConfiguration config)
    {
        // Dependency injection
        serviceCollection.AddScoped<IMimirorgTokenRepository, MimirorgTokenRepository>();

        serviceCollection.AddScoped<IMimirorgAuthService, MimirorgAuthService>();
        serviceCollection.AddScoped<IMimirorgUserService, MimirorgUserService>();

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
                .AddJwtBearer("MimirorgAuth", cfg =>
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

            serviceCollection.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder("MimirorgAuth", JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });
        }

        return serviceCollection;
    }

    public static void UseMimirorgAuthenticationModule(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        app.UseAuthentication();
        app.UseAuthorization();

        // Migrate database
        var context = serviceScope.ServiceProvider.GetRequiredService<MimirorgAuthenticationContext>();
        if (context.Database.IsRelational())
        {
            context.Database.Migrate();
        }
    }
}