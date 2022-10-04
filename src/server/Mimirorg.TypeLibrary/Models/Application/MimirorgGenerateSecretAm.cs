using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgGenerateSecretAm
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        public string Email { get; set; }

        [Display(Name = "TokenType")]
        [Required(ErrorMessage = "{0} is required")]
        public MimirorgTokenType TokenType { get; set; }
    }
}
