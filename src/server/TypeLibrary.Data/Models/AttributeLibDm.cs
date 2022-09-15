using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Contracts;
using Mimirorg.TypeLibrary.Enums;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Data.Models
{
    public class AttributeLibDm : ILibraryType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string TypeReferences { get; set; }

        public string AttributeQualifier { get; set; }
        public string AttributeSource { get; set; }
        public string AttributeCondition { get; set; }
        public string AttributeFormat { get; set; }

        public int CompanyId { get; set; }
        public Aspect Aspect { get; set; }
        public Discipline Discipline { get; set; }
        public Select Select { get; set; }
        public string SelectValuesString { get; set; }
        public string Units { get; set; }
        public State State { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public virtual HashSet<string> Tags { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

        public virtual ICollection<TerminalLibDm> Terminals { get; set; }
        public virtual ICollection<InterfaceLibDm> Interfaces { get; set; }
        public virtual ICollection<NodeLibDm> Nodes { get; set; }
        public virtual ICollection<SimpleLibDm> Simple { get; set; }
        public virtual ICollection<TransportLibDm> Transports { get; set; }
    }
}