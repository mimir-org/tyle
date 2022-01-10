using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Mimirorg.Common.Extensions;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Data.TypeEditor
{
    public class AttributeType
    {
        public string Id { get; set; }
        public string Entity { get; set; }
        public Aspect Aspect { get; set; }

        public string QualifierId { get; set; }
        public AttributeQualifier Qualifier { get; set; }

        public string SourceId { get; set; }
        public AttributeSource Source { get; set; }
        
        public string ConditionId { get; set; }
        public AttributeCondition Condition { get; set; }
        
        public ICollection<Unit> Units { get; set; }
        
        public string FormatId { get; set; }
        public AttributeFormat Format { get; set; }

        public virtual HashSet<string> Tags { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

        [JsonIgnore]
        public string SelectValuesString { get; set; }

        public SelectType SelectType { get; set; }

        public Discipline Discipline { get; set; }

        public string Description => CreateDescription();

        [JsonIgnore]
        public virtual ICollection<TerminalType> TerminalTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<NodeType> NodeTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransportType> TransportTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<SimpleType> SimpleTypes { get; set; }

        private string CreateDescription()
        {
            var text = string.Empty;

            if (Source?.Name != null && Source?.Name != "NotSet")
                text += Source.Name + " ";

            text += Entity;

            var subText = string.Empty;

            if (Qualifier?.Name != null && Qualifier.Name != "NotSet")
                subText = Qualifier.Name;

            if (Condition?.Name != null && Condition.Name != "NotSet")
                subText += ", " + Condition.Name;

            if (!string.IsNullOrEmpty(subText))
                text += " - " + subText;

            return text;
        }
    }
}
