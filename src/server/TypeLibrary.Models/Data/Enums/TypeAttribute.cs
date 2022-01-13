using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Data.Enums
{
    public class TypeAttribute 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public string ParentId { get; set; }
        public TypeAttribute Parent { get; set; }
        public ICollection<TypeAttribute> Children { get; set; }
        public Aspect Aspect { get; set; }

        private const string InternalType = "Mb.Models.Data.Enums.TypeAttribute";

        [JsonIgnore]
        public virtual string Key => string.IsNullOrEmpty(ParentId) ? $"{Name}-{InternalType}" : $"{Name}-{InternalType}-{ParentId}";
    }
}