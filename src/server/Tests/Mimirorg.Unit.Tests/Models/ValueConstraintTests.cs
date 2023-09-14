using System.Collections.Generic;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Tyle.Core.Attributes.Domain;
using Tyle.Core.Common.Exceptions;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

public class ValueConstraintTests : UnitTest<MimirorgCommonFixture>
{
    public ValueConstraintTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Theory]
    [MemberData(nameof(ValidConstraints))]
    public void ValidConstraintsShouldNotThrowExceptions(ValueConstraintRequest request)
    {
        var valueConstraint = new ValueConstraint();
        var exception = Record.Exception(() => valueConstraint.SetConstraints(request));
        Assert.Null(exception);
    }

    public static IEnumerable<object[]> ValidConstraints()
    {
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.HasValue,
                Value = "12",
                DataType = XsdDataType.Integer
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.In,
                AllowedValues = new List<string> { "A", "B", "C" },
                DataType = XsdDataType.String,
                MinCount = 1
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.DataType,
                DataType = XsdDataType.Boolean,
                MinCount = 1
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.Pattern,
                DataType = XsdDataType.String,
                MinCount = 1,
                Pattern = "[0-9]+"
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.Range,
                DataType = XsdDataType.Decimal,
                MinCount = 1,
                MinValue = -2.3M,
                MaxValue = 14.2M,
                MinInclusive = true,
                MaxInclusive = true
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.Range,
                DataType = XsdDataType.Integer,
                MinCount = 1,
                MinValue = -2,
                MinInclusive = true
            }
        };
    }

    [Theory]
    [MemberData(nameof(InvalidRangeConstraints))]
    public void InvalidRangeConstraintsThrowsExceptions(ValueConstraintRequest request)
    {
        var valueConstraint = new ValueConstraint();
        Assert.Throws<MimirorgBadRequestException>(() => valueConstraint.SetConstraints(request));
    }

    public static IEnumerable<object[]> InvalidRangeConstraints()
    {
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.Range,
                DataType = XsdDataType.Decimal,
                MinCount = 1
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.Range,
                DataType = XsdDataType.Decimal,
                MinCount = 1,
                MinValue = -2.3M,
                MaxValue = 14.2M,
                MaxInclusive = true
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.Range,
                DataType = XsdDataType.Decimal,
                MinCount = 1,
                MinValue = -2.3M,
                MinInclusive = true,
                MaxInclusive = true
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.Range,
                DataType = XsdDataType.Decimal,
                MinCount = 1,
                MinValue = 22.3M,
                MaxValue = 14.2M,
                MinInclusive = true,
                MaxInclusive = true
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.Range,
                DataType = XsdDataType.Integer,
                MinCount = 1,
                MinValue = 2,
                MaxValue = 1,
                MinInclusive = true,
                MaxInclusive = true
            }
        };
        yield return new object[]
        {
            new ValueConstraintRequest
            {
                ConstraintType = ConstraintType.Range,
                DataType = XsdDataType.Integer,
                MinCount = 1,
                MinValue = 2.3M,
                MaxValue = 2.5M,
                MinInclusive = false,
                MaxInclusive = false
            }
        };
    }
}
