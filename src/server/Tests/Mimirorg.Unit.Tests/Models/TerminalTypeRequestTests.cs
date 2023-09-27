using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

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
            ClassifierReferenceIds = new List<int> { 1, 2, 5, 7 },
            Qualifier = Direction.Bidirectional,
            TerminalAttributes = new List<TerminalAttributeRequest>
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
            ClassifierReferenceIds = new List<int> { 1, 5, 5, 7 },
            Qualifier = Direction.Bidirectional,
            TerminalAttributes = new List<TerminalAttributeRequest>
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
            ClassifierReferenceIds = new List<int> { 1, 2, 5, 7 },
            Qualifier = Direction.Bidirectional,
            TerminalAttributes = new List<TerminalAttributeRequest>
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