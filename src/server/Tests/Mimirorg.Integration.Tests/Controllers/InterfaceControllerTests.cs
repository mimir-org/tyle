using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using Xunit;

namespace Mimirorg.Integration.Tests.Controllers
{
    public class InterfaceControllerTests : IntegrationTest
    {
        public InterfaceControllerTests(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/v1/libraryinterface")]
        public async Task GET_Retrieves_Status_Ok(string endpoint)
        {
            var client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(_ =>
                {

                });
            }).CreateClient(new WebApplicationFactoryClientOptions());


            var response = await client.GetAsync(endpoint);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/v1/libraryinterface/AB01BBCDA5B2CC285A922F90DA6E836D")]
        public async Task GET_Id_Retrieves_Status_Ok(string endpoint)
        {
            var client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(_ =>
                {

                });
            }).CreateClient(new WebApplicationFactoryClientOptions());

            using var scope = Factory.Server.Services.CreateScope();
            var interfaceService = scope.ServiceProvider.GetRequiredService<IInterfaceService>();
            var terminalService = scope.ServiceProvider.GetRequiredService<ITerminalService>();

            var terminalAm = new TerminalLibAm
            {
                Name = "Terminal11hhh001",
                Color = "#45678",
                CompanyId = 1,
                Version = "1.0"
            };

            var terminalCm = await terminalService.Create(terminalAm);

            const string guid = "2f9e0813-1067-472e-86ea-7c0b47a4eb18";

            // Ensure Interface in fake database
            var interfaceToCreate = new InterfaceLibAm
            {
                Name = $"{guid}_dummy_name",
                RdsName = $"{guid}_dummy_rds_name",
                RdsCode = $"{guid}_dummy_rds_code",
                PurposeName = $"{guid}_dummy_purpose_name",
                TerminalId = terminalCm.Id,
                Aspect = Aspect.NotSet,
                CompanyId = 1,
                Version = "1.0"
            };

            _ = await interfaceService.Create(interfaceToCreate);

            var response = await client.GetAsync(endpoint);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/v1/libraryinterface/dummy_id")]
        public async Task GET_Id_Retrieves_Status_No_Content(string endpoint)
        {
            var client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(_ =>
                {

                });
            }).CreateClient(new WebApplicationFactoryClientOptions());

            var response = await client.GetAsync(endpoint);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}