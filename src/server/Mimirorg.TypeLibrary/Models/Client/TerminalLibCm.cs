namespace Mimirorg.TypeLibrary.Models.Client
{
    public class TerminalLibCm
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public TerminalLibCm Parent { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public ICollection<AttributeLibCm> Attributes { get; set; }
        public ICollection<TerminalLibCm> Children { get; set; }
    }
}
