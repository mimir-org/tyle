using System;
using System.Collections.Generic;

namespace TypeLibrary.Data.Models
{
    public class AttributeLibDm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public string State { get; set; }
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<AttributeUnitLibDm> AttributeUnits { get; set; }
        public ICollection<NodeAttributeLibDm> AttributeNodes { get; set; }
        public ICollection<TerminalAttributeLibDm> AttributeTerminals { get; set; }
    }
}