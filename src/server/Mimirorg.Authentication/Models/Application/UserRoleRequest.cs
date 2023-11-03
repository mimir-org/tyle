using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Models.Application;

public class UserRoleRequest
{
    [Display(Name = "UserId")]
    [Required(ErrorMessage = "{0} is required")]
    public required string UserId { get; set; }

    [Display(Name = "MimirorgRoleId")]
    [Required(ErrorMessage = "{0} is required")]
    public required string MimirorgRoleId { get; set; }
}