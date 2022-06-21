using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgCompanyAuthAm
    {
        [Display(Name = "Domain")]
        [Required(ErrorMessage = "{0} is required")]
        public string Domain { get; set; }

        [Display(Name = "Secret")]
        [Required(ErrorMessage = "{0} is required")]
        public string Secret { get; set; }
    }
}