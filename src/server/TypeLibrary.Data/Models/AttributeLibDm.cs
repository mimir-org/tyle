using System.Collections.Generic;

namespace TypeLibrary.Data.Models
{
    public class AttributeLibDm
    {
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Source { get; set; }
        public ICollection<UnitLibDm> Units { get; set; }

        public string Id => Iri?[(Iri.LastIndexOf('/') + 1)..];
    }
}