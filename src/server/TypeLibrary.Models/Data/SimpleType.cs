using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Data
{
    public class SimpleType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        
        public virtual ICollection<AttributeType> AttributeTypes { get; set; }

        [JsonIgnore]
        public ICollection<NodeType> NodeTypes { get; set; }
    }
}
