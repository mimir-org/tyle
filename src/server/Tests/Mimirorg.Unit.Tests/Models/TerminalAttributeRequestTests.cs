using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Models.Application;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

public class TerminalAttributeRequestTests : UnitTest<MimirorgCommonFixture>
{
    public TerminalAttributeRequestTests(MimirorgCommonFixture fixture) : base(fixture)
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
        var terminalAttributeRequest = new TerminalAttributeRequest
        {
            MinCount = min,
            MaxCount = max,
            AttributeId = Guid.NewGuid()
        };

        var validationContext = new ValidationContext(terminalAttributeRequest);

        var results = terminalAttributeRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }
}
