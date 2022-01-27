using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Models.Data
{
    public class SimpleDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        
        public virtual ICollection<AttributeDm> AttributeList { get; set; }

        [JsonIgnore]
        public ICollection<NodeDm> NodeTypes { get; set; }
    }
}
