using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class ValueConstraintLibAm : IValidatableObject
{
    [Required]
    public ConstraintType ConstraintType { get; set; }

    public string Value { get; set; }
    
    public ICollection<string> AllowedValues { get; set; }

    public string ClassIri { get; set; }

    public XsdDataType DataType { get; set; }

    public int? MinCount { get; set; }
    public int? MaxCount { get; set; }

    public string Pattern { get; set; }
    
    public decimal? MinValue { get; set; }
    public decimal? MaxValue { get; set; }
    public bool? MinInclusive { get; set; }
    public bool? MaxInclusive { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
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
                if (AllowedValues == null || AllowedValues.Count < 2)
                {
                    yield return new ValidationResult("Constraints of type In must specify at least two possible values.");
                }
                else
                {
                    foreach (var value in AllowedValues)
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
                // Null and type checking for this constraint type is performed in controller and ValueConstraint constructor.
                break;
        }
    }

    private static bool ValidateAgainstDataType(string value, XsdDataType dataType, out ValidationResult result)
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
            case XsdDataType.Boolean when !bool.TryParse(value, out var _): 
                result = new ValidationResult("Values with data type boolean must be true or false.");
                return false;
            default:
                result = null;
                return true;
        }
    }
}