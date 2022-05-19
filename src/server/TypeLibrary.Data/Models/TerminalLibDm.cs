using System;
using System.Collections.Generic;

namespace TypeLibrary.Data.Models
{
    public class TerminalLibDm
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public TerminalLibDm Parent { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public string Iri { get; set; }
        public string ContentReferences { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public ICollection<AttributeLibDm> Attributes { get; set; }
        public ICollection<TerminalLibDm> Children { get; set; }
        public ICollection<NodeTerminalLibDm> TerminalNodes { get; set; }
        public ICollection<InterfaceLibDm> Interfaces { get; set; }
        public ICollection<TransportLibDm> Transports { get; set; }
    }
}