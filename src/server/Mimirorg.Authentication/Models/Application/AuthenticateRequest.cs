using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Models.Application;

public class AuthenticateRequest
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} is required")]
    public required string Email { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} is required")]
    public required string Password { get; set; }

    [Display(Name = "Code")]
    public required string Code { get; set; }
}