using System;
using System.Collections.Generic;
using Mimirorg.TypeLibrary.Contracts;
using Newtonsoft.Json;

namespace TypeLibrary.Data.Models
{
    public class CollectionLibDm : ILibraryType
    {
        public string Id { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
       
        [JsonIgnore]
        public virtual string Key => $"{Name}";

        [JsonIgnore]
        public virtual ICollection<NodeLibDm> Types { get; set; }
    }
}
