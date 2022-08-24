namespace Mimirorg.TypeLibrary.Models.Client
{
    public class UnitLibCm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public ICollection<TypeReferenceCm> TypeReferences { get; set; }
        public string Description { get; set; }
        public string Kind => nameof(UnitLibCm);
    }
}