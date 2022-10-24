using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgAuthenticateAm
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }
    }
}