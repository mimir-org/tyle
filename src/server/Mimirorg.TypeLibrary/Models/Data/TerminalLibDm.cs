namespace Mimirorg.TypeLibrary.Models.Data
{
    public class TerminalLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public string Color { get; set; }
        public string ParentId { get; set; }
        public TerminalLibDm Parent { get; set; }
        
        public ICollection<TerminalLibDm> Children { get; set; }
        public ICollection<AttributeLibDm> Attributes { get; set; }
        public ICollection<NodeTerminalLibDm> Nodes { get; set; }
        public ICollection<InterfaceLibDm> Interfaces { get; set; }
        public ICollection<TransportLibDm> Transports { get; set; }
    }
}