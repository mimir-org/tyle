using System.ComponentModel.DataAnnotations;
using Mimirorg.TypeLibrary.Models.Data;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Application
{
    public class TerminalLibAm
    {
        public string ParentId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        
        public ICollection<string> AttributeIdList { get; set; }

        [JsonIgnore]
        public ICollection<AttributeLibDm> ConvertToObject => AttributeIdList?.Select(x => new AttributeLibDm { Id = x }).ToList();

        [JsonIgnore]
        public string Key => $"{Name}";
    }
}
