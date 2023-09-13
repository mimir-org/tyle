using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Castle.Components.DictionaryAdapter.Xml;
using J2N.Text;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

public class ValueConstraintRequestTests : UnitTest<MimirorgCommonFixture>
{
    private readonly MimirorgCommonFixture _fixture;

    public ValueConstraintRequestTests(MimirorgCommonFixture fixture) : base(fixture)
    {
        _fixture = fixture;
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
            AllowedValues = new List<string>() { "A", "B", "C" }
        };

        validationContext = new ValidationContext(valueConstraintRequest);

        results = valueConstraintRequest.Validate(validationContext);

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

    [Theory]
    [MemberData(nameof(AllowedValuesExamples))]
    public void AtLeastTwoValuesMustBeProvidedForConstraintTypeIn(ICollection<string> values, bool result)
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.In,
            DataType = XsdDataType.String,
            AllowedValues = values,
            MinCount = 1
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.Equal(result, results.IsNullOrEmpty());
    }

    public static IEnumerable<object[]> AllowedValuesExamples()
    {
        yield return new object[] { null, false };
        yield return new object[] {new List<string> {"single"}, false};
        yield return new object[] {new List<string> {"one", "two"}, true};
        yield return new object[] {new List<string> {"one", "two", "three"}, true};
    }

    [Fact]
    public void DataTypeValidationFailsOnWrongDataTypeForConstraintTypeIn()
    {
        var valueConstraintRequest = new ValueConstraintRequest
        {
            ConstraintType = ConstraintType.In,
            DataType = XsdDataType.Integer,
            AllowedValues = new List<string> { "1", "3", "five" },
            MinCount = 1
        };

        var validationContext = new ValidationContext(valueConstraintRequest);

        var results = valueConstraintRequest.Validate(validationContext);

        Assert.False(results.IsNullOrEmpty());
    }

    [Theory]
    [InlineData(null, false)]
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
            Pattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$",
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
