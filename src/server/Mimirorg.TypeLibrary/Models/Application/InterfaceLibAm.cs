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
        public string RdsName { get; set; }

        [Required]
        public string RdsCode { get; set; }

        [Required]
        public string PurposeName { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public ICollection<string> AttributeIdList { get; set; }

        public string Description { get; set; }
        public string Version { get; set; } = "1.0";
        public string ParentId { get; set; }

        [JsonIgnore]
        public string Id => $"{Name}-{RdsCode}-{Aspect}-{Version}".CreateMd5();
    }
}