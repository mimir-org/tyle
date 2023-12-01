using System.ComponentModel.DataAnnotations;
using Tyle.Application.Terminals;
using Tyle.Core.Terminals;

namespace Tyle.Application.Blocks.Requests;

public class TerminalTypeReferenceRequest : IValidatableObject
{
    [Required, Range(0, int.MaxValue, ErrorMessage = "The terminal min count cannot be negative.")]
    public int MinCount { get; set; }

    public int? MaxCount { get; set; }

    [Required, EnumDataType(typeof(Direction))]
    public Direction Direction { get; set; }

    [Required]
    public Guid TerminalId { get; set; }

    public int? ConnectionPointId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MinCount > MaxCount)
        {
            yield return new ValidationResult("The terminal min count cannot be larger than the terminal max count.");
        }

        var terminalRepository = (ITerminalRepository) validationContext.GetService(typeof(ITerminalRepository))!;

        var terminal = terminalRepository.Get(TerminalId).Result;

        if (terminal == null)
        {
            yield return new ValidationResult($"Couldn't find a terminal with id {TerminalId}.");
        }
        else if (terminal.Qualifier != Direction.Bidirectional && terminal.Qualifier != Direction)
        {
            yield return new ValidationResult($"A terminal with qualifier '{terminal.Qualifier}' can't be used as an {Direction.ToString().ToLower()} terminal.");
        }
    }
}