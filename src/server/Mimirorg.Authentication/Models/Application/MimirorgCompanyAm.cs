using System.ComponentModel.DataAnnotations;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Models.Application
{
    public class MimirorgCompanyAm
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [Display(Name = "DisplayName")]
        [Required(ErrorMessage = "{0} is required")]
        public string DisplayName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "ManagerId")]
        [Required(ErrorMessage = "{0} is required")]
        public string ManagerId { get; set; }

        public MimirorgCompany ToDomainModel()
        {
            return new MimirorgCompany
            {
                Name = Name,
                DisplayName = DisplayName,
                Description = Description,
                ManagerId = ManagerId
            };
        }
    }
}
