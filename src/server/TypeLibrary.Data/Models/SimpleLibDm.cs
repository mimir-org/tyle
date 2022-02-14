using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Data.Models
{
    public class SimpleLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }

        [JsonIgnore]
        public ICollection<NodeLibDm> Nodes { get; set; }
    }
}
