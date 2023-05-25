using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application;

public class AspectObjectTerminalLibAm
{
    [Required]
    public int MinQuantity { get; set; }
    [Required]
    public int MaxQuantity { get; set; }
    [Required]
    public ConnectorDirection ConnectorDirection { get; set; }
    [Required]
    public string TerminalId { get; set; }
}