using System;
using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class InterfaceLibDm
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public InterfaceLibDm Parent { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public string Iri { get; set; }
        public string ContentReferences { get; set; }
        public string RdsCode { get; set; }
        public string RdsName { get; set; }
        public string PurposeName { get; set; }
        public Aspect Aspect { get; set; }
        public State State { get; set; }
        public int CompanyId { get; set; }
        public string TerminalId { get; set; }
        public TerminalLibDm Terminal { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }
        public virtual ICollection<InterfaceLibDm> Children { get; set; }
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }
    }
}