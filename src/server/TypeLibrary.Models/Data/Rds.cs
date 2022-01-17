using System.Collections.Generic;
using Newtonsoft.Json;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Data
{
    public class Rds
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public string RdsCategoryId { get; set; }
        public RdsCategory RdsCategory { get; set; }

        public string SemanticReference { get; set; }
        public Aspect Aspect { get; set; }

        [JsonIgnore]
        public ICollection<LibraryType> LibraryTypes { get; set; }
    }
}
