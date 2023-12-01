using System.Text.Json.Serialization;

namespace Tyle.External.Model
{
    public class ExternalType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("identity")]
        public string Identity { get; set; } = string.Empty;
    }
}
