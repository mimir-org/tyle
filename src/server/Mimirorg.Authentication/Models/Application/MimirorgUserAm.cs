using System.ComponentModel.DataAnnotations;
using Mimirorg.Authentication.Attributes;

namespace Mimirorg.Authentication.Models.Application;

public class MimirorgUserAm
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} is required")]
    [EmailAddress(ErrorMessage = "{0} is not valid")]
    public string Email { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} is required")]
    [Compare("ConfirmPassword", ErrorMessage = "The passwords are not equal")]
    [Password]
    public string Password { get; set; }

    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "{0} is required")]
    [Compare("Password", ErrorMessage = "The passwords are not equal")]
    [Password]
    public string ConfirmPassword { get; set; }

    [Display(Name = "Firstname")]
    [Required(ErrorMessage = "{0} is required")]
    public string FirstName { get; set; }

    [Display(Name = "Lastname")]
    [Required(ErrorMessage = "{0} is required")]
    public string LastName { get; set; }

    [Display(Name = "Purpose")]
    public string Purpose { get; set; }
}