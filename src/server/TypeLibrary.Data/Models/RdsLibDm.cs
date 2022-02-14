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
        public ICollection<LibraryTypeLibDm> LibraryTypes { get; set; }
    }
}
