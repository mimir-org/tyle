using System.ComponentModel.DataAnnotations;
using Tyle.Core.Terminals;

namespace Tyle.Application.Blocks.Requests;

public class TerminalTypeReferenceRequest : IValidatableObject
{
    [Required, Range(0, int.MaxValue, ErrorMessage = "The terminal min count cannot be negative.")]
    public int MinCount { get; }
    
    public int? MaxCount { get; }

    [Required]
    public Direction Direction { get; }

    [Required]
    public Guid TerminalId { get; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MinCount > MaxCount)
        {
            yield return new ValidationResult("The terminal min count cannot be larger than the terminal max count.");
        }
    }
}