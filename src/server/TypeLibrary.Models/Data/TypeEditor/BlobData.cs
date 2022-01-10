using Newtonsoft.Json;
using TypeLibrary.Models.Enums;

namespace TypeLibrary.Models.Data.TypeEditor
{
    public class BlobData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }
        public Discipline Discipline { get; set; }

        [JsonIgnore]
        public virtual string Key => $"{Name}-{Discipline}";
    }
}
