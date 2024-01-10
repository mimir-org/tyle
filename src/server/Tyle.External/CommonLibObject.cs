using System.Text.Json.Serialization;

namespace Tyle.External;

public class CommonLibObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("attributes")]
    public List<CommonLibObjectAttribute> Attributes { get; set; } = new List<CommonLibObjectAttribute>();
}

public class CommonLibObjectAttribute
{
    [JsonPropertyName("definitionName")]
    public string DefinitionName { get; set; } = string.Empty;
    [JsonPropertyName("displayValue")]
    public string DisplayValue { get; set; } = string.Empty;
}