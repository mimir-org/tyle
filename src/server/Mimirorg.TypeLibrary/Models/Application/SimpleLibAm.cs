using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Extensions;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class SimpleLibAm
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> Attributes { get; set; } // TODO: Bør være objektet AttributeLibAm

        [JsonIgnore]
        public string Id => $"{Name}".CreateMd5();
    }
}
