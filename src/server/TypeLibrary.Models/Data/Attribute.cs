using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Extensions;

namespace TypeLibrary.Models.Data
{
    [Serializable]
    public class Attribute
    {
        #region Properties

        public string Id { get; set; }
        public string Iri { get; set; }

        public string Domain => Id.ResolveDomain();

        public string Kind => nameof(Attribute);

        public string Entity { get; set; }
        public string Value { get; set; }
        public string SemanticReference { get; set; }
        public string AttributeTypeId { get; set; }
        public bool IsLocked {  get; set; }
        public string IsLockedStatusBy {  get; set; }
        public DateTime? IsLockedStatusDate {  get; set; }
        
        public string SelectedUnitId { get; set; }
        
        public string QualifierId { get; set; }
        public AttributeQualifier Qualifier { get; set; }

        public string SourceId { get; set; }
        public AttributeSource Source { get; set; }
        
        public string ConditionId { get; set; }
        public AttributeCondition Condition { get; set; }

        public string FormatId { get; set; }
        public AttributeFormat Format { get; set; }

        public virtual HashSet<string> Tags { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

        [JsonIgnore]
        public string SelectValuesString { get; set; }

        public SelectType SelectType { get; set; }

        public Discipline Discipline { get; set; }

        [NotMapped]
        public virtual ICollection<Unit> Units { get; set; }

        [JsonIgnore]
        public string UnitString { get; set; }

        public virtual string TerminalId { get; set; }
        public virtual string NodeId { get; set; }
        public virtual string NodeIri { get; set; }
        public virtual string TransportId { get; set; }
        public virtual string InterfaceId { get; set; }
        public virtual string SimpleId { get; set; }

        [JsonIgnore]
        public virtual Terminal Terminal { get; set; }

        [JsonIgnore]
        public virtual Node Node { get; set; }

        [JsonIgnore]
        public virtual Simple Simple { get; set; }

        [JsonIgnore]
        public virtual Transport Transport { get; set; }

        [JsonIgnore]
        public virtual Interface Interface { get; set; }

        #endregion

    }
}
