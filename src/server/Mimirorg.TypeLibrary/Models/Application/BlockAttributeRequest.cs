using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class BlockAttributeRequest : IValidatableObject
{
    [Required, Range(0, int.MaxValue, ErrorMessage = "The attribute min count cannot be negative.")]
    public int MinCount { get; set; }

    public int? MaxCount { get; set; }

    [Required]
    public Guid AttributeId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MinCount > MaxCount)
        {
            yield return new ValidationResult("The attribute min count cannot be larger than the attribute max count.");
        }
    }
}