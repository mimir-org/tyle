using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgAuthenticateSecretAm
    {
        [Display(Name = "Secret")]
        [Required(ErrorMessage = "{0} is required")]
        public string Secret { get; set; }
    }
}