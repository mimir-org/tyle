using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Discipline = Mimirorg.TypeLibrary.Enums.Discipline;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeLibAm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        [Required]
        public Discipline Discipline { get; set; }

        [Required]
        public Select Select { get; set; }

        [Required]
        public string AttributeQualifier { get; set; }

        [Required]
        public string AttributeSource { get; set; }

        [Required]
        public string AttributeCondition { get; set; }

        [Required]
        public string AttributeFormat { get; set; }

        public ICollection<string> ContentReferences { get; set; }
        public string ParentId { get; set; }
        public ICollection<string> SelectValues { get; set; }
        public ICollection<string> UnitIdList { get; set; }
        public HashSet<string> Tags { get; set; }

        [JsonIgnore]
        public string Id => ($"{Name}-{Aspect}-{AttributeQualifier}-{AttributeSource}-{AttributeCondition}").CreateMd5();
    }
}
