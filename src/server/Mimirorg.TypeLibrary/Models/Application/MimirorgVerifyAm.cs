using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Attributes;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgVerifyAm
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        public string Email { get; set; }

        [Display(Name = "Code")]
        [Required(ErrorMessage = "{0} is required")]
        [Digit]
        public string Code { get; set; }
    }
}
