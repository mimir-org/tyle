using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class BlockTerminalLibAm
{
    [Required]
    public int MinQuantity { get; set; }
    [Required]
    public int MaxQuantity { get; set; }
    [Required]
    public Direction Direction { get; set; }
    [Required]
    public string TerminalId { get; set; }
}