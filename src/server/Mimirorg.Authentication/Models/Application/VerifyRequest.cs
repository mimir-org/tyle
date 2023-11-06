using System.ComponentModel.DataAnnotations;
using Mimirorg.Authentication.Attributes;

namespace Mimirorg.Authentication.Models.Application;

public class VerifyRequest
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} is required")]
    public required string Email { get; set; }

    [Display(Name = "Code")]
    [Required(ErrorMessage = "{0} is required")]
    [Digit]
    public required string Code { get; set; }
}