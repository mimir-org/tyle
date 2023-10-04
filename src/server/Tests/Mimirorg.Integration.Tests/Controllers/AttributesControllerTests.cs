using System.Net;
using System.Net.Http.Json;
using Mimirorg.Test.Setup;
using Newtonsoft.Json;
using TypeLibrary.Api.Attributes;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes.Requests;
using Xunit;

namespace Mimirorg.Test.Integration.Controllers;

public class AttributesControllerTests : IntegrationTest
{
    private const string Endpoint = "/attributes";

    public AttributesControllerTests(ApiWebApplicationFactory fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task GET_Retrieves_Status_Ok()
    {
        var authorizedResponse = await Client.GetAsync(Endpoint);
        Assert.Equal(HttpStatusCode.OK, authorizedResponse.StatusCode);
    }

    [Fact]
    public async Task POST_Retrieves_Status_Created_And_Correct_Object()
    {
        var attributesRequest = new AttributeTypeRequest
        {
            Name = "Test attribute",
            Description = "For testing",
            UnitMinCount = 0,
            UnitMaxCount = 1,
            ProvenanceQualifier = ProvenanceQualifier.CalculatedQualifier,
            RangeQualifier = RangeQualifier.AverageQualifier,
            RegularityQualifier = RegularityQualifier.AbsoluteQualifier,
            ScopeQualifier = ScopeQualifier.DesignQualifier
        };

        var response = await Client.PostAsync(Endpoint, JsonContent.Create(attributesRequest));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseContent = JsonConvert.DeserializeObject<AttributeView>(response.Content.ReadAsStringAsync().Result);

        Assert.Equal(attributesRequest.Name, responseContent?.Name);
        Assert.Equal(attributesRequest.Description, responseContent?.Description);
        Assert.Equal(attributesRequest.UnitMinCount, responseContent?.UnitMinCount);
        Assert.Equal(attributesRequest.UnitMaxCount, responseContent?.UnitMaxCount);
        Assert.Equal(attributesRequest.ProvenanceQualifier, responseContent?.ProvenanceQualifier);
        Assert.Equal(attributesRequest.RangeQualifier, responseContent?.RangeQualifier);
        Assert.Equal(attributesRequest.RegularityQualifier, responseContent?.RegularityQualifier);
        Assert.Equal(attributesRequest.ScopeQualifier, responseContent?.ScopeQualifier);
    }

    [Fact]
    public async Task GET_Returns_Larger_Collection_After_Creation()
    {
        var initialCollection = JsonConvert.DeserializeObject<List<AttributeView>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);

        var initialCollectionCount = initialCollection?.Count;

        var attributeRequest = new AttributeTypeRequest
        {
            Name = "Test attribute",
            UnitMinCount = 0,
            UnitMaxCount = 0
        };

        await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));

        var collection = JsonConvert.DeserializeObject<List<AttributeView>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount + 3, collectionCount);
    }

    [Fact]
    public async Task GET_Retrieves_Object_After_Creation()
    {
        var attributeRequest = new AttributeTypeRequest
        {
            Name = "Test attribute",
            UnitMinCount = 0,
            UnitMaxCount = 0
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));

        var postResponseContent = JsonConvert.DeserializeObject<AttributeView>(postResponse.Content.ReadAsStringAsync().Result);

        var getResponse = await Client.GetAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
    }

    [Fact]
    public async Task GET_Retrieves_Status_NotFound_On_Unknown_Id()
    {
        var response = await Client.GetAsync($"{Endpoint}/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task DELETE_Works_Correctly()
    {
        var attributeRequest = new AttributeTypeRequest
        {
            Name = "Test attribute",
            Description = "For testing",
            UnitMinCount = 1,
            UnitMaxCount = 1
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));

        var postResponseContent = JsonConvert.DeserializeObject<AttributeView>(postResponse.Content.ReadAsStringAsync().Result);

        var initialCollection = JsonConvert.DeserializeObject<List<AttributeView>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);

        var initialCollectionCount = initialCollection?.Count;

        var deleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var collection = JsonConvert.DeserializeObject<List<AttributeView>>((await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync().Result);
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount - 1, collectionCount);

        var getResponse = await Client.GetAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        var newDeleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, newDeleteResponse.StatusCode);
    }
}