using System.ComponentModel.DataAnnotations;
using Tyle.Core.Terminals;

namespace Tyle.Application.Blocks.Requests;

public class TerminalTypeReferenceRequest : IValidatableObject
{
    [Required, Range(0, int.MaxValue, ErrorMessage = "The terminal min count cannot be negative.")]
    public int MinCount { get; set; }

    public int? MaxCount { get; set; }

    [Required]
    [EnumDataType(typeof(Direction))]
    public Direction Direction { get; set; }

    [Required]
    public Guid TerminalId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MinCount > MaxCount)
        {
            yield return new ValidationResult("The terminal min count cannot be larger than the terminal max count.");
        }
    }
}