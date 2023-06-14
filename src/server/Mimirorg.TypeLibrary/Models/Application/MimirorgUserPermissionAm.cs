using Mimirorg.TypeLibrary.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class MimirorgUserPermissionAm
{
    [Display(Name = "UserId")]
    [Required(ErrorMessage = "{0} is required")]
    public string UserId { get; set; }

    [Display(Name = "CompanyId")]
    [Required(ErrorMessage = "{0} is required")]
    public int CompanyId { get; set; }

    [Display(Name = "Permission")]
    [Required(ErrorMessage = "{0} is required")]
    [EnumDataType(typeof(MimirorgPermission))]
    public MimirorgPermission Permission { get; set; }
}