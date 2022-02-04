using System.Collections.Generic;
using Mimirorg.Common.Enums;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Models.Data
{
    public class RdsLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public string RdsCategoryId { get; set; }
        public RdsCategoryLibDm RdsCategoryDm { get; set; }

        public string SemanticReference { get; set; }
        public Aspect Aspect { get; set; }

        [JsonIgnore]
        public ICollection<TypeLibDm> LibraryTypes { get; set; }
    }
}
