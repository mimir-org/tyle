using Newtonsoft.Json.Linq;
using Tyle.Core.Attributes;
using Tyle.Core.Blocks;
using Tyle.Core.Terminals;

namespace Tyle.Converters;

public interface IJsonLdConversionService
{
    Task<JObject> ConvertToJsonLd(BlockType block);

    Task<JObject> ConvertToJsonLd(TerminalType terminal);

    Task<JObject> ConvertToJsonLd(AttributeType attribute);
}
