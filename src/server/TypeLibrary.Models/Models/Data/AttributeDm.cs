using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Mimirorg.Common.Extensions;
using Newtonsoft.Json;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Models.Data
{
    public class AttributeDm
    {
        public string Id { get; set; }
        public string Entity { get; set; }
        public Aspect Aspect { get; set; }

        public string QualifierId { get; set; }
        public QualifierDm QualifierDm { get; set; }

        public string SourceId { get; set; }
        public SourceDm SourceDm { get; set; }
        
        public string ConditionId { get; set; }
        public ConditionDm ConditionDm { get; set; }
        
        public ICollection<UnitDm> Units { get; set; }
        
        public string FormatId { get; set; }
        public FormatDm FormatDm { get; set; }

        public virtual HashSet<string> Tags { get; set; }

        [NotMapped]
        public ICollection<string> SelectValues => string.IsNullOrEmpty(SelectValuesString) ? null : SelectValuesString.ConvertToArray();

        [JsonIgnore]
        public string SelectValuesString { get; set; }

        public SelectType SelectType { get; set; }

        public Discipline Discipline { get; set; }

        public string Description => CreateDescription();

        [JsonIgnore]
        public virtual ICollection<TerminalDm> TerminalTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<NodeDm> NodeTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransportDm> TransportTypes { get; set; }

        [JsonIgnore]
        public virtual ICollection<SimpleDm> SimpleTypes { get; set; }

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
