using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class AttributeLibAm
{
    [Required]
    public string Name { get; set; }

    public string TypeReference { get; set; }

    public string Description { get; set; }

    public ICollection<AttributeUnitLibAm> AttributeUnits { get; set; }
}