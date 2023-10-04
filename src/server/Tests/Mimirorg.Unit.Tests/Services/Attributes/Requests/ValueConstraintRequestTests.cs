using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using TypeLibrary.Core.Attributes;
using TypeLibrary.Services.Attributes.Requests;
using TypeLibrary.Services.Common;
using Xunit;

namespace Mimirorg.Test.Unit.Services.Attributes.Requests;

public class ValueConstraintRequestTests : UnitTest<MimirorgCommonFixture>
{
    public ValueConstraintRequestTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Theory]
    [InlineData(null, true)]
    [InlineData(0, false)]
    [InlineData(1, false)]
    public void HasValueDemandsMinCountIsNull(int? minCount, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.HasValue,
            Value = "15",
            DataType = XsdDataType.Integer,
            MinCount = minCount
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData(0, true)]
    [InlineData(1, true)]
    public void MinCountMustBeSetForConstraintsOtherThanHasValue(int? minCount, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.Range,
            DataType = XsdDataType.Integer,
            MinCount = minCount,
            MinValue = 15,
            MinInclusive = true
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());

        valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.In,
            DataType = XsdDataType.String,
            MinCount = minCount,
            ValueList = new List<string>() { "A", "B", "C" }
        };

        validationContext = new ValidationContext(valueConstraintRequest);

        results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    [Theory]
    [InlineData(0, 2, true)]
    [InlineData(3, 3, true)]
    [InlineData(1, null, true)]
    [InlineData(5, 1, false)]
    public void MinCountMustBeSmallerThanOrEqualToMaxCount(int? minCount, int? maxCount, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.DataType,
            DataType = XsdDataType.Integer,
            MinCount = minCount,
            MaxCount = maxCount
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("test value", true)]
    public void ValueMustBeSetForConstraintTypeHasValue(string value, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.HasValue,
            DataType = XsdDataType.String,
            Value = value
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    [Fact]
    public void ConstraintTypeInCanNotHaveBooleanDataType()
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.In,
            DataType = XsdDataType.Boolean,
            ValueList = new List<string> { "false", "true" },
            MinCount = 1
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }

    [Theory]
    [InlineData(StringLengthConstants.ValueLength - 1, true)]
    [InlineData(StringLengthConstants.ValueLength, true)]
    [InlineData(StringLengthConstants.ValueLength + 1, false)]
    public void DataValueLengthForConstraintTypeInValidatesCorrectly(int length, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.In,
            DataType = XsdDataType.String,
            ValueList = new List<string> { "123", new string('*', length) },
            MinCount = 1
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    [Theory]
    [MemberData(nameof(AllowedValuesExamples))]
    public void AtLeastTwoValuesMustBeProvidedForConstraintTypeIn(ICollection<string> values, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.In,
            DataType = XsdDataType.String,
            ValueList = values,
            MinCount = 1
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    public static IEnumerable<object[]> AllowedValuesExamples()
    {
        yield return new object[] { new List<string>(), false };
        yield return new object[] { new List<string> { "single" }, false };
        yield return new object[] { new List<string> { "one", "two" }, true };
        yield return new object[] { new List<string> { "one", "two", "three" }, true };
    }

    [Fact]
    public void DataTypeValidationFailsOnWrongDataTypeForConstraintTypeIn()
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.In,
            DataType = XsdDataType.Integer,
            ValueList = new List<string> { "1", "3", "five" },
            MinCount = 1
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("[0-9]+", true)]
    public void ValueMustBeSetForConstraintTypePattern(string pattern, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.Pattern,
            DataType = XsdDataType.String,
            Pattern = pattern,
            MinCount = 1
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    [Fact]
    public void DataTypeMustBeStringForConstraintTypePattern()
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.Pattern,
            DataType = XsdDataType.Boolean,
            Pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
            MinCount = 1
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }

    [Theory]
    [InlineData(XsdDataType.Integer, true)]
    [InlineData(XsdDataType.Decimal, true)]
    [InlineData(XsdDataType.String, false)]
    public void DataTypeMustBeNumericalForConstraintTypeRange(XsdDataType dataType, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.Range,
            DataType = dataType,
            MinCount = 1,
            MinValue = 12,
            MaxValue = 15,
            MinInclusive = false,
            MaxInclusive = true
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    [Theory]
    [MemberData(nameof(RangeBoundsExamples))]
    public void RangeBoundsValidateCorrectly(decimal? minValue, decimal? maxValue, bool? minInclusive, bool? maxInclusive, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.Range,
            DataType = XsdDataType.Decimal,
            MinCount = 1,
            MinValue = minValue,
            MaxValue = maxValue,
            MinInclusive = minInclusive,
            MaxInclusive = maxInclusive
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    public static IEnumerable<object?[]> RangeBoundsExamples()
    {
        yield return new object?[] { null, null, null, null, false };
        yield return new object?[] { 1M, null, false, null, true };
        yield return new object?[] { null, 2000M, null, true, true };
        yield return new object?[] { 23M, 24M, null, true, false };
        yield return new object?[] { null, 15M, null, null, false };
        yield return new object?[] { 23.4M, 23.4M, true, true, false };
        yield return new object?[] { 23.4M, 23.5M, true, true, true };
        yield return new object?[] { 23.5M, 23.4M, true, true, false };
    }

    [Theory]
    [MemberData(nameof(RangeBoundsExamplesWithIntConversion))]
    public void RangeBoundsValidateCorrectlyWithIntConversion(decimal? minValue, decimal? maxValue, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.Range,
            DataType = XsdDataType.Integer,
            MinCount = 1,
            MinValue = minValue,
            MaxValue = maxValue,
            MinInclusive = false,
            MaxInclusive = false
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    public static IEnumerable<object?[]> RangeBoundsExamplesWithIntConversion()
    {
        yield return new object?[] { 1M, 2M, true };
        yield return new object?[] { 1M, 1.2M, false };
        yield return new object?[] { 1M, 1.7M, false };
        yield return new object?[] { 2.6M, 2.7M, false };
    }

    [Theory]
    [InlineData("", XsdDataType.String, false)]
    [InlineData("test", XsdDataType.String, true)]
    [InlineData("www.vg.no", XsdDataType.AnyUri, false)]
    [InlineData("http://example.com/123", XsdDataType.AnyUri, true)]
    [InlineData("15", XsdDataType.Decimal, true)]
    [InlineData("-11.5", XsdDataType.Decimal, true)]
    [InlineData("15,342", XsdDataType.Decimal, false)]
    [InlineData("15", XsdDataType.Integer, true)]
    [InlineData("-15", XsdDataType.Integer, true)]
    [InlineData("15.2", XsdDataType.Integer, false)]
    [InlineData("false", XsdDataType.Boolean, true)]
    [InlineData("maybe", XsdDataType.Boolean, false)]
    public void DataTypeValidationWorksCorrectly(string value, XsdDataType dataType, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.HasValue,
            DataType = dataType,
            Value = value
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }
}