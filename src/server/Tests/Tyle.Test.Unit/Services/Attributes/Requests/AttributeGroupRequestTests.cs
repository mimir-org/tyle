using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Tyle.Application.Attributes.Requests;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Services.Attributes.Requests;

public class AttributeGroupRequestTests : UnitTest<RequestTestFixture>
{
    private readonly IServiceProvider _serviceProvider;

    public AttributeGroupRequestTests(RequestTestFixture fixture) : base(fixture)
    {
        _serviceProvider = fixture.ServiceProvider.Object;
    }

    [Fact]
    public void ValidationPassesWithUniqueAttributeIds()
    {
        var attributeGroupRequest = new AttributeGroupRequest
        {
            Name = "Test group",
            AttributeIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }
        };

        var validationContext = new ValidationContext(attributeGroupRequest, _serviceProvider, null);

        var results = attributeGroupRequest.Validate(validationContext);

        Assert.True(results.IsNullOrEmpty());
    }

    [Fact]
    public void ValidationFailsWithRepeatedAttributeIds()
    {
        var repeatedGuid = Guid.NewGuid();

        var attributeGroupRequest = new AttributeGroupRequest
        {
            Name = "Test group",
            AttributeIds = new List<Guid> { repeatedGuid, repeatedGuid, Guid.NewGuid() }
        };

        var validationContext = new ValidationContext(attributeGroupRequest, _serviceProvider, null);

        var results = attributeGroupRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }

}