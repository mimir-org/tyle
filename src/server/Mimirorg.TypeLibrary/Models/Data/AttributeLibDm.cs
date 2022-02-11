using System.ComponentModel.DataAnnotations.Schema;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Extensions;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Data
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

        [JsonIgnore]
        public string SelectValuesString { get; set; }

        public string AttributeQualifier { get; set; }
        public string AttributeSource { get; set; }
        public string AttributeCondition { get; set; }
        public string AttributeFormat { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

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

        public string Description => CreateDescription();

        private string CreateDescription()
        {
            var text = string.Empty;

            if (!string.IsNullOrWhiteSpace(AttributeSource) && AttributeSource != "NotSet")
                text += AttributeSource + " ";

            text += Name;

            var subText = string.Empty;

            if (!string.IsNullOrWhiteSpace(AttributeQualifier) && AttributeQualifier != "NotSet")
                subText = AttributeQualifier;

            if (!string.IsNullOrWhiteSpace(AttributeCondition) && AttributeCondition != "NotSet")
                subText += ", " + AttributeCondition;

            if (!string.IsNullOrEmpty(subText))
                text += " - " + subText;

            return text;
        }
    }
}
