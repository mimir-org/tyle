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
        public QualifierLibDm QualifierDm { get; set; }

        public string SourceId { get; set; }
        public SourceLibDm SourceDm { get; set; }
        
        public string ConditionId { get; set; }
        public ConditionLibDm ConditionDm { get; set; }
        
        public ICollection<UnitLibDm> Units { get; set; }
        
        public string FormatId { get; set; }
        public FormatLibDm FormatDm { get; set; }

        public virtual HashSet<string> Tags { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

        [JsonIgnore]
        public string SelectValuesString { get; set; }

        public SelectType SelectType { get; set; }

        public Discipline Discipline { get; set; }

        public string Description => CreateDescription();

        [JsonIgnore]
        public virtual ICollection<TerminalLibDm> TerminalTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<NodeLibDm> NodeTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransportLibDm> TransportTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<SimpleLibDm> SimpleTypes { get; set; }

        private string CreateDescription()
        {
            var text = string.Empty;

            if (SourceDm?.Name != null && SourceDm?.Name != "NotSet")
                text += SourceDm.Name + " ";

            text += Entity;

            var subText = string.Empty;

            if (QualifierDm?.Name != null && QualifierDm.Name != "NotSet")
                subText = QualifierDm.Name;

            if (ConditionDm?.Name != null && ConditionDm.Name != "NotSet")
                subText += ", " + ConditionDm.Name;

            if (!string.IsNullOrEmpty(subText))
                text += " - " + subText;

            return text;
        }
    }
}
