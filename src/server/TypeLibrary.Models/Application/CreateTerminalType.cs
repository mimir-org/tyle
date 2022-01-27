using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using TypeLibrary.Models.Data;

namespace TypeLibrary.Models.Application
{
    public class CreateTerminalType
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
        public ICollection<Attribute> ConvertToObject => Attributes.Select(x => new Attribute { Id = x }).ToList();
    }
}
