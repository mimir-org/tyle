using System.ComponentModel.DataAnnotations;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Extensions;

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

        [Display(Name = "Secret")]
        public string Secret { get; set; }

        [Display(Name = "Domain")]
        [Required(ErrorMessage = "{0} is required")]
        public string Domain { get; set; }

        [Display(Name = "Logo")]
        public string Logo { get; set; }
        
        [Display(Name = "Iris")]
        public ICollection<string> Iris { get; set; }

        public MimirorgCompany ToDomainModel()
        {
            return new MimirorgCompany
            {
                Name = Name,
                DisplayName = DisplayName,
                Description = Description,
                ManagerId = ManagerId,
                Secret = Secret,
                Domain = Domain,
                Logo = Logo,
                Iris = Iris?.ConvertToString()
            };
        }
    }
}