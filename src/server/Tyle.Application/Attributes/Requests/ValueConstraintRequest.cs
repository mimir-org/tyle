using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Attributes;

namespace Tyle.Application.Attributes.Requests;

public class ValueConstraintRequest : IValidatableObject
{
    [Required, EnumDataType(typeof(ConstraintType))]
    public ConstraintType ConstraintType { get; set; }

    [Required, EnumDataType(typeof(XsdDataType))]
    public XsdDataType DataType { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Min count must be null or a non-negative integer.")]
    public int? MinCount { get; set; }

    public int? MaxCount { get; set; }

    [MaxLength(StringLengthConstants.ValueLength)]
    public string? Value { get; set; }

    public ICollection<string> ValueList { get; set; } = new List<string>();

    [MaxLength(StringLengthConstants.ValueLength)]
    public string? Pattern { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }

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

                if (ValueList.Count < 2)
                {
                    yield return new ValidationResult("Constraints of type In must specify at least two possible values.");
                }
                else
                {
                    var dataTypeValidationResults = 0;

                    foreach (var value in ValueList)
                    {
                        if (value.Length > StringLengthConstants.ValueLength)
                        {
                            yield return new ValidationResult($"Values in a value list can at most be {StringLengthConstants.ValueLength} characters long.");
                        }

                        if (!ValidateAgainstDataType(value, DataType, out var result))
                        {
                            dataTypeValidationResults++;
                            yield return result;
                        }
                    }

                    if (dataTypeValidationResults == 0)
                    {
                        switch (DataType)
                        {
                            case XsdDataType.String:
                                foreach (var validationResult in UniqueCollectionValidator.Validate(ValueList, "Value list entry"))
                                {
                                    yield return validationResult;
                                }

                                break;
                            case XsdDataType.Decimal:
                                var decimalValueList = ValueList.Select(x => decimal.Parse(x, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture));
                                foreach (var validationResult in UniqueCollectionValidator.Validate(decimalValueList, "Value list entry"))
                                {
                                    yield return validationResult;
                                }
                                break;
                            case XsdDataType.Integer:
                                var integerValueList = ValueList.Select(int.Parse);
                                foreach (var validationResult in UniqueCollectionValidator.Validate(integerValueList, "Value list entry"))
                                {
                                    yield return validationResult;
                                }
                                break;
                            case XsdDataType.AnyUri:
                                var iriValueList = ValueList.Select(x => new Uri(x));
                                foreach (var validationResult in UniqueCollectionValidator.Validate(iriValueList, "Value list entry"))
                                {
                                    yield return validationResult;
                                }
                                break;
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
                if (DataType == XsdDataType.Decimal && MinValue >= MaxValue)
                {
                    yield return new ValidationResult("The upper bound must be larger than the lower bound.");
                }
                else if (DataType == XsdDataType.Integer && (int?) MinValue >= (int?) MaxValue)
                {
                    yield return new ValidationResult("The upper bound must be larger than the lower bound.");
                }
                break;
        }
    }

    private static bool ValidateAgainstDataType(string value, XsdDataType dataType, out ValidationResult? result)
    {
        switch (dataType)
        {
            case XsdDataType.AnyUri when !Uri.TryCreate(value, UriKind.Absolute, out var _):
                result = new ValidationResult("Values with data type AnyUri must be a valid Uri.");
                return false;
            case XsdDataType.String when string.IsNullOrWhiteSpace(value):
                result = new ValidationResult("Values with data type string must not be only whitespace.");
                return false;
            // The number styles and IFormatProvider is needed to prohibit the thousands separator (,)
            case XsdDataType.Decimal when !decimal.TryParse(value, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var _):
                result = new ValidationResult("Values with data type decimal must be a valid decimal.");
                return false;
            case XsdDataType.Integer when !int.TryParse(value, out var _):
                result = new ValidationResult("Values with data type integer must be a valid integer.");
                return false;
            case XsdDataType.Boolean when !bool.TryParse(value, out var _):
                result = new ValidationResult("Values with data type boolean must be a valid boolean.");
                return false;
            default:
                result = null;
                return true;
        }
    }
}