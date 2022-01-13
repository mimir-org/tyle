using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.ApplicationModels
{
    public class AuthenticateAm
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "{0} is required")]
        public int Code { get; set; }
    }
}
