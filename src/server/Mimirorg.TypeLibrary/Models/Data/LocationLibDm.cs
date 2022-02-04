using Mimirorg.Common.Enums;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Data
{
    public class LocationLibDm 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Iri { get; set; }
        public string ParentId { get; set; }
        public LocationLibDm Parent { get; set; }
        public ICollection<LocationLibDm> Children { get; set; }
        public Aspect Aspect { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        [JsonIgnore]
        private const string InternalType = "Mb.Models.Data.Enums.TypeAttribute";

        [JsonIgnore]
        public virtual string Key => string.IsNullOrEmpty(ParentId) ? $"{Name}-{InternalType}" : $"{Name}-{InternalType}-{ParentId}";
    }
}