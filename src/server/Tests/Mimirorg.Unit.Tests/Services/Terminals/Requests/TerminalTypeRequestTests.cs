using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Common.Requests;
using TypeLibrary.Services.Terminals.Requests;
using Xunit;

namespace Mimirorg.Test.Unit.Services.Terminals.Requests;

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