using System.Net;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Tyle.Api.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Core.Attributes;
using Tyle.Test.Setup;
using Xunit;

namespace Tyle.Test.Integration.Controllers;

public class AttributesControllerTests : IntegrationTest
{
    private const string Endpoint = "/attributes";
    private const string PredicatesEndpoint = "/predicates";
    private const string UnitsEndpoint = "/units";

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

        var responseContent = JsonConvert.DeserializeObject<AttributeView>(await response.Content.ReadAsStringAsync());

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
    public async Task POST_Retrieves_Status_Created_And_Correct_Object_With_References_And_Value_Constraints()
    {
        // Set up, creating necessary predicate and units

        var predicateRequest = new RdlPredicateRequest
        {
            Name = "Test predicate",
            Iri = "http://example.com/predicate"
        };

        var predicateResponse = await Client.PostAsync(PredicatesEndpoint, JsonContent.Create(predicateRequest));
        var predicateResponseContent = JsonConvert.DeserializeObject<RdlPredicate>(await predicateResponse.Content.ReadAsStringAsync());

        var firstUnitRequest = new RdlUnitRequest
        {
            Name = "kg",
            Iri = "http://example.com/kg"
        };

        var firstUnitResponse = await Client.PostAsync(UnitsEndpoint, JsonContent.Create(firstUnitRequest));
        var firstUnitResponseContent = JsonConvert.DeserializeObject<RdlUnit>(await firstUnitResponse.Content.ReadAsStringAsync());

        var secondUnitRequest = new RdlUnitRequest
        {
            Name = "m",
            Iri = "http://example.com/m"
        };

        var secondUnitResponse = await Client.PostAsync(UnitsEndpoint, JsonContent.Create(secondUnitRequest));
        var secondUnitResponseContent = JsonConvert.DeserializeObject<RdlUnit>(await secondUnitResponse.Content.ReadAsStringAsync());


        // Creating the attribute type

        var attributesRequest = new AttributeTypeRequest
        {
            Name = "Test attribute",
            UnitMinCount = 0,
            UnitMaxCount = 1,
            PredicateId = predicateResponseContent?.Id,
            UnitIds = new List<int> { firstUnitResponseContent!.Id, secondUnitResponseContent!.Id },
            ValueConstraint = new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.HasValue,
                DataType = XsdDataType.String,
                Value = "Test value"
            }
        };

        var response = await Client.PostAsync(Endpoint, JsonContent.Create(attributesRequest));

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseContent = JsonConvert.DeserializeObject<AttributeViewWithStringValueConstraint>(await response.Content.ReadAsStringAsync());

        Assert.Equal(attributesRequest.Name, responseContent?.Name);
        Assert.Equal(attributesRequest.UnitMinCount, responseContent?.UnitMinCount);
        Assert.Equal(attributesRequest.UnitMaxCount, responseContent?.UnitMaxCount);
        Assert.Equal(attributesRequest.PredicateId, responseContent?.Predicate?.Id);
        Assert.Contains(firstUnitResponseContent.Id, responseContent!.Units.Select(x => x.Id));
        Assert.Contains(secondUnitResponseContent.Id, responseContent!.Units.Select(x => x.Id));
        Assert.Equal(attributesRequest.ValueConstraint.Value, responseContent.ValueConstraint?.Value);
    }

    [Fact]
    public async Task GET_Returns_Larger_Collection_After_Creation()
    {
        var initialCollection = JsonConvert.DeserializeObject<List<AttributeView>>(await (await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync());

        var initialCollectionCount = initialCollection?.Count;

        var attributeRequest = new AttributeTypeRequest
        {
            Name = "Test attribute",
            UnitMinCount = 0,
            UnitMaxCount = 0,
            ProvenanceQualifier = ProvenanceQualifier.MeasuredQualifier,
            RangeQualifier = RangeQualifier.MaximumQualifier,
            RegularityQualifier = RegularityQualifier.ContinuousQualifier,
            ScopeQualifier = ScopeQualifier.OperatingQualifier
        };

        await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));
        await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));

        var collection = JsonConvert.DeserializeObject<List<AttributeView>>(await (await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync());
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
            UnitMaxCount = 0,
            ProvenanceQualifier = ProvenanceQualifier.SpecifiedQualifier,
            RangeQualifier = RangeQualifier.MinimumQualifier
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));

        var postResponseContent = JsonConvert.DeserializeObject<AttributeView>(await postResponse.Content.ReadAsStringAsync());

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
    public async Task PUT_Can_Update_Simple_Fields()
    {
        var attributeRequest = new AttributeTypeRequest
        {
            Name = "Test attribute",
            Description = "For testing",
            UnitMinCount = 0,
            UnitMaxCount = 1,
            ProvenanceQualifier = ProvenanceQualifier.CalculatedQualifier,
            RangeQualifier = RangeQualifier.NominalQualifier,
            RegularityQualifier = RegularityQualifier.AbsoluteQualifier,
            ScopeQualifier = ScopeQualifier.DesignQualifier
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(attributeRequest));

        var postResponseContent = JsonConvert.DeserializeObject<AttributeView>(await postResponse.Content.ReadAsStringAsync());

        var attributesUpdateRequest = new AttributeTypeRequest
        {
            Name = "Updated test attribute",
            UnitMinCount = 1,
            UnitMaxCount = 1,
            ProvenanceQualifier = ProvenanceQualifier.MeasuredQualifier,
            RangeQualifier = RangeQualifier.NormalQualifier
        };

        var putResponse = await Client.PutAsync($"{Endpoint}/{postResponseContent?.Id}", JsonContent.Create(attributesUpdateRequest));

        Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);

        var putResponseContent = JsonConvert.DeserializeObject<AttributeView>(await putResponse.Content.ReadAsStringAsync());

        Assert.Equal(attributesUpdateRequest.Name, putResponseContent?.Name);
        Assert.Null(attributesUpdateRequest.Description);
        Assert.Equal(attributesUpdateRequest.UnitMinCount, putResponseContent?.UnitMinCount);
        Assert.Equal(attributesUpdateRequest.UnitMaxCount, putResponseContent?.UnitMaxCount);
        Assert.Equal(attributesUpdateRequest.ProvenanceQualifier, putResponseContent?.ProvenanceQualifier);
        Assert.Equal(attributesUpdateRequest.RangeQualifier, putResponseContent?.RangeQualifier);
        Assert.Null(attributesUpdateRequest.RegularityQualifier);
        Assert.Null(attributesUpdateRequest.ScopeQualifier);
    }

    [Fact]
    public async Task PUT_Can_Update_Complex_Fields()
    {
        // Set up, creating necessary predicate and units

        var firstPredicateRequest = new RdlPredicateRequest
        {
            Name = "Test predicate",
            Iri = "http://example.com/predicate"
        };

        var firstPredicateResponse = await Client.PostAsync(PredicatesEndpoint, JsonContent.Create(firstPredicateRequest));
        var firstPredicateResponseContent = JsonConvert.DeserializeObject<RdlPredicate>(await firstPredicateResponse.Content.ReadAsStringAsync());

        var secondPredicateRequest = new RdlPredicateRequest
        {
            Name = "Test predicate two",
            Iri = "http://example.com/predicate/two"
        };

        var secondPredicateResponse = await Client.PostAsync(PredicatesEndpoint, JsonContent.Create(secondPredicateRequest));
        var secondPredicateResponseContent = JsonConvert.DeserializeObject<RdlPredicate>(await secondPredicateResponse.Content.ReadAsStringAsync());

        var firstUnitRequest = new RdlUnitRequest
        {
            Name = "kg",
            Iri = "http://example.com/kg"
        };

        var firstUnitResponse = await Client.PostAsync(UnitsEndpoint, JsonContent.Create(firstUnitRequest));
        var firstUnitResponseContent = JsonConvert.DeserializeObject<RdlUnit>(await firstUnitResponse.Content.ReadAsStringAsync());

        var secondUnitRequest = new RdlUnitRequest
        {
            Name = "m",
            Iri = "http://example.com/m"
        };

        var secondUnitResponse = await Client.PostAsync(UnitsEndpoint, JsonContent.Create(secondUnitRequest));
        var secondUnitResponseContent = JsonConvert.DeserializeObject<RdlUnit>(await secondUnitResponse.Content.ReadAsStringAsync());

        var thirdUnitRequest = new RdlUnitRequest
        {
            Name = "W",
            Iri = "http://example.com/W"
        };

        var thirdUnitResponse = await Client.PostAsync(UnitsEndpoint, JsonContent.Create(thirdUnitRequest));
        var thirdUnitResponseContent = JsonConvert.DeserializeObject<RdlUnit>(await thirdUnitResponse.Content.ReadAsStringAsync());


        // Creating the attribute type

        var attributesRequest = new AttributeTypeRequest
        {
            Name = "Test attribute",
            UnitMinCount = 0,
            UnitMaxCount = 1,
            PredicateId = firstPredicateResponseContent?.Id,
            UnitIds = new List<int> { firstUnitResponseContent!.Id, secondUnitResponseContent!.Id },
            ValueConstraint = new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.HasValue,
                DataType = XsdDataType.String,
                Value = "Test value"
            }
        };

        var postResponse = await Client.PostAsync(Endpoint, JsonContent.Create(attributesRequest));
        var postResponseContent = JsonConvert.DeserializeObject<AttributeViewWithStringValueConstraint>(await postResponse.Content.ReadAsStringAsync());

        var firstAttributeUpdate = new AttributeTypeRequest
        {
            Name = "Test attribute",
            UnitMinCount = 0,
            UnitMaxCount = 1,
            PredicateId = secondPredicateResponseContent?.Id,
            UnitIds = new List<int> { firstUnitResponseContent!.Id, thirdUnitResponseContent!.Id },
            ValueConstraint = new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.In,
                DataType = XsdDataType.Decimal,
                ValueList = new List<string> { "0.2", "-45.3", "270" },
                MinCount = 1,
                MaxCount = 1
            }
        };

        var firstPutResponse = await Client.PutAsync($"{Endpoint}/{postResponseContent?.Id}", JsonContent.Create(firstAttributeUpdate));

        Assert.Equal(HttpStatusCode.OK, firstPutResponse.StatusCode);

        var firstPutResponseContent = JsonConvert.DeserializeObject<AttributeViewWithNumericalValueListConstraint>(await firstPutResponse.Content.ReadAsStringAsync());

        Assert.Equal(firstAttributeUpdate.PredicateId, firstPutResponseContent?.Predicate?.Id);
        Assert.Contains(firstUnitResponseContent.Id, firstPutResponseContent!.Units.Select(x => x.Id));
        Assert.Contains(thirdUnitResponseContent.Id, firstPutResponseContent.Units.Select(x => x.Id));
        Assert.DoesNotContain(secondUnitResponseContent.Id, firstPutResponseContent.Units.Select(x => x.Id));
        Assert.Equal(3, firstPutResponseContent.ValueConstraint!.ValueList.Count());
        Assert.Contains(-45.3M, firstPutResponseContent.ValueConstraint.ValueList);

        var secondAttributeUpdate = new AttributeTypeRequest
        {
            Name = "Test attribute",
            UnitMinCount = 0,
            UnitMaxCount = 1
        };

        var secondPutResponse = await Client.PutAsync($"{Endpoint}/{postResponseContent?.Id}", JsonContent.Create(secondAttributeUpdate));

        Assert.Equal(HttpStatusCode.OK, secondPutResponse.StatusCode);

        var secondPutResponseContent = JsonConvert.DeserializeObject<AttributeView>(await secondPutResponse.Content.ReadAsStringAsync());

        Assert.Null(secondPutResponseContent!.Predicate);
        Assert.Empty(secondPutResponseContent.Units);
        Assert.Null(secondPutResponseContent.ValueConstraint);
    }

    [Fact]
    public async Task PUT_Retrieves_Status_NotFound_On_Unknown_Id()
    {
        var attributesUpdateRequest = new AttributeTypeRequest
        {
            Name = "Updated test attribute",
            UnitMinCount = 1,
            UnitMaxCount = 1,
            ProvenanceQualifier = ProvenanceQualifier.MeasuredQualifier,
            RangeQualifier = RangeQualifier.NormalQualifier
        };

        var response = await Client.PutAsync($"{Endpoint}/{Guid.NewGuid()}", JsonContent.Create(attributesUpdateRequest));

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

        var postResponseContent = JsonConvert.DeserializeObject<AttributeView>(await postResponse.Content.ReadAsStringAsync());

        var initialCollection = JsonConvert.DeserializeObject<List<AttributeView>>(await (await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync());

        var initialCollectionCount = initialCollection?.Count;

        var deleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var collection = JsonConvert.DeserializeObject<List<AttributeView>>(await (await Client.GetAsync(Endpoint)).Content.ReadAsStringAsync());
        var collectionCount = collection?.Count;

        Assert.Equal(initialCollectionCount - 1, collectionCount);

        var getResponse = await Client.GetAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);

        var newDeleteResponse = await Client.DeleteAsync($"{Endpoint}/{postResponseContent?.Id}");

        Assert.Equal(HttpStatusCode.NotFound, newDeleteResponse.StatusCode);
    }
}

internal class AttributeViewWithStringValueConstraint : AttributeView
{
    public new HasStringValueConstraintView? ValueConstraint { get; set; }
}

internal class AttributeViewWithNumericalValueListConstraint : AttributeView
{
    public new NumericValueListConstraintView? ValueConstraint { get; set; }
}