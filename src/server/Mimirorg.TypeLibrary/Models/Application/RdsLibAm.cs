using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class RdsLibAm
{
    [Required]
    public string RdsCode { get; set; }

    [Required]
    public string Name { get; set; }

    public string TypeReference { get; set; }

    [Display(Name = "CompanyId")]
    [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
    public int? CompanyId { get; set; }

    public string Description { get; set; }

    [Required]
    public string CategoryId { get; set; }
}