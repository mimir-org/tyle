using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeLibAm
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Iri { get; set; }
        
        public ICollection<TypeReferenceAm> TypeReferences { get; set; }
        
        [Display(Name = "CompanyId")]
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than 0")]
        public int CompanyId { get; set; }

        public string Description { get; set; }

        public ICollection<AttributeUnitLibAm> AttributeUnits { get; set; }
    }
}