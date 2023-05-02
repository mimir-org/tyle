using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Api;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace Mimirorg.Test.Setup;

public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // remove the existing context configuration
            var descriptors = services.Where(d => d.ServiceType == typeof(IHostedService) || d.ServiceType == typeof(DbContextOptions<TypeLibraryDbContext>) || d.ServiceType == typeof(DbContextOptions<MimirorgAuthenticationContext>)).ToList();
            if (descriptors.Any())
            {
                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }
            }

            services.AddDbContext<TypeLibraryDbContext>(options => options.UseInMemoryDatabase("TestDB"), ServiceLifetime.Transient);
            services.AddDbContext<MimirorgAuthenticationContext>(options => options.UseInMemoryDatabase("TestDBAuth"), ServiceLifetime.Transient);
            services.AddAuthentication("IntegrationUser").AddScheme<AuthenticationSchemeOptions, IntegrationTestAuthenticationHandler>("IntegrationUser", _ => { });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<TypeLibraryDbContext>();
            var logger = scopedServices.GetRequiredService<ILogger<ApiWebApplicationFactory>>();

            var terminalService = scopedServices.GetRequiredService<ITerminalService>();
            var rdsRepository = scopedServices.GetRequiredService<IRdsRepository>();
            db.Database.EnsureCreated();

            try
            {
                _ = SeedTerminalData(terminalService).Result;
            }
            catch (Exception e)
            {
                logger.LogError($"An error occurred seeding the database with test data. Error: {e.Message}");
            }
            try
            {
                var categoryId = rdsRepository.Get().ToList().FirstOrDefault()?.CategoryId;

                var rds = new RdsLibDm
                {
                    Id = "rds-id",
                    RdsCode = "XXXXX",
                    Name = "Test RDS",
                    CategoryId = categoryId,
                    Iri = "",
                    TypeReference = "",
                    Created = DateTime.UtcNow,
                    CreatedBy = "",
                    State = State.Draft,
                    Description = ""
                };

                var createdRds = rdsRepository.Create(rds).Result;
                rdsRepository.ChangeState(State.Approved, createdRds.Id);
            }
            catch (Exception e)
            {
                logger.LogError($"An error occurred seeding the database with test rds data. Error: {e.Message}");
            }
        });
    }

    private static async Task<bool> SeedTerminalData(ITerminalService terminalService)
    {
        var terminalA = new TerminalLibAm
        {
            Name = "Information",
            Color = "#006600"
        };

        var terminalB = new TerminalLibAm
        {
            Name = "Automation System 87",
            Color = "#00CC66"
        };

        await terminalService.Create(terminalA);

        await terminalService.Create(terminalB);

        return true;
    }
}