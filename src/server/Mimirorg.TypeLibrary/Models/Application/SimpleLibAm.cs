using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class SimpleLibAm
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }

        public ICollection<string> AttributeIdList { get; set; }

        [JsonIgnore]
        public string Key => $"{Name}";
    }
}
