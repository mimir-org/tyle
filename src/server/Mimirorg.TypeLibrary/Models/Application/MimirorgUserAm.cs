using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgUserAm
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [EmailAddress(ErrorMessage = "{0} is not valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [MinLength(10, ErrorMessage = "{0} has min length {1}")]
        [Compare("ConfirmPassword", ErrorMessage = "The passwords are not equal")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "{0} is required")]
        [MinLength(10, ErrorMessage = "{0} has min length {1}")]
        [Compare("Password", ErrorMessage = "The passwords are not equal")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "{0} is required")]
        public string FirstName { get; set; }

        [Display(Name = "Lastname")]
        [Required(ErrorMessage = "{0} is required")]
        public string LastName { get; set; }

        [Display(Name = "CompanyId")]
        [Required(ErrorMessage = "{0} is required")]
        public int CompanyId { get; set; }

        [Display(Name = "Purpose")]
        [Required(ErrorMessage = "{0} is required")]
        public string Purpose { get; set; }
    }
}