using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Tyle.Application.Attributes.Requests;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Services.Attributes.Requests;

public class AttributeTypeRequestTests : UnitTest<MimirorgCommonFixture>
{
    public AttributeTypeRequestTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Theory]
    [InlineData(0, 1, true)]
    [InlineData(0, 0, true)]
    [InlineData(1, 1, true)]
    [InlineData(1, 0, false)]
    public void UnitCountValidatesCorrectly(int unitMinCount, int unitMaxCount, bool result)
    {
        var attributeTypeRequest = new AttributeTypeRequest
        {
            Name = "Test",
            UnitIds = new List<int>(),
            UnitMinCount = unitMinCount,
            UnitMaxCount = unitMaxCount
        };

        var validationContext = new ValidationContext(attributeTypeRequest);

        var results = attributeTypeRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    [Fact]
    public void ValidationFailsWithDuplicateUnitIds()
    {
        var attributeTypeRequest = new AttributeTypeRequest
        {
            Name = "Test",
            UnitIds = new List<int>() { 1, 2, 3, 1 },
            UnitMinCount = 0,
            UnitMaxCount = 1
        };

        var validationContext = new ValidationContext(attributeTypeRequest);

        var results = attributeTypeRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }
}