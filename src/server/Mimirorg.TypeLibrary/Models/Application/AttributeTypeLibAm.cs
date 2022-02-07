using System.ComponentModel.DataAnnotations;
using Mimirorg.Common.Enums;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeTypeLibAm
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public string ParentId { get; set; }
        public AttributeTypeLibAm Parent { get; set; }
        public ICollection<AttributeTypeLibAm> Children { get; set; }
        public Aspect Aspect { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}