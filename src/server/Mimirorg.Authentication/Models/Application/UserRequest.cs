using System.ComponentModel.DataAnnotations;
using Mimirorg.Authentication.Attributes;

namespace Mimirorg.Authentication.Models.Application;

public class UserRequest
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "{0} is required")]
    [EmailAddress(ErrorMessage = "{0} is not valid")]
    public required string Email { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} is required")]
    [Compare("ConfirmPassword", ErrorMessage = "The passwords are not equal")]
    [Password]
    public required string Password { get; set; }

    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "{0} is required")]
    [Compare("Password", ErrorMessage = "The passwords are not equal")]
    [Password]
    public required string ConfirmPassword { get; set; }

    [Display(Name = "Firstname")]
    [Required(ErrorMessage = "{0} is required")]
    public required string FirstName { get; set; }

    [Display(Name = "Lastname")]
    [Required(ErrorMessage = "{0} is required")]
    public required string LastName { get; set; }

    [Display(Name = "Purpose")]
    [Required(ErrorMessage = "{0} is required")]
    public required string Purpose { get; set; }
}