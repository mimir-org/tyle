using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TypeLibrary.Services.Attributes.Requests;

public class ValueConstraintRequest : IValidatableObject
{
    [Required]
    public ConstraintType ConstraintType { get; set; }

    public XsdDataType DataType { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Min count must be null or above zero.")]
    public int? MinCount { get; set; }

    public int? MaxCount { get; set; }

    public string? Value { get; set; }
    
    public ICollection<string>? ValueList { get; set; }

    public string? Pattern { get; set; }
    
    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }

    public bool? MinInclusive { get; set; }

    public bool? MaxInclusive { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ConstraintType == ConstraintType.HasValue)
        {
            if (MinCount != null)
            {
                yield return new ValidationResult("When the constraint type is HasValue, min count must be null.");
            }
        }
        else
        {
            if (MinCount == null)
            {
                yield return new ValidationResult("Min count must be set when the constraint type is not HasValue.");
            }
            else if (MaxCount < MinCount)
            {
                yield return new ValidationResult("The max count can't be smaller than the min count.");
            }
        }

        switch (ConstraintType)
        {
            case ConstraintType.HasValue:
                if (DataType == XsdDataType.Boolean)
                {
                    yield return new ValidationResult("Constraints of type HasValue can't have data type boolean.");
                }
                if (Value == null)
                {
                    yield return new ValidationResult("Constraints of type HasValue must specify a value.");
                }
                else if (!ValidateAgainstDataType(Value, DataType, out var result))
                {
                    yield return result;
                }
                break;
            case ConstraintType.In:
                if (DataType == XsdDataType.Boolean)
                {
                    yield return new ValidationResult("Constraints of type In can't have data type boolean.");
                }
                if (ValueList == null || ValueList.Count < 2)
                {
                    yield return new ValidationResult("Constraints of type In must specify at least two possible values.");
                }
                else
                {
                    foreach (var value in ValueList)
                    {
                        if (!ValidateAgainstDataType(value, DataType, out var result))
                        {
                            yield return result;
                        }
                    }
                }
                break;
            case ConstraintType.Pattern:
                if (Pattern == null)
                {
                    yield return new ValidationResult("Constraints of type Pattern must specify a pattern.");
                }
                else if (DataType != XsdDataType.String)
                {
                    yield return new ValidationResult("Constraints of type Pattern must have data type string.");
                }
                else if (!ValidateAgainstDataType(Pattern, DataType, out var result))
                {
                    yield return result;
                }
                break;
            case ConstraintType.Range:
                if (DataType != XsdDataType.Decimal && DataType != XsdDataType.Integer)
                {
                    yield return new ValidationResult(
                        "Constraints of type Range must have data type decimal or integer.");
                }
                if (MinValue == null && MaxValue == null)
                {
                    yield return new ValidationResult("Constraints of type Range must provide at least an upper or lower bound.");
                }
                if (MinValue != null && MinInclusive == null)
                {
                    yield return new ValidationResult("When providing a lower bound, the lower inclusive/exclusive parameter must be set.");
                }
                if (MaxValue != null && MaxInclusive == null)
                {
                    yield return new ValidationResult("When providing an upper bound, the upper inclusive/exclusive parameter must be set.");
                }
                if (DataType == XsdDataType.Decimal && MinValue >= MaxValue)
                {
                    yield return new ValidationResult("The upper bound must be larger than the lower bound.");
                }
                else if (DataType == XsdDataType.Integer && (int?)MinValue >= (int?)MaxValue)
                {
                    yield return new ValidationResult("The upper bound must be larger than the lower bound.");
                }
                break;
        }
    }

    private static bool ValidateAgainstDataType(string value, XsdDataType dataType, out ValidationResult? result)
    {
        IFormatProvider provider = CultureInfo.InvariantCulture;
        switch (dataType)
        {
            case XsdDataType.AnyUri when !Uri.TryCreate(value, UriKind.Absolute, out var _):
                result = new ValidationResult("Values with data type AnyUri must be a valid Uri.");
                return false;
            case XsdDataType.String when string.IsNullOrWhiteSpace(value):
                result = new ValidationResult("Values with data type string must not be only whitespace.");
                return false;
            // The number styles and IFormatProvider is needed to prohibit the thousands separator (,)
            case XsdDataType.Decimal when !decimal.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, provider, out var _):
                result = new ValidationResult("Values with data type decimal must be a valid decimal.");
                return false;
            case XsdDataType.Integer when !int.TryParse(value, out var _):
                result = new ValidationResult("Values with data type integer must be a valid integer.");
                return false;
            default:
                result = null;
                return true;
        }
    }
}