using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Tyle.Application.Common.Requests;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Terminals;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Services.Terminals.Requests;

public class TerminalTypeRequestTests : UnitTest<MimirorgCommonFixture>
{
    public TerminalTypeRequestTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public void ValidationDoesNotFailWithNoDuplicateIds()
    {
        var terminalTypeRequest = new TerminalTypeRequest
        {
            Name = "Test",
            ClassifierIds = new List<int> { 1, 2, 5, 7 },
            Qualifier = Direction.Bidirectional,
            Attributes = new List<AttributeTypeReferenceRequest>
            {
                new()
                {
                    MinCount = 1,
                    AttributeId = Guid.NewGuid()
                },
                new()
                {
                    MinCount = 1,
                    AttributeId = Guid.NewGuid()
                },
                new()
                {
                    MinCount = 0,
                    AttributeId = Guid.NewGuid()
                }
            }
        };

        var validationContext = new ValidationContext(terminalTypeRequest);

        var results = terminalTypeRequest.Validate(validationContext);

        Assert.True(results.IsNullOrEmpty());
    }

    [Fact]
    public void ValidationFailsWithDuplicateClassifierIds()
    {
        var terminalTypeRequest = new TerminalTypeRequest
        {
            Name = "Test",
            ClassifierIds = new List<int> { 1, 5, 5, 7 },
            Qualifier = Direction.Bidirectional,
            Attributes = new List<AttributeTypeReferenceRequest>
            {
                new()
                {
                    MinCount = 1,
                    AttributeId = Guid.NewGuid()
                },
                new()
                {
                    MinCount = 1,
                    AttributeId = Guid.NewGuid()
                },
                new()
                {
                    MinCount = 0,
                    AttributeId = Guid.NewGuid()
                }
            }
        };

        var validationContext = new ValidationContext(terminalTypeRequest);

        var results = terminalTypeRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }

    [Fact]
    public void ValidationFailsWithDuplicateAttributeIds()
    {
        var duplicateAttributeId = Guid.NewGuid();

        var terminalTypeRequest = new TerminalTypeRequest
        {
            Name = "Test",
            ClassifierIds = new List<int> { 1, 2, 5, 7 },
            Qualifier = Direction.Bidirectional,
            Attributes = new List<AttributeTypeReferenceRequest>
            {
                new()
                {
                    MinCount = 1,
                    AttributeId = duplicateAttributeId
                },
                new()
                {
                    MinCount = 1,
                    AttributeId = Guid.NewGuid()
                },
                new()
                {
                    MinCount = 0,
                    AttributeId = duplicateAttributeId
                }
            }
        };

        var validationContext = new ValidationContext(terminalTypeRequest);

        var results = terminalTypeRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }
}