using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgRoleAm
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "{0} is required")]
        public string Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
    }
}
