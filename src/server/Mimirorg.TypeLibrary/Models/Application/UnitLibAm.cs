using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class UnitLibAm
{
    [Required]
    public string Name { get; set; }

    public string TypeReference { get; set; }

    public string Symbol { get; set; }

    [Display(Name = "CompanyId")]
    [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
    public int? CompanyId { get; set; }

    public string Description { get; set; }
}