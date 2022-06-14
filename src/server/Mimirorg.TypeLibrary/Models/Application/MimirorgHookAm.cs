using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class MimirorgHookAm
    {
        [Display(Name = "CompanyId")]
        [Required(ErrorMessage = "{0} is required")]
        public int CompanyId { get; set; }

        [Display(Name = "Key")]
        [Required(ErrorMessage = "{0} is required")]
        public CacheKey Key { get; set; }

        [Display(Name = "Iri")]
        [Required(ErrorMessage = "{0} is required")]
        public string Iri { get; set; }
    }
}
