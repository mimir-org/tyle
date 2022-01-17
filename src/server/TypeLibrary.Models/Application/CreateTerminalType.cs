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

        [Required]
        public string TerminalCategoryId { get; set; }

        public string SemanticReference { get; set; }
        public string Color { get; set; }
        public ICollection<string> Attributes { get; set; }

        [JsonIgnore]
        public string Key => $"{Name}-{TerminalCategoryId}";

        [JsonIgnore]
        public ICollection<AttributeType> ConvertToObject => Attributes.Select(x => new AttributeType { Id = x }).ToList();
    }
}
