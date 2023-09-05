using System.ComponentModel.DataAnnotations;
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
                else
                {
                    switch (DataType)
                    {
                        case XsdDataType.AnyUri when !Uri.TryCreate(Value, UriKind.Absolute, out var _):
                            yield return new ValidationResult("Values with data type AnyUri must be a valid Uri.");
                            break;
                        case XsdDataType.Decimal:
                            break;
                    }
                }
        }
    }
}