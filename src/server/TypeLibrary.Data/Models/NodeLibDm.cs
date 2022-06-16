using System;
using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class NodeLibDm
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public NodeLibDm Parent { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public string Iri { get; set; }
        public string AttributeAspectIri { get; set; }
        public string ContentReferences { get; set; }
        public string RdsCode { get; set; }
        public string RdsName { get; set; }
        public string PurposeName { get; set; }
        public Aspect Aspect { get; set; }
        public State State { get; set; }
        public int CompanyId { get; set; }
        public string Symbol { get; set; }
        public virtual List<SelectedAttributePredefinedLibDm> SelectedAttributePredefined { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<NodeLibDm> Children { get; set; }
        public virtual ICollection<NodeTerminalLibDm> NodeTerminals { get; set; }
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }
        public virtual ICollection<SimpleLibDm> Simples { get; set; }
    }
}