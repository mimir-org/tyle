using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;
using Newtonsoft.Json;

namespace TypeLibrary.Data.Models
{
    public class RdsLibDm
    {
        public string Id { get; set; }
        public string RdsCategoryId { get; set; }
        public RdsCategoryLibDm RdsCategory { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Code { get; set; }
        public Aspect Aspect { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransportLibDm> Transports { get; set; }

        [JsonIgnore]
        public virtual ICollection<InterfaceLibDm> Interfaces { get; set; }

        [JsonIgnore]
        public virtual ICollection<NodeLibDm> Nodes { get; set; }
    }
}
