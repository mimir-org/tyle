using System.Collections.Generic;

namespace TypeLibrary.Models.Models.Data
{
    public class TerminalDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public string Color { get; set; }
        public string ParentId { get; set; }
        public TerminalDm Parent { get; set; }
        
        public ICollection<TerminalDm> Children { get; set; }
        public ICollection<AttributeDm> Attributes { get; set; }
        public ICollection<NodeTerminalDm> Nodes { get; set; }
        public ICollection<InterfaceDm> Interfaces { get; set; }
        public ICollection<TransportDm> Transports { get; set; }
    }
}