using System;
using System.Collections.Generic;
using TypeLibrary.Models.Data;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Enums;
using Attribute = TypeLibrary.Models.Data.Attribute;

namespace TypeLibrary.Models.Application
{
    public class LibraryNodeItem
    {
        public string Id { get; set; }
        public string Version { get; set; } = "1.0";
        public string Rds { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusId { get; set; } = "4590637F39B6BA6F39C74293BE9138DF";
        public Aspect Aspect { get; set; }
        public ICollection<Connector> Connectors { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        public string SemanticReference { get; set; }
        public string SymbolId { get; set; }
        public ObjectType LibraryType => ObjectType.ObjectBlock;
        public ICollection<Simple> Simples { get; set; }
        public Purpose Purpose { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
