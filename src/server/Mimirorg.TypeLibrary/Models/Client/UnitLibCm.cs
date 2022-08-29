using Mimirorg.Common.Converters;
using Newtonsoft.Json;

namespace Mimirorg.TypeLibrary.Models.Client
{
    public class UnitLibCm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }

        [JsonConverter(typeof(EmbeddedJsonConverter))]
        public ICollection<TypeReferenceCm> TypeReferences { get; set; }

        public string Description { get; set; }
        public string Symbol { get; set; }
        public string Kind => nameof(UnitLibCm);
    }
}