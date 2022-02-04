using Mimirorg.Common.Enums;
using Newtonsoft.Json;

namespace TypeLibrary.Models.Models.Application
{
    public class RdsLibAm
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string RdsCategoryId { get; set; }
        public string SemanticReference { get; set; }
        public Aspect Aspect { get; set; }

        [JsonIgnore]
        public string Key => $"{Code}-{RdsCategoryId}";
    }
}
