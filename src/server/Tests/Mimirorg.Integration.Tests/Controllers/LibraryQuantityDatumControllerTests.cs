using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Mimirorg.Test.Setup;
using Xunit;

namespace Mimirorg.Test.Integration.Controllers;

public class LibraryQuantityDatumControllerTests : IntegrationTest
{
    public LibraryQuantityDatumControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
    {

    }

    [Theory]
    [InlineData("/v1/libraryquantitydatum")]
    [InlineData("/v1/libraryquantitydatum/type/0")]
    [InlineData("/v1/libraryquantitydatum/type/1")]
    [InlineData("/v1/libraryquantitydatum/type/2")]
    [InlineData("/v1/libraryquantitydatum/type/3")]
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