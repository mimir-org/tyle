using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeScriptBuilder;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class NodeLibAm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string RdsName { get; set; }

        [Required]
        public string RdsCode { get; set; }

        [Required]
        public string PurposeName { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public ICollection<string> SimpleIdList { get; set; }
        public ICollection<string> AttributeIdList { get; set; }
        public ICollection<NodeTerminalLibAm> NodeTerminals { get; set; }
        public ICollection<SelectedAttributePredefinedLibAm> SelectedAttributePredefined { get; set; }

        public string Description { get; set; }
        public string Symbol { get; set; }
        public string AttributeAspectIri { get; set; }

        [TSExclude]
        public string Version { get; set; } = "1.0";

        public ICollection<string> ContentReferences { get; set; }
        public string ParentId { get; set; }

        [TSExclude]
        public string Id => $"{Name}-{RdsCode}-{Aspect}-{Version}".CreateMd5();
    }
}