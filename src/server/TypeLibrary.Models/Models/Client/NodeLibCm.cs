using System;
using System.Collections.Generic;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Models.Data;
using Attribute = System.Attribute;

namespace TypeLibrary.Models.Models.Client
{
    public class NodeLibCm
    {
        public string Id { get; set; }
        public string Version { get; set; } = "1.0";
        public string Rds { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusId { get; set; } = "4590637F39B6BA6F39C74293BE9138DF";
        public Aspect Aspect { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        public string SemanticReference { get; set; }
        public string SymbolId { get; set; }
        public ObjectType LibraryType => ObjectType.ObjectBlock;
        public PurposeLibDm PurposeDm { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
