using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class UnitLibAm
    {
        [Required]
        public string Name { get; set; }
        public virtual string Description { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        [JsonIgnore]
        private const string InternalType = "Mb.Models.Data.Enums.Unit";

        [JsonIgnore]
        public virtual string Id => $"{Name}-{InternalType}".CreateMd5();
    }
}