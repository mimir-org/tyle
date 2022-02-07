using System.ComponentModel.DataAnnotations.Schema;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Extensions;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Data
{
    public class AttributeLibDm
    {
        public string Id { get; set; }
        public string Entity { get; set; }
        public Aspect Aspect { get; set; }

        public string QualifierId { get; set; }
        public QualifierLibDm Qualifier { get; set; }

        public string SourceId { get; set; }
        public SourceLibDm Source { get; set; }
        
        public string ConditionId { get; set; }
        public ConditionLibDm Condition { get; set; }
        
        public ICollection<UnitLibDm> Units { get; set; }
        
        public string FormatId { get; set; }
        public FormatLibDm Format { get; set; }

        public virtual HashSet<string> Tags { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

        [JsonIgnore]
        public string SelectValuesString { get; set; }

        public SelectType SelectType { get; set; }

        public Discipline Discipline { get; set; }

        public string Description => CreateDescription();

        [JsonIgnore]
        public virtual ICollection<TerminalLibDm> Terminals { get; set; }

        [JsonIgnore]
        public virtual ICollection<NodeLibDm> Nodes { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransportLibDm> Transports { get; set; }

        [JsonIgnore]
        public virtual ICollection<SimpleLibDm> SimpleTypes { get; set; }

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
