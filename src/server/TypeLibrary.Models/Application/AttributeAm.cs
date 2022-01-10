using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Application
{
    public class AttributeAm
    {
        public string Id { get; set; }
        public string Iri { get; set; }
        public string Domain => Id.ResolveDomain();
        [Required]
        public string Entity { get; set; }
        public string Value { get; set; }
        public string SemanticReference { get; set; }
        public string SelectedUnitId { get; set; }
        [Required]
        public string AttributeTypeId { get; set; }
        //public string AttributeTypeIri { get; set; }
        public bool IsLocked { get; set; }
        public string IsLockedStatusBy { get; set; }
        public DateTime? IsLockedStatusDate { get; set; }
        [Required]
        public string QualifierId { get; set; }
        [Required]
        public string SourceId { get; set; }
        [Required]
        public string ConditionId { get; set; }
        [Required]
        public string FormatId { get; set; }
        public virtual HashSet<string> Tags { get; set; }
        public string TerminalId { get; set; }
        public string NodeId { get; set; }
        public string NodeIri { get; set; }
        public string TransportId { get; set; }
        public string InterfaceId { get; set; }
        public string SimpleId { get; set; }
        public virtual ICollection<UnitAm> Units { get; set; }
        public ICollection<string> SelectValues { get; set; }
        public SelectType SelectType { get; set; }
        public Discipline Discipline { get; set; }
    }
}
