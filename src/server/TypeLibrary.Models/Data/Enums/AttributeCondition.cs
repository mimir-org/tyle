using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Data.Enums
{
    public class AttributeCondition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }

        private const string InternalType = "Mb.Models.Data.Enums.AttributeCondition";

        [JsonIgnore]
        public virtual string Key => $"{Name}-{InternalType}";

        [JsonIgnore]
        public virtual ICollection<AttributeType> AttributeTypes { get; set; }
    }
}
