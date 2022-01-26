using System.Collections.Generic;

namespace TypeLibrary.Models.Data
{
    public class TerminalType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public string Color { get; set; }
        public string ParentId { get; set; }
        public TerminalType Parent { get; set; }
        
        public ICollection<TerminalType> Children { get; set; }
        public ICollection<AttributeType> Attributes { get; set; }
        public ICollection<NodeTypeTerminalType> NodeTypes { get; set; }
        public ICollection<InterfaceType> InterfaceTypes { get; set; }
        public ICollection<TransportType> TransportTypes { get; set; }
    }
}