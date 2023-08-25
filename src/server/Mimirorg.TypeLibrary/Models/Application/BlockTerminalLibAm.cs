using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class BlockTerminalLibAm
{
    [Required]
    public int MinCount { get; set; }
    

    public int? MaxCount { get; set; }

    [Required]
    public Direction Direction { get; set; }

    [Required]
    public Guid TerminalId { get; set; }
}