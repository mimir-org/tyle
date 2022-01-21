using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Models.Application
{
    public class MimirorgUserRoleAm
    {
        [Display(Name = "UserId")]
        [Required(ErrorMessage = "{0} is required")]
        public string UserId { get; set; }

        [Display(Name = "MimirorgRole")]
        [Required(ErrorMessage = "{0} is required")]
        public MimirorgRoleAm MimirorgRole { get; set; }
    }
}
