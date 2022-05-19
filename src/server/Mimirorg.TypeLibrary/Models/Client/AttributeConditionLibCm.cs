namespace Mimirorg.TypeLibrary.Models.Client
{
    public class AttributeConditionLibCm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public ICollection<string> ContentReferences { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string Kind => nameof(AttributeConditionLibCm);
    }
}