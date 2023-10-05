using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Tyle.Application.Attributes.Requests;
using Tyle.Core.Attributes;
using Tyle.Test.Setup;
using Xunit;

namespace Tyle.Test.Integration.Controllers;

public class PredicatesControllerTests : IntegrationTest
{
    private const string Endpoint = "/predicates";

    public PredicatesControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
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
        var predicateRequest = new RdlPredicateRequest
        {
            Name = "Test predicate",
            Description = "For testing",
            Iri = "http://test.com/predicate"
        };

        var response = await Client.PostAsync(Endpoint, JsonContent.Create(predicateRequest));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseContent = JsonConvert.DeserializeObject<RdlPredicate>(await response.Content.ReadAsStringAsync());

        Assert.Equal(predicateRequest.Name, responseContent?.Name);
        Assert.Equal(predicateRequest.Description, responseContent?.Description);
        Assert.Equal(predicateRequest.Iri, responseContent?.Iri.OriginalString);
    }

    [Fact]
    public async Task GET_Returns_Larger_Collection_After_Creation()
    {
        var initialCollection = JsonConvert.DeserializeObject<List<RdlPredicate>>(await (await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync());

        var initialCollectionCount = initialCollection?.Count;

        var predicateRequest = new RdlPredicateRequest
        {
            Name = "Test predicate",
            Description = "For testing",
            Iri = "http://test.com/predicate"
        };

        await Client.PostAsync(Endpoint, JsonContent.Create(predicateRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(predicateRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(predicateRequest));

        var collection = JsonConvert.DeserializeObject<List<RdlPredicate>>(await (await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync());
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount + 3, collectionCount);
    }

    [Fact]
    public async Task GET_Retrieves_Object_After_Creation()
    {
        var predicateRequest = new RdlPredicateRequest
        {
            Name = "Test predicate",
            Description = "For testing",
            Iri = "http://test.com/predicate"
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(predicateRequest));

        var postResponseContent = JsonConvert.DeserializeObject<RdlPredicate>(await postResponse.Content.ReadAsStringAsync());

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
        var predicateRequest = new RdlPredicateRequest
        {
            Name = "Test predicate",
            Description = "For testing",
            Iri = "http://test.com/predicate"
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(predicateRequest));

        var postResponseContent = JsonConvert.DeserializeObject<RdlPredicate>(await postResponse.Content.ReadAsStringAsync());

        var initialCollection = JsonConvert.DeserializeObject<List<RdlPredicate>>(await (await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync());

        var initialCollectionCount = initialCollection?.Count;

        var deleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var collection = JsonConvert.DeserializeObject<List<RdlPredicate>>(await (await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync());
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount - 1, collectionCount);

        var getResponse = await Client.GetAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        var newDeleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, newDeleteResponse.StatusCode);
    }
}