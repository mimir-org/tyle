using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Blocks.Requests;
using TypeLibrary.Services.Common.Requests;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

public class BlockTypeRequestTests : UnitTest<MimirorgCommonFixture>
{
    public BlockTypeRequestTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public void ValidationDoesNotFailWithNoDuplicateIds()
    {
        var blockTypeRequest = new BlockTypeRequest
        {
            Name = "Test",
            ClassifierIds = new List<int> { 1, 2, 5, 7 },
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
            },
            Terminals = new List<TerminalTypeReferenceRequest>
                {
                    new()
                    {
                        MinCount = 1,
                        TerminalId = Guid.NewGuid(),
                        Direction = Direction.Bidirectional
                    },
                    new()
                    {
                        MinCount = 1,
                        TerminalId = Guid.NewGuid(),
                        Direction = Direction.Bidirectional
                    },
                    new()
                    {
                        MinCount = 0,
                        TerminalId = Guid.NewGuid(),
                        Direction = Direction.Output
                    }
                }
        };

        var validationContext = new ValidationContext(blockTypeRequest);

        var results = blockTypeRequest.Validate(validationContext);

        Assert.True(results.IsNullOrEmpty());
    }

    [Fact]
    public void ValidationFailsWithDuplicateClassifierIds()
    {
        var blockTypeRequest = new BlockTypeRequest
        {
            Name = "Test",
            ClassifierIds = new List<int> { 1, 5, 5, 7 },
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

        var validationContext = new ValidationContext(blockTypeRequest);

        var results = blockTypeRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }

    [Fact]
    public void ValidationFailsWithDuplicateAttributeIds()
    {
        var duplicateAttributeId = Guid.NewGuid();

        var blockTypeRequest = new BlockTypeRequest
        {
            Name = "Test",
            ClassifierIds = new List<int> { 1, 2, 5, 7 },
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

        var validationContext = new ValidationContext(blockTypeRequest);

        var results = blockTypeRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }



    [Fact]
    public void ValidationDoesNotFailWithDuplicateTerminalIdsButDifferentDirections()
    {
        var duplicateTerminalId = Guid.NewGuid();

        var blockTypeRequest = new BlockTypeRequest
        {
            Name = "Test",
            ClassifierIds = new List<int> { 1, 2, 5, 7 },
            Terminals = new List<TerminalTypeReferenceRequest>
            {
                new()
                {
                    MinCount = 1,
                    TerminalId = duplicateTerminalId,
                    Direction = Direction.Bidirectional
                },
                new()
                {
                    MinCount = 1,
                    TerminalId = duplicateTerminalId,
                    Direction = Direction.Input
                },
                new()
                {
                    MinCount = 0,
                    TerminalId = duplicateTerminalId,
                    Direction = Direction.Output
                }
            }
        };

        var validationContext = new ValidationContext(blockTypeRequest);

        var results = blockTypeRequest.Validate(validationContext);

        Assert.True(results.IsNullOrEmpty());
    }



    [Fact]
    public void ValidationFailsWithDuplicateTerminalIdsAndDirections()
    {
        var duplicateTerminalId = Guid.NewGuid();

        var blockTypeRequest = new BlockTypeRequest
        {
            Name = "Test",
            ClassifierIds = new List<int> { 1, 2, 5, 7 },
            Terminals = new List<TerminalTypeReferenceRequest>
            {
                new()
                {
                    MinCount = 1,
                    TerminalId = duplicateTerminalId,
                    Direction = Direction.Bidirectional
                },
                new()
                {
                    MinCount = 1,
                    TerminalId = duplicateTerminalId,
                    Direction = Direction.Bidirectional
                },
                new()
                {
                    MinCount = 0,
                    TerminalId = duplicateTerminalId,
                    Direction = Direction.Output
                }
            }
        };

        var validationContext = new ValidationContext(blockTypeRequest);

        var results = blockTypeRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }
}