using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Data
{
    public class Collection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
     
        [JsonIgnore]
        public virtual string Key => $"{Name}";
    }
}
