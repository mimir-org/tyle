using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class AttributeAspectLibCm
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public AttributeAspectLibCm Parent { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public Aspect Aspect { get; set; }
        public string Description { get; set; }

        public ICollection<AttributeAspectLibCm> Children { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
