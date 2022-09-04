namespace Mimirorg.TypeLibrary.Models.Client
{
    public class TypeReferenceCm
    {
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Source { get; set; }
        public string SubName { get; set; }
        public string SubIri { get; set; }
        public string Kind => nameof(TypeReferenceCm);
    }
}