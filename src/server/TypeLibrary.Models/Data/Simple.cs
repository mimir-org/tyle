using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Data
{
    public class Simple
    {
        public string Id { get; set; }
        public string Name { get; set; }
        
        public string Kind => nameof(Simple);
        public string SemanticReference { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
        public virtual string NodeId { get; set; }

        [JsonIgnore]
        public Node Node { get; set; }
    }
}
