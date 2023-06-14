using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application;

public class MimirorgUserRoleAm
{
    [Display(Name = "UserId")]
    [Required(ErrorMessage = "{0} is required")]
    public string UserId { get; set; }

    [Display(Name = "MimirorgRoleId")]
    [Required(ErrorMessage = "{0} is required")]
    public string MimirorgRoleId { get; set; }
}