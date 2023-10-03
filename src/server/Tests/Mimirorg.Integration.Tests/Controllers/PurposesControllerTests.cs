using System.Net;
using System.Net.Http.Json;
using Mimirorg.Test.Setup;
using TypeLibrary.Services.Common.Requests;
using Xunit;

namespace Mimirorg.Test.Integration.Controllers;

public class PurposesControllerTests : IntegrationTest
{
    private const string Endpoint = "/purposes";

    public PurposesControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task GET_Retrieves_Status_Ok()
    {
        var authorizedResponse = await Client.GetAsync(Endpoint);
        Assert.Equal(HttpStatusCode.OK, authorizedResponse.StatusCode);
    }

    [Fact]
    public async Task POST_Retrieves_Status_Ok()
    {
        var purposeRequest = new RdlPurposeRequest
        {
            Name = "Test purpose",
            Description = "For testing",
            Iri = "http://test.com/purpose"
        };

        var response = await Client.PostAsync(Endpoint, JsonContent.Create(purposeRequest));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}