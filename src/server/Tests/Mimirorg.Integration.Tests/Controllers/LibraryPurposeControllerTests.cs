using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Mimirorg.Test.Setup;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace Mimirorg.Test.Integration.Controllers;

public class LibraryPurposeControllerTests : IntegrationTest
{
    public LibraryPurposeControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
    {

    }

    [Theory]
    [InlineData("/v1/librarypurpose")]
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
}