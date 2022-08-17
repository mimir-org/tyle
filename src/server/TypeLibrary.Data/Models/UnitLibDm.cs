using System.Collections.Generic;

namespace TypeLibrary.Data.Models
{
    public class UnitLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string ContentReferences { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }
    }
}