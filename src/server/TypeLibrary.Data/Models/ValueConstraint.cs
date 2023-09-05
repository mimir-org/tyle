using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models;

public class ValueConstraint
{
    public int Id { get; set; }
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
    public ConstraintType ConstraintType { get; }
    public string? Value { get; }
    public ICollection<string>? AllowedValues { get; }
    public Uri? ClassIri { get; }
    public XsdDataType? DataType { get; }
    public int? MinCount { get; }
    public int? MaxCount { get; }
    public string? Pattern { get; }
    public decimal? MinValue { get; }
    public decimal? MaxValue { get; }
    public bool? MinInclusive { get; }
    public bool? MaxInclusive { get; }

    public ValueConstraint(XsdDataType dataType, string value)
    {
        ConstraintType = ConstraintType.HasValue;
        DataType = dataType;
        Value = ParseThenToString(value);
    }

    public ValueConstraint(XsdDataType dataType, ICollection<string> value)
    {
        ConstraintType = ConstraintType.In;
        DataType = dataType;
        AllowedValues = value.Select(ParseThenToString).ToList();
    }

    public ValueConstraint(Uri classIri)
    {
        ConstraintType = ConstraintType.Class;
        DataType = XsdDataType.AnyUri;
        ClassIri = classIri;
    }

    public ValueConstraint(XsdDataType dataType)
    {
        ConstraintType = ConstraintType.DataType;
        DataType = dataType;
    }

    public ValueConstraint(string pattern)
    {
        ConstraintType = ConstraintType.Pattern;
        DataType = XsdDataType.String;
        Pattern = pattern;
    }

    public ValueConstraint(decimal? minValue, decimal? maxValue, bool? minInclusive, bool? maxInclusive)
    {
        if (minValue == null && maxValue == null)
        {
            throw new MimirorgBadRequestException("At least one of minValue or maxValue must be provided.");
        }

        if ((minValue != null && minInclusive == null) || (minValue == null && minInclusive != null))
        {
            throw new MimirorgBadRequestException("minValue and minInclusive must both be provided, or both set to null.");
        }

        if ((maxValue != null && maxInclusive == null) || (maxValue == null && maxInclusive != null))
        {
            throw new MimirorgBadRequestException("maxValue and maxInclusive must both be provided, or both set to null.");
        }

        if (minValue >= maxValue)
        {
            throw new MimirorgBadRequestException("maxValue must be greater than minValue.");
        }

        ConstraintType = ConstraintType.Range;
        DataType = XsdDataType.Decimal;
        MinValue = minValue;
        MaxValue = maxValue;
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
    }

    public ValueConstraint(int? minValue, int? maxValue, bool? minInclusive, bool? maxInclusive)
    {
        if (minValue == null && maxValue == null)
        {
            throw new MimirorgBadRequestException("At least one of minValue or maxValue must be provided.");
        }

        if ((minValue != null && minInclusive == null) || (minValue == null && minInclusive != null))
        {
            throw new MimirorgBadRequestException("minValue and minInclusive must both be provided, or both set to null.");
        }

        if ((maxValue != null && maxInclusive == null) || (maxValue == null && maxInclusive != null))
        {
            throw new MimirorgBadRequestException("maxValue and maxInclusive must both be provided, or both set to null.");
        }

        if (minValue >= maxValue)
        {
            throw new MimirorgBadRequestException("maxValue must be greater than minValue.");
        }

        ConstraintType = ConstraintType.Range;
        DataType = XsdDataType.Integer;
        MinValue = minValue;
        MaxValue = maxValue;
        MinInclusive = minInclusive;
        MaxInclusive = maxInclusive;
    }

    private string ParseThenToString(string value)
    {
        switch (DataType)
        {
            case XsdDataType.Decimal:
                return decimal.Parse(value, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            case XsdDataType.Integer:
                return int.Parse(value).ToString(CultureInfo.InvariantCulture);
            case XsdDataType.Boolean:
                return bool.Parse(value).ToString(CultureInfo.InvariantCulture);
            default:
                return value;
        }
    }
}
