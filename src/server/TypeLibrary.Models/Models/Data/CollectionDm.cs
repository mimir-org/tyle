using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Models.Data
{
    public class CollectionDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CompanyId { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
       
        [JsonIgnore]
        public virtual string Key => $"{Name}";

        [JsonIgnore]
        public virtual ICollection<TypeDm> Types { get; set; }
    }
}
