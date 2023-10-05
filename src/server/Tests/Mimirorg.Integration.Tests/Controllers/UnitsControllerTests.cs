using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Tyle.Application.Attributes.Requests;
using Tyle.Core.Attributes;
using Tyle.Test.Setup;
using Xunit;

namespace Tyle.Test.Integration.Controllers;

public class UnitsControllerTests : IntegrationTest
{
    private const string Endpoint = "/units";

    public UnitsControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
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
        var unitRequest = new RdlUnitRequest
        {
            Name = "Test unit",
            Symbol = "tu",
            Description = "For testing",
            Iri = "http://test.com/unit"
        };

        var response = await Client.PostAsync(Endpoint, JsonContent.Create(unitRequest));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = JsonConvert.DeserializeObject<RdlUnit>(response.Content.ReadAsStringAsync().Result);

        Assert.Equal(unitRequest.Name, responseContent?.Name);
        Assert.Equal(unitRequest.Symbol, responseContent?.Symbol);
        Assert.Equal(unitRequest.Description, responseContent?.Description);
        Assert.Equal(unitRequest.Iri, responseContent?.Iri.OriginalString);
    }

    [Fact]
    public async Task GET_Returns_Larger_Collection_After_Creation()
    {
        var initialCollection = JsonConvert.DeserializeObject<List<RdlUnit>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);

        var initialCollectionCount = initialCollection?.Count;

        var unitRequest = new RdlUnitRequest
        {
            Name = "Test unit",
            Symbol = "tu",
            Description = "For testing",
            Iri = "http://test.com/unit"
        };

        await Client.PostAsync(Endpoint, JsonContent.Create(unitRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(unitRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(unitRequest));

        var collection = JsonConvert.DeserializeObject<List<RdlUnit>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount + 3, collectionCount);
    }

    [Fact]
    public async Task GET_Retrieves_Object_After_Creation()
    {
        var unitRequest = new RdlUnitRequest
        {
            Name = "Test unit",
            Description = "For testing",
            Iri = "http://test.com/unit"
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(unitRequest));

        var postResponseContent = JsonConvert.DeserializeObject<RdlUnit>(postResponse.Content.ReadAsStringAsync().Result);

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
        var unitRequest = new RdlUnitRequest
        {
            Name = "Test unit",
            Description = "For testing",
            Iri = "http://test.com/unit"
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(unitRequest));

        var postResponseContent = JsonConvert.DeserializeObject<RdlUnit>(postResponse.Content.ReadAsStringAsync().Result);

        var initialCollection = JsonConvert.DeserializeObject<List<RdlUnit>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);

        var initialCollectionCount = initialCollection?.Count;

        var deleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var collection = JsonConvert.DeserializeObject<List<RdlUnit>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount - 1, collectionCount);

        var getResponse = await Client.GetAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        var newDeleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, newDeleteResponse.StatusCode);
    }
}