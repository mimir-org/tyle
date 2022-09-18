using System.Collections.Generic;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Data.Models
{
    public class TypeReferenceDm
    {
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Source { get; set; }
        public ICollection<TypeReferenceSub> Subs { get; set; }
    }
}