using System.Text.Json.Serialization;

namespace Tyle.External.Model
{
    public class ExternalType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("attributes")]
        public List<ExternalTypeAttribute> Attributes { get; set; } = new List<ExternalTypeAttribute>();
    }

    public class ExternalTypeAttribute
    {
        [JsonPropertyName("definitionName")]
        public string DefinitionName { get; set; } = string.Empty;
        [JsonPropertyName("displayValue")]
        public string DisplayValue { get; set; } = string.Empty;
    }
}