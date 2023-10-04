using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using TypeLibrary.Services.Attributes.Requests;
using Xunit;

namespace Mimirorg.Test.Unit.Services.Attributes.Requests;

public class AttributeGroupRequestTests : UnitTest<MimirorgCommonFixture>
{
    public AttributeGroupRequestTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public void ValidationPassesWithUniqueAttributeIds()
    {
        var attributeGroupRequest = new AttributeGroupRequest
        {
            Name = "Test group",
            AttributeIds = new List<Guid> {Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()}
        };

        var validationContext = new ValidationContext(attributeGroupRequest);

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

        var validationContext = new ValidationContext(attributeGroupRequest);

        var results = attributeGroupRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }

}
