using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json;
using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Models.Models.Application
{
    public class TerminalAm
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
        public ICollection<AttributeDm> ConvertToObject => Attributes.Select(x => new AttributeDm { Id = x }).ToList();
    }
}
