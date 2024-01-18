using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Tyle.Application.Blocks.Requests;
using Tyle.Application.Common.Requests;
using Tyle.Core.Terminals;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Services.Blocks.Requests;

public class BlockTypeRequestTests : UnitTest<RequestTestFixture>
{
    private readonly IServiceProvider _serviceProvider;

    public BlockTypeRequestTests(RequestTestFixture fixture) : base(fixture)
    {
        _serviceProvider = fixture.ServiceProvider.Object;
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

        var validationContext = new ValidationContext(blockTypeRequest, _serviceProvider, null);

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

        var validationContext = new ValidationContext(blockTypeRequest, _serviceProvider, null);

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

        var validationContext = new ValidationContext(blockTypeRequest, _serviceProvider, null);

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

        var validationContext = new ValidationContext(blockTypeRequest, _serviceProvider, null);

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

        var validationContext = new ValidationContext(blockTypeRequest, _serviceProvider, null);

        var results = blockTypeRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }
}