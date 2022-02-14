using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models
{
    public class AttributeLibDm
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public AttributeLibDm Parent { get; set; }
        public ICollection<AttributeLibDm> Children { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public Aspect Aspect { get; set; }
        public Discipline Discipline { get; set; }
        public virtual HashSet<string> Tags { get; set; }
        public Select Select { get; set; }
        public string SelectValuesString { get; set; }
        public string AttributeQualifier { get; set; }
        public string AttributeSource { get; set; }
        public string AttributeCondition { get; set; }
        public string AttributeFormat { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

        public virtual ICollection<TerminalLibDm> Terminals { get; set; }
        public virtual ICollection<InterfaceLibDm> Interfaces { get; set; }
        public virtual ICollection<NodeLibDm> Nodes { get; set; }
        public virtual ICollection<SimpleLibDm> Simple { get; set; }
        public virtual ICollection<TransportLibDm> Transports { get; set; }
        public ICollection<UnitLibDm> Units { get; set; }
    }
}
