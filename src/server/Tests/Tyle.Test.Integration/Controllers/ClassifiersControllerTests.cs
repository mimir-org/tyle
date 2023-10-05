using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;
using Tyle.Test.Setup;
using Xunit;

namespace Tyle.Test.Integration.Controllers;

public class ClassifiersControllerTests : IntegrationTest
{
    private const string Endpoint = "/classifiers";

    public ClassifiersControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
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
        var classifierRequest = new RdlClassifierRequest
        {
            Name = "Test classifier",
            Description = "For testing",
            Iri = "http://test.com/classifier"
        };

        var response = await Client.PostAsync(Endpoint, JsonContent.Create(classifierRequest));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = JsonConvert.DeserializeObject<RdlClassifier>(response.Content.ReadAsStringAsync().Result);

        Assert.Equal(classifierRequest.Name, responseContent?.Name);
        Assert.Equal(classifierRequest.Description, responseContent?.Description);
        Assert.Equal(classifierRequest.Iri, responseContent?.Iri.OriginalString);
    }

    [Fact]
    public async Task GET_Returns_Larger_Collection_After_Creation()
    {
        var initialCollection = JsonConvert.DeserializeObject<List<RdlClassifier>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);

        var initialCollectionCount = initialCollection?.Count;

        var classifierRequest = new RdlClassifierRequest
        {
            Name = "Test classifier",
            Description = "For testing",
            Iri = "http://test.com/classifier"
        };

        await Client.PostAsync(Endpoint, JsonContent.Create(classifierRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(classifierRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(classifierRequest));

        var collection = JsonConvert.DeserializeObject<List<RdlClassifier>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount + 3, collectionCount);
    }

    [Fact]
    public async Task GET_Retrieves_Object_After_Creation()
    {
        var classifierRequest = new RdlClassifierRequest
        {
            Name = "Test classifier",
            Description = "For testing",
            Iri = "http://test.com/classifier"
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(classifierRequest));

        var postResponseContent = JsonConvert.DeserializeObject<RdlClassifier>(postResponse.Content.ReadAsStringAsync().Result);

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
        var classifierRequest = new RdlClassifierRequest
        {
            Name = "Test classifier",
            Description = "For testing",
            Iri = "http://test.com/classifier"
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(classifierRequest));

        var postResponseContent = JsonConvert.DeserializeObject<RdlClassifier>(postResponse.Content.ReadAsStringAsync().Result);

        var initialCollection = JsonConvert.DeserializeObject<List<RdlClassifier>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);

        var initialCollectionCount = initialCollection?.Count;

        var deleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var collection = JsonConvert.DeserializeObject<List<RdlClassifier>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount - 1, collectionCount);

        var getResponse = await Client.GetAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        var newDeleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, newDeleteResponse.StatusCode);
    }
}