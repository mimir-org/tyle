using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class UnitLibAm
{
    [Required]
    public string Name { get; set; }

    public string TypeReference { get; set; }

    public string Symbol { get; set; }

    public string Description { get; set; }
}