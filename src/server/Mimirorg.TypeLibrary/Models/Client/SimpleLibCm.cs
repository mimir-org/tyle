namespace Mimirorg.TypeLibrary.Models.Client
{
    public class SimpleLibCm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public ICollection<string> ContentReferences { get; set; }
        public string Description { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public ICollection<AttributeLibCm> Attributes { get; set; }

        public string Kind => nameof(SimpleLibCm);
    }
}