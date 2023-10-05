using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Tyle.Application.Blocks.Requests;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Services.Blocks.Requests;

public class TerminalTypeReferenceRequestTests : UnitTest<MimirorgCommonFixture>
{
    public TerminalTypeReferenceRequestTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Theory]
    [InlineData(0, 1, true)]
    [InlineData(1, 5, true)]
    [InlineData(3, 1, false)]
    [InlineData(4, 0, false)]
    [InlineData(1, null, true)]
    [InlineData(0, null, true)]
    public void MinMaxCountValidatesCorrectly(int min, int? max, bool result)
    {
        var blockTerminalRequest = new TerminalTypeReferenceRequest
        {
            MinCount = min,
            MaxCount = max,
            TerminalId = Guid.NewGuid()
        };

        var validationContext = new ValidationContext(blockTerminalRequest);

        var results = blockTerminalRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }
}