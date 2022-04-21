using System;
using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class InterfaceLibDm
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Name { get; set; }
        public string RdsId { get; set; }
        public string RdsName { get; set; }
        public string PurposeId { get; set; }
        public string PurposeName { get; set; }
        public string ParentId { get; set; }
        public InterfaceLibDm Parent { get; set; }
        public string Version { get; set; }
        public string FirstVersionId { get; set; }
        public Aspect Aspect { get; set; }
        public string Description { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public string TerminalId { get; set; }
        public TerminalLibDm Terminal { get; set; }
        public virtual ICollection<InterfaceLibDm> Children { get; set; }
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }
    }
}
