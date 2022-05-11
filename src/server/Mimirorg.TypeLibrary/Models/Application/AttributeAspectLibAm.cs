using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class AttributeAspectLibAm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        public ICollection<string> ContentReferences { get; set; }
        public string ParentId { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        private const string InternalType = "Mb.Models.Data.Enums.AttributeAspect";

        [JsonIgnore]
        public string Id => (string.IsNullOrEmpty(ParentId) ? $"{Name}-{InternalType}" : $"{Name}-{InternalType}-{ParentId}").CreateMd5();

    }
}