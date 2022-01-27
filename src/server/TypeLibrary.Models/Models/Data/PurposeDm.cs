using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Models.Data
{
    public class PurposeDm
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public Discipline Discipline { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        [JsonIgnore]
        private const string InternalType = "Mb.Models.Data.Enums.Purpose";

        [JsonIgnore]
        public virtual string Key => $"{Name}-{InternalType}-{Discipline}";

        [JsonIgnore]
        public virtual ICollection<TypeDm> LibraryTypes { get; set; }
    }
}