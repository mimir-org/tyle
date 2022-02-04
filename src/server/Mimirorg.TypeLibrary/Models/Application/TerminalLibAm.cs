using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Models.Data;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class TerminalLibAm
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Iri { get; set; }
        public string ParentId { get; set; }
        public string Color { get; set; }
        public ICollection<string> Attributes { get; set; }

        [JsonIgnore]
        public string Key => $"{Name}";

        [JsonIgnore]
        public ICollection<AttributeLibDm> ConvertToObject => Attributes.Select(x => new AttributeLibDm { Id = x }).ToList();
    }
}
