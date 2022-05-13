using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;

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

        [TSExclude]
        private const string InternalType = "Mb.Models.Data.Enums.AttributeAspect";

        [TSExclude]
        public string Id => (string.IsNullOrEmpty(ParentId) ? $"{Name}-{InternalType}" : $"{Name}-{InternalType}-{ParentId}").CreateMd5();

    }
}