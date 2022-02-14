using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class RdsLibAm
    {
        [Required]
        public string RdsCategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public Aspect Aspect { get; set; }

        [JsonIgnore]
        public string Id => $"{Code}-{RdsCategoryId}".CreateMd5(); // TODO: burde være en kombinasjon av Code og Aspect
    }
}
