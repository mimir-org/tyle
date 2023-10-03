using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Mimirorg.Test.Setup;
using TypeLibrary.Services.Common.Requests;
using Xunit;

// ReSharper disable StringLiteralTypo

namespace Mimirorg.Test.Integration.Controllers;

public class LibraryPurposeControllerTests : IntegrationTest
{
    private HttpClient _httpClient;

    public LibraryPurposeControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
    {
        _httpClient = fixture.CreateClient();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("IntegrationUser");
    }

    [Theory]
    [InlineData("/purposes")]
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
    [InlineData("/purposes")]
    public async Task POST_Retrieves_Status_Created(string endpoint)
    {
        var purposeRequest = new RdlPurposeRequest
        {
            Name = "Test purpose",
            Description = "For testing",
            Iri = "http://test.com/purpose"
        };

        var response = await _httpClient.PostAsync(endpoint, JsonContent.Create(purposeRequest));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}