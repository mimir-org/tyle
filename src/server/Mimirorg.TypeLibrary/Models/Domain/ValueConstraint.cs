using System.Globalization;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;

namespace Mimirorg.TypeLibrary.Models.Domain;

public class ValueConstraint
{
    public int Id { get; set; }
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
    public ConstraintType ConstraintType { get; private set; }
    public string? Value { get; private set; }
    public ICollection<string>? AllowedValues { get; private set; }
    public XsdDataType DataType { get; private set; }
    public int? MinCount { get; private set; }
    public int? MaxCount { get; } = 1;
    public string? Pattern { get; private set; }
    public decimal? MinValue { get; private set; }
    public decimal? MaxValue { get; private set; }
    public bool? MinInclusive { get; private set; }
    public bool? MaxInclusive { get; private set; }

    public void SetConstraints(ValueConstraintRequest request)
    {
        ConstraintType = request.ConstraintType;
        DataType = request.DataType;
        MinCount = request.MinCount;
        Value = null;
        AllowedValues = null;
        Pattern = null;
        MinValue = null;
        MinInclusive = null;
        MaxInclusive = null;

        switch (ConstraintType)
        {
            case ConstraintType.HasValue:
                Value = ParseThenToString(request.Value!);
                break;
            case ConstraintType.In:
                AllowedValues = request.AllowedValues!.Select(ParseThenToString).ToList();
                break;
            case ConstraintType.Pattern:
                Pattern = request.Pattern;
                break;
            case ConstraintType.Range:
                if (request.MinValue == null && request.MaxValue == null)
                {
                    throw new MimirorgBadRequestException("At least one of minValue or maxValue must be provided.");
                }

                if ((request.MinValue != null && request.MinInclusive == null) || (request.MinValue == null && request.MinInclusive != null))
                {
                    throw new MimirorgBadRequestException("minValue and minInclusive must both be provided, or both set to null.");
                }

                if ((request.MaxValue != null && request.MaxInclusive == null) || (request.MaxValue == null && request.MaxInclusive != null))
                {
                    throw new MimirorgBadRequestException("maxValue and maxInclusive must both be provided, or both set to null.");
                }

                if (DataType == XsdDataType.Decimal)
                {
                    if (request.MinValue >= request.MaxValue)
                    {
                        throw new MimirorgBadRequestException("maxValue must be greater than minValue.");
                    }

                    MinValue = request.MinValue;
                    MaxValue = request.MaxValue;
                }
                else
                {
                    if ((int?)request.MinValue >= (int?)request.MaxValue)
                    {
                        throw new MimirorgBadRequestException("maxValue must be greater than minValue.");
                    }
                    MinValue = (int?)request.MinValue;
                    MaxValue = (int?)request.MaxValue;
                }

                MinInclusive = request.MinInclusive;
                MaxInclusive = request.MaxInclusive;

                break;
        }
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
