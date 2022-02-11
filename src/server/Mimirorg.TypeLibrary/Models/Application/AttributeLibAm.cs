using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Data;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeLibAm
    {
        public string ParentId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Iri { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        [Required]
        public Discipline Discipline { get; set; }

        public HashSet<string> Tags { get; set; }

        [Required]
        public Select Select { get; set; }

        public ICollection<string> SelectValues { get; set; }
        public ICollection<string> Units { get; set; }

        [Required]
        public string AttributeQualifier { get; set; }
        
        [Required]
        public string AttributeSource { get; set; }

        [Required]
        public string AttributeCondition { get; set; }

        [Required]
        public string AttributeFormat { get; set; }

        [JsonIgnore]
        public ICollection<UnitLibDm> ConvertToObject => Units?.Select(x => new UnitLibDm { Id = x }).ToList();

        [JsonIgnore]
        public string Key => $"{Name}-{Aspect}-{AttributeQualifier}-{AttributeSource}-{AttributeCondition}";
    }
}
