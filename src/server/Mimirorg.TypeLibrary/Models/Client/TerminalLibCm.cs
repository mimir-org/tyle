namespace Mimirorg.TypeLibrary.Models.Client
{
    public class TerminalLibCm
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string ParentName { get; set; }
        public string ParentIri { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public string Iri { get; set; }
        public ICollection<TypeReferenceCm> TypeReferences { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public int CompanyId { get; set; }
        public ICollection<AttributeLibCm> Attributes { get; set; }
        public ICollection<TerminalLibCm> Children { get; set; }
        public string Kind => nameof(TerminalLibCm);
    }
}