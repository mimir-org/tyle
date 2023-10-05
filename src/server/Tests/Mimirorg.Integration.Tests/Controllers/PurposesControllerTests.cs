using System.Net;
using System.Net.Http.Json;
using Mimirorg.Test.Setup;
using Newtonsoft.Json;
using TypeLibrary.Core.Common;
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
    public async Task POST_Retrieves_Status_Ok_And_Correct_Object()
    {
        var purposeRequest = new RdlPurposeRequest
        {
            Name = "Test purpose",
            Description = "For testing",
            Iri = "http://test.com/purpose"
        };

        var response = await Client.PostAsync(Endpoint, JsonContent.Create(purposeRequest));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = JsonConvert.DeserializeObject<RdlPurpose>(response.Content.ReadAsStringAsync().Result);

        Assert.Equal(purposeRequest.Name, responseContent?.Name);
        Assert.Equal(purposeRequest.Description, responseContent?.Description);
        Assert.Equal(purposeRequest.Iri, responseContent?.Iri.OriginalString);
    }

    [Fact]
    public async Task GET_Returns_Larger_Collection_After_Creation()
    {
        var initialCollection = JsonConvert.DeserializeObject<List<RdlPurpose>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);

        var initialCollectionCount = initialCollection?.Count;

        var purposeRequest = new RdlPurposeRequest
        {
            Name = "Test purpose",
            Description = "For testing",
            Iri = "http://test.com/purpose"
        };

        await Client.PostAsync(Endpoint, JsonContent.Create(purposeRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(purposeRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(purposeRequest));

        var collection = JsonConvert.DeserializeObject<List<RdlPurpose>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount + 3, collectionCount);
    }

    [Fact]
    public async Task GET_Retrieves_Object_After_Creation()
    {
        var purposeRequest = new RdlPurposeRequest
        {
            Name = "Test purpose",
            Description = "For testing",
            Iri = "http://test.com/purpose"
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(purposeRequest));

        var postResponseContent = JsonConvert.DeserializeObject<RdlPurpose>(postResponse.Content.ReadAsStringAsync().Result);

        var getResponse = await Client.GetAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task GET_Retrieves_Status_NotFound_On_Unknown_Id()
    {
        var response = await Client.GetAsync($"{Endpoint}/666");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DELETE_Works_Correctly()
    {
        var purposeRequest = new RdlPurposeRequest
        {
            Name = "Test purpose",
            Description = "For testing",
            Iri = "http://test.com/purpose"
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(purposeRequest));

        var postResponseContent = JsonConvert.DeserializeObject<RdlPurpose>(postResponse.Content.ReadAsStringAsync().Result);

        var initialCollection = JsonConvert.DeserializeObject<List<RdlPurpose>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);

        var initialCollectionCount = initialCollection?.Count;

        var deleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var collection = JsonConvert.DeserializeObject<List<RdlPurpose>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount - 1, collectionCount);

        var getResponse = await Client.GetAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        var newDeleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, newDeleteResponse.StatusCode);
    }
}