using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Mimirorg.Setup;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace Mimirorg.Integration.Tests.Controllers
{
    public class LibraryAttributeControllerTests : IntegrationTest
    {
        public LibraryAttributeControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
        {

        }

        [Theory]
        [InlineData("/v1/libraryattribute")]
        [InlineData("/v1/libraryattribute/aspect/0")]
        [InlineData("/v1/libraryattribute/aspect/1")]
        [InlineData("/v1/libraryattribute/aspect/2")]
        [InlineData("/v1/libraryattribute/aspect/4")]
        [InlineData("/v1/libraryattribute/aspect/8")]
        [InlineData("/v1/libraryattribute/predefined")]
        [InlineData("/v1/libraryattribute/datum/0")]
        [InlineData("/v1/libraryattribute/datum/1")]
        [InlineData("/v1/libraryattribute/datum/2")]
        [InlineData("/v1/libraryattribute/datum/3")]
        public async Task GET_Retrieves_Status_Ok(string endpoint)
        {
            var client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {

                });
            }).CreateClient(new WebApplicationFactoryClientOptions());


            var response = await client.GetAsync(endpoint);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}