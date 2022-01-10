using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Application
{
    public class ConnectorAm
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Domain => Id.ResolveDomain();
        [Required]
        public string Name { get; set; }
        public ConnectorType Type { get; set; }
        public string SemanticReference { get; set; }
        public bool Visible { get; set; }
        public virtual string NodeId { get; set; }
        public virtual string NodeIri { get; set; }
        public bool IsRequired { get; set; }

        // Relation
        public RelationType RelationType { get; set; }

        // Terminal
        public string Color { get; set; }
        public string TerminalCategoryId { get; set; }
        public string TerminalTypeId { get; set; }
        public virtual ICollection<AttributeAm> Attributes { get; set; }
    }
}
