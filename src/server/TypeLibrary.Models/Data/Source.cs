using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Data
{
    public class Source
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }

        private const string InternalType = "Mb.Models.Data.Enums.AttributeSource";

        [JsonIgnore]
        public virtual string Key => $"{Name}-{InternalType}";

        [JsonIgnore]
        public virtual ICollection<AttributeType> AttributeTypes { get; set; }
    }
}