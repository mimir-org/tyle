using System;
using System.Collections.Generic;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Contracts;

namespace TypeLibrary.Data.Models
{
    public class SimpleLibDm : ILibraryType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }
        public ICollection<NodeLibDm> Nodes { get; set; }
    }
}