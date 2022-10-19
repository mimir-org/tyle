using System.Collections.Generic;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Data.Models
{
    public class TypeReferenceDm
    {
        public string Id => Iri?[(Iri.LastIndexOf('/') + 1)..];
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Source { get; set; }
        public ICollection<TypeReferenceSub> Units { get; set; }
    }
}