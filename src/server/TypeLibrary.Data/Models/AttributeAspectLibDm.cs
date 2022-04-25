using System;
using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;
using Newtonsoft.Json;

namespace TypeLibrary.Data.Models
{
    public class AttributeAspectLibDm
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public AttributeAspectLibDm Parent { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public Aspect Aspect { get; set; }
        public string Description { get; set; }

        public ICollection<AttributeAspectLibDm> Children { get; set; }

        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public virtual ICollection<NodeLibDm> Nodes { get; set; }
    }
}