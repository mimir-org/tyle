using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.Enums;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Application.TypeEditor
{
    public class AttributeTypeAm
    {
        [Required]
        public string Entity { get; set; }

        [Required]
        public string QualifierId { get; set; }
        
        [Required]
        public string SourceId { get; set; }

        [Required]
        public string ConditionId { get; set; }

        public ICollection<string> Units { get; set; }
        
        public ICollection<string> SelectValues { get; set; }

        [Required]
        public SelectType SelectType { get; set; }

        [Required]
        public Discipline Discipline { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        [Required]
        public string FormatId { get; set; }

        public HashSet<string> Tags { get; set; }

        [JsonIgnore]
        public string Key => $"{Entity}-{Aspect}-{QualifierId}-{SourceId}-{ConditionId}";

        [JsonIgnore]
        public ICollection<Unit> ConvertToObject => Units.Select(x => new Unit { Id = x }).ToList();
    }
}
