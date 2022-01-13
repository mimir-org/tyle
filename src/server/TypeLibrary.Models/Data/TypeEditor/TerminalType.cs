using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Data.TypeEditor
{
    public class TerminalType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public string ParentId { get; set; }
        public TerminalType Parent { get; set; }
        public string Color { get; set; }

        private const string InternalType = "Mb.Models.Data.Enums.TerminalCategory";

        [JsonIgnore]
        public virtual string Key => $"{Name}-{InternalType}";

        public ICollection<AttributeType> Attributes { get; set; }
        public ICollection<NodeTypeTerminalType> NodeTypes { get; set; }
        public ICollection<InterfaceType> InterfaceTypes { get; set; }
        public ICollection<TransportType> TransportTypes { get; set; }
    }
}