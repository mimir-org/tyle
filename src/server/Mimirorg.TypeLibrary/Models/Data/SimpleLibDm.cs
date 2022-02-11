using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Data
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
