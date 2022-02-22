using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class InterfaceLibAm
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
        public Aspect Aspect { get; set; }
        public string Description { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public ICollection<string> AttributeIdList { get; set; }

        [JsonIgnore]
        public string Id => $"{Name}-{RdsId}-{Aspect}-{Version}".CreateMd5();
    }
}
