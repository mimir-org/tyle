using System;
using Mimirorg.Common.Exceptions;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Domain;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

public class TerminalAttributeTypeReferenceTests : UnitTest<MimirorgCommonFixture>
{
    public TerminalAttributeTypeReferenceTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(0, null)]
    [InlineData(1, null)]
    [InlineData(1, 1)]
    [InlineData(1, 5)]
    public void ConstructorShouldNotThrowExceptionWithValidInput(int minCount, int? maxCount)
    {
        var exception = Record.Exception(() =>
            new TerminalAttributeTypeReference(Guid.NewGuid(), Guid.NewGuid(), minCount, maxCount));

        Assert.Null(exception);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(-1, 2)]
    [InlineData(-1, null)]
    [InlineData(0, -1)]
    [InlineData(5, 1)]
    public void ConstructorShouldThrowExceptionWhenMinCountIsNegativeOrLargerThanMaxCount(int minCount, int? maxCount)
    {
        Assert.Throws<MimirorgBadRequestException>(() =>
            new TerminalAttributeTypeReference(Guid.NewGuid(), Guid.NewGuid(), minCount, maxCount));
    }
}
