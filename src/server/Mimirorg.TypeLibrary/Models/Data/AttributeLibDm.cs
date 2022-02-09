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

        public string AttributeQualifierId { get; set; }
        public AttributeQualifierLibDm AttributeQualifier { get; set; }

        public string AttributeSourceId { get; set; }
        public AttributeSourceLibDm AttributeSource { get; set; }
        
        public string AttributeConditionId { get; set; }
        public AttributeConditionLibDm AttributeCondition { get; set; }
        
        public string AttributeFormatId { get; set; }
        public AttributeFormatLibDm AttributeFormat { get; set; }

        public virtual HashSet<string> Tags { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

        [JsonIgnore]
        public string SelectValuesString { get; set; }

        public Select Select { get; set; }

        public Discipline Discipline { get; set; }

        public string Description => CreateDescription();

        [JsonIgnore]
        public virtual ICollection<TerminalLibDm> Terminals { get; set; }

        [JsonIgnore]
        public virtual ICollection<InterfaceLibDm> Interfaces { get; set; }

        [JsonIgnore]
        public virtual ICollection<NodeLibDm> Nodes { get; set; }

        [JsonIgnore]
        public virtual ICollection<SimpleLibDm> Simple { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransportLibDm> Transports { get; set; }

        [JsonIgnore]
        public ICollection<UnitLibDm> Units { get; set; }

        private string CreateDescription()
        {
            var text = string.Empty;

            if (AttributeSource?.Name != null && AttributeSource?.Name != "NotSet")
                text += AttributeSource.Name + " ";

            text += Entity;

            var subText = string.Empty;

            if (AttributeQualifier?.Name != null && AttributeQualifier.Name != "NotSet")
                subText = AttributeQualifier.Name;

            if (AttributeCondition?.Name != null && AttributeCondition.Name != "NotSet")
                subText += ", " + AttributeCondition.Name;

            if (!string.IsNullOrEmpty(subText))
                text += " - " + subText;

            return text;
        }
    }
}
