namespace Mimirorg.TypeLibrary.Models.Client
{
    public class TypeReferenceCm
    {
        public string Id => Iri?.Substring(Iri.LastIndexOf('/') + 1);
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Source { get; set; }
        public ICollection<TypeReferenceSub> Units { get; set; }
        public string Kind => nameof(TypeReferenceCm);
    }
}