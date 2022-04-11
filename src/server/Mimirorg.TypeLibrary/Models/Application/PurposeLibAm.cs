using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;
using Newtonsoft.Json;
using Discipline = Mimirorg.TypeLibrary.Enums.Discipline;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class PurposeLibAm
    {
        [Required]
        public string Name { get; set; }
        public Discipline Discipline { get; set; }
        public string Description { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        [JsonIgnore]
        private const string InternalType = "Mb.Models.Data.Enums.Purpose";

        [JsonIgnore]
        public virtual string Id => $"{Name}-{InternalType}-{Discipline}".CreateMd5();
    }
}