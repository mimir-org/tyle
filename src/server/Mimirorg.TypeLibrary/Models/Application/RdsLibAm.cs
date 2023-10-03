using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class RdsLibAm
{
    [Required]
    public string RdsCode { get; set; }

    [Required]
    public string Name { get; set; }

    public string TypeReference { get; set; }

    public string Description { get; set; }
}