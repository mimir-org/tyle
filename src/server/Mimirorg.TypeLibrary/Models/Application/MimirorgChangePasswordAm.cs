using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Attributes;

namespace Mimirorg.TypeLibrary.Models.Application;

public class MimirorgChangePasswordAm
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
    // ReSharper disable once UnusedMember.Global
    public string ConfirmPassword { get; set; }

    [Display(Name = "Code")]
    [Required(ErrorMessage = "{0} is required")]
    [Digit]
    public string Code { get; set; }
}