using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class AttributeUnitLibAm
{
    [Required]
    public bool IsDefault { get; set; }
    [Required]
    public string UnitId { get; set; }
}