using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Mimirorg.Setup;
using Mimirorg.TypeLibrary.Models.Client;
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
        [InlineData("/v1/libraryattribute/0")]
        [InlineData("/v1/libraryattribute/1")]
        [InlineData("/v1/libraryattribute/2")]
        [InlineData("/v1/libraryattribute/4")]
        [InlineData("/v1/libraryattribute/8")]
        [InlineData("/v1/libraryattribute/predefined")]
        [InlineData("/v1/libraryattribute/format")]
        [InlineData("/v1/libraryattribute/condition")]
        [InlineData("/v1/libraryattribute/qualifier")]
        [InlineData("/v1/libraryattribute/source")]
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

        [Fact]
        public async Task GET_Retrieves_Same_Data_NotSet_And_Empty()
        {
            var client = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {

                });
            }).CreateClient(new WebApplicationFactoryClientOptions());

            var notSetResponse = await client.GetAndDeserialize<List<AttributeLibCm>>("/v1/libraryattribute/0");
            var response = await client.GetAndDeserialize<List<AttributeLibCm>>("/v1/libraryattribute");
            Assert.Equal(notSetResponse.Count, response.Count);
        }
    }
}