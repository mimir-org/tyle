using Newtonsoft.Json;

namespace Mimirorg.Common.Converters;

public class EmbeddedJsonConverter : Newtonsoft.Json.JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.Value is string value)
            return serializer.Deserialize(new StringReader(value), objectType);

        return serializer.Deserialize(reader, objectType);
    }

    public override bool CanConvert(Type objectType)
    {
        return true;
    }
}