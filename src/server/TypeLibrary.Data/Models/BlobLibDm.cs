using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;
using Newtonsoft.Json;

namespace TypeLibrary.Data.Models
{
    public class BlobLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public Discipline Discipline { get; set; }
        public string Data { get; set; }

        [JsonIgnore]
        public virtual string Key => $"{Name}-{Discipline}";

        [JsonIgnore]
        public virtual ICollection<NodeLibDm> Nodes { get; set; }
    }
}
