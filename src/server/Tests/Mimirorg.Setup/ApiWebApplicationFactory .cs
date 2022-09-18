using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using TypeLibrary.Api;
using TypeLibrary.Data;
using TypeLibrary.Services.Contracts;

namespace Mimirorg.Setup
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // remove the existing context configuration
                var descriptors = services.Where(d => d.ServiceType == typeof(DbContextOptions<TypeLibraryDbContext>) || d.ServiceType == typeof(DbContextOptions<MimirorgAuthenticationContext>)).ToList();
                if (descriptors.Any())
                {
                    foreach (var descriptor in descriptors)
                    {
                        services.Remove(descriptor);
                    }
                }

                services.AddDbContext<TypeLibraryDbContext>(options => options.UseInMemoryDatabase("TestDB"));
                services.AddDbContext<MimirorgAuthenticationContext>(options => options.UseInMemoryDatabase("TestDBAuth"));
                services.AddAuthentication("IntegrationUser").AddScheme<AuthenticationSchemeOptions, IntegrationTestAuthenticationHandler>("IntegrationUser", options => { });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TypeLibraryDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<ApiWebApplicationFactory>>();

                var attributeService = scopedServices.GetRequiredService<IAttributeService>();
                var terminalService = scopedServices.GetRequiredService<ITerminalService>();
                db.Database.EnsureCreated();

                try
                {
                    _ = SeedAttributeData(attributeService).Result;
                    _ = SeedTerminalData(terminalService).Result;
                }
                catch (Exception e)
                {
                    logger.LogError($"An error occurred seeding the database with test data. Error: {e.Message}");
                }
            });
        }

        private static async Task<bool> SeedAttributeData(IAttributeService attributeService)
        {
            var attribute = new AttributeLibAm
            {
                Name = "Pressure, absolute",
                Aspect = Aspect.Product,
                Discipline = Discipline.Process,
                Select = Select.None,
                SelectValues = null,
                UnitIdList = null,
                TypeReferences = new List<TypeReferenceAm>
                {
                    new()
                    {
                        Name = "pressure",
                        Iri = @"http://rds.posccaesar.org/ontology/plm/rdl/PCA_100003596",
                        Source = "PCA",
                        Subs = new List<TypeReferenceSub>
                        {
                            new()
                            {
                                Name = "pascal",
                                Iri = @"http://rds.posccaesar.org/ontology/plm/rdl/PCA_100003716",
                                IsDefault = true
                            }
                        }
                    }
                },
                QuantityDatumSpecifiedScope = "Design Datum",
                QuantityDatumSpecifiedProvenance = "Calculated Datum",
                QuantityDatumRangeSpecifying = "Maximum Datum",
                QuantityDatumRegularitySpecified = "Absolute Datum",
                CompanyId = 1
            };

            await attributeService.Create(attribute);
            return true;
        }

        private static async Task<bool> SeedTerminalData(ITerminalService terminalService)
        {
            var terminals = new List<TerminalLibAm>
            {
                new()
                {
                    Name = "Information",
                    Color = "#006600",
                    ParentId = null,
                    CompanyId = 1,
                    AttributeIdList = null
                },
                new()
                {
                    Name = "Automation System 87",
                    Color = "#00CC66",
                    ParentId = "201B169264C4F9249039054BCCDD4494",
                    CompanyId = 1,
                    AttributeIdList = null
                    //AttributeIdList = new List<string> {"CA20DF193D58238C3C557A0316C15533"}
                }
            };

            await terminalService.Create(terminals);
            return true;
        }
    }
}