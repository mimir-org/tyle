using Mimirorg.TypeLibrary.Extensions;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class TypeReferenceSub
    {
        public string Id => Iri?.Substring(Iri.LastIndexOf('/') + 1);
        public string Name { get; set; }
        public string Iri { get; set; }
        public bool IsDefault { get; set; }
    }
}