using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Data;
using TypeLibrary.Services.Terminals;
using TypeLibrary.Services.Terminals.Requests;

namespace Mimirorg.Test.Setup;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // remove the existing context configuration
            var descriptors = services.Where(d => d.ServiceType == typeof(IHostedService) || d.ServiceType == typeof(DbContextOptions<TyleDbContext>) || d.ServiceType == typeof(DbContextOptions<MimirorgAuthenticationContext>)).ToList();
            if (descriptors.Any())
            {
                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }
            }

            services.AddDbContext<TyleDbContext>(options => options.UseInMemoryDatabase("TestDB"), ServiceLifetime.Transient);
            services.AddDbContext<MimirorgAuthenticationContext>(options => options.UseInMemoryDatabase("TestDBAuth"), ServiceLifetime.Transient);
            services.AddAuthentication("IntegrationUser").AddScheme<AuthenticationSchemeOptions, IntegrationTestAuthenticationHandler>("IntegrationUser", _ => { });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<TyleDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<ApiWebApplicationFactory>>();

            var terminalRepository = scopedServices.GetRequiredService<ITerminalRepository>();
            db.Database.EnsureCreated();

            try
            {
                _ = SeedTerminalData(terminalRepository).Result;
            }
            catch (Exception e)
            {
                logger.LogError($"An error occurred seeding the database with test data. Error: {e.Message}");
            }
        });
    }

    private static async Task<bool> SeedTerminalData(ITerminalRepository terminalRepository)
    {
        var terminalA = new TerminalTypeRequest
        {
            Name = "Information",
            Qualifier = Direction.Bidirectional
        };

        var terminalB = new TerminalTypeRequest
        {
            Name = "Automation System 87",
            Qualifier = Direction.Bidirectional
        };

        await terminalRepository.Create(terminalA);

        await terminalRepository.Create(terminalB);

        return true;
    }
}