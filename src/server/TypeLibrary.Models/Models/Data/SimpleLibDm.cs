using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Models.Data
{
    public class SimpleLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        
        public virtual ICollection<AttributeLibDm> AttributeList { get; set; }

        [JsonIgnore]
        public ICollection<NodeLibDm> NodeTypes { get; set; }
    }
}
