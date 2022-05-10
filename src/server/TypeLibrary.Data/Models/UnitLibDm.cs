using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Data.Models
{
    public class UnitLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string ContentReferences { get; set; }
        public string Description { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Deleted { get; set; }

        [JsonIgnore]
        public virtual ICollection<AttributeLibDm> Attributes { get; set; }
    }
}