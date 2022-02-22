using System;
using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;
using Newtonsoft.Json;

namespace TypeLibrary.Data.Models
{
    public class PurposeLibDm
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public Discipline Discipline { get; set; }
        public string Description { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public virtual ICollection<TransportLibDm> Transports { get; set; }

        [JsonIgnore]
        public virtual ICollection<InterfaceLibDm> Interfaces { get; set; }

        [JsonIgnore]
        public virtual ICollection<NodeLibDm> Nodes { get; set; }
    }
}