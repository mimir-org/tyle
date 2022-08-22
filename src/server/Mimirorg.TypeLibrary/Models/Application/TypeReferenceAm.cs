using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class TypeReferenceAm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Iri { get; set; }
    }
}
