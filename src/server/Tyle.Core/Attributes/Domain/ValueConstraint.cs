using System.Globalization;
using Tyle.Core.Common.Exceptions;

namespace Tyle.Core.Attributes.Domain;

public class ValueConstraint
{
    public int Id { get; set; }
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
    public ConstraintType ConstraintType { get; private set; }
    public XsdDataType DataType { get; private set; }
    public int? MinCount { get; private set; }
    public int? MaxCount { get; } = 1;
    public string? Value { get; private set; }
    public IEnumerable<string>? AllowedValues { get; private set; }
    public string? Pattern { get; private set; }
    public decimal? MinValue { get; private set; }
    public decimal? MaxValue { get; private set; }
    public bool? MinInclusive { get; private set; }
    public bool? MaxInclusive { get; private set; }

    public void SetHasValueConstraint(XsdDataType dataType, string value)
    {
        SetAllConstraintsToNull();
        ConstraintType = ConstraintType.HasValue;
        DataType = dataType;
        MinCount = null;
        Value = ParseThenToString(value);
    }

    public void SetInConstraint(XsdDataType dataType, IEnumerable<string> allowedValues, int minCount)
    {
        SetAllConstraintsToNull();
        ConstraintType = ConstraintType.In;
        DataType = dataType;
        MinCount = minCount;
        AllowedValues = allowedValues.Select(ParseThenToString);
    }

    public void SetDataTypeConstraint(XsdDataType dataType, int minCount)
    {
        SetAllConstraintsToNull();
        ConstraintType = ConstraintType.DataType;
        DataType = dataType;
        MinCount = minCount;
    }

    public void SetPatternConstraint(string pattern, int minCount)
    {
        SetAllConstraintsToNull();
        ConstraintType = ConstraintType.Pattern;
        DataType = XsdDataType.String;
        MinCount = minCount;
        Pattern = pattern;
    }

    public void SetRangeConstraint(XsdDataType dataType, decimal? minValue, decimal? maxValue, bool? minInclusive,
        bool? maxInclusive, int minCount)
    {
        if (dataType != XsdDataType.Decimal && dataType != XsdDataType.Integer)
        {
            throw new MimirorgBadRequestException("A range constraint must have a numerical data type.");
        }
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

        SetAllConstraintsToNull();
        ConstraintType = ConstraintType.Range;
        DataType = dataType;
        MinCount = minCount;

        if (DataType == XsdDataType.Decimal)
        {
            if (minValue >= maxValue)
            {
                throw new MimirorgBadRequestException("maxValue must be greater than minValue.");
            }

            MinValue = minValue;
            MaxValue = maxValue;
        }
        else
        {
            if ((int?) minValue >= (int?) maxValue)
            {
                throw new MimirorgBadRequestException("maxValue must be greater than minValue.");
            }
            MinValue = (int?) minValue;
            MaxValue = (int?) maxValue;
        }

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

    private void SetAllConstraintsToNull()
    {
        Value = null;
        AllowedValues = null;
        Pattern = null;
        MinValue = null;
        MaxValue = null;
        MinInclusive = null;
        MaxInclusive = null;
    }
}
