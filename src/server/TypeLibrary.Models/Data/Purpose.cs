using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Data
{
    public class Purpose
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public Discipline Discipline { get; set; }

        private const string InternalType = "Mb.Models.Data.Enums.Purpose";

        [JsonIgnore]
        public virtual string Key => $"{Name}-{InternalType}-{Discipline}";

        [JsonIgnore]
        public virtual ICollection<LibraryType> LibraryTypes { get; set; }
    }
}