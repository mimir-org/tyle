using System.ComponentModel.DataAnnotations;
using TypeScriptBuilder;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeLibAm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Iri { get; set; }
        [Required]
        public string Source { get; set; }

        public ICollection<UnitLibAm> Units { get; set; }

        [TSExclude]
        public string Id => Iri?[(Iri.LastIndexOf('/') + 1)..];
    }
}