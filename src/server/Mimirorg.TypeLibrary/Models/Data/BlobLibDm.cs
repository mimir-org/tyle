using Mimirorg.Common.Enums;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Data
{
    public class BlobLibDm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public Discipline Discipline { get; set; }

        [JsonIgnore]
        public virtual string Key => $"{Name}-{Discipline}";
    }
}
