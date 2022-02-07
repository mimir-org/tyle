﻿using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Data
{
    public class QualifierLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        [JsonIgnore]
        private const string InternalType = "Mb.Models.Data.Enums.AttributeQualifier";

        [JsonIgnore]
        public virtual string Key => $"{Name}-{InternalType}";

        [JsonIgnore]
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }
    }
}