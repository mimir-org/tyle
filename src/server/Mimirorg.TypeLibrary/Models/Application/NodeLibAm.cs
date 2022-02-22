using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Attributes;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class NodeLibAm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string RdsId { get; set; }

        [Required]
        public string PurposeId { get; set; }

        public string ParentId { get; set; }
        public string Version { get; set; } = "1.0";
        public string FirstVersionId { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        [Required]
        public State State { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public string Description { get; set; }
        public string BlobId { get; set; }
        public string AttributeAspectId { get; set; }
        public ICollection<NodeTerminalLibAm> NodeTerminals { get; set; }
        public ICollection<string> AttributeIdList { get; set; }
        public ICollection<SelectedAttributePredefinedLibAm> SelectedAttributePredefined { get; set; }
        public ICollection<string> SimpleIdList { get; set; }
        public ICollection<string> CollectionIdList { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }

        [Required]
        [UtcDatetime]
        public DateTime Created { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public string Id => $"{Name}-{RdsId}-{Aspect}-{Version}".CreateMd5();

    }
}
