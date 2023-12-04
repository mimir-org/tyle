using Newtonsoft.Json.Linq;
using Tyle.Core.Attributes;
using Tyle.Core.Blocks;
using Tyle.Core.Terminals;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Writing;

namespace Tyle.Converters;

public class JsonLdConversionService : IJsonLdConversionService
{
    private readonly IAttributeTypeConverter _attributeTypeConverter;
    private readonly ITerminalTypeConverter _terminalTypeConverter;
    private readonly IBlockTypeConverter _blockTypeConverter;

    public JsonLdConversionService(IAttributeTypeConverter attributeTypeConverter, ITerminalTypeConverter terminalTypeConverter, IBlockTypeConverter blockTypeConverter)
    {
        _attributeTypeConverter = attributeTypeConverter;
        _terminalTypeConverter = terminalTypeConverter;
        _blockTypeConverter = blockTypeConverter;
    }

    private static JObject GetJsonLd(IGraph g, string frame)
    {
        var store = new TripleStore();

        store.Add(g);

        var jsonLdWriter = new JsonLdWriter();
        var serializedGraph = jsonLdWriter.SerializeStore(store);
        var jsonString = $"{{ \"@graph\": {serializedGraph} }}";

        var result = JsonLdProcessor.Frame(JToken.Parse(jsonString), JToken.Parse(frame), new JsonLdProcessorOptions());

        return result;
    }

    public async Task<JObject> ConvertToJsonLd(BlockType block)
    {
        var uniqueAttributes = new List<AttributeType>();
        var uniqueTerminals = new List<TerminalType>();

        foreach (var attribute in block.Attributes)
        {
            if (uniqueAttributes.Select(x => x.Id).Contains(attribute.AttributeId)) continue;
            uniqueAttributes.Add(attribute.Attribute);
        }

        foreach (var terminal in block.Terminals)
        {
            if (uniqueTerminals.Select(x => x.Id).Contains(terminal.TerminalId)) continue;
            uniqueTerminals.Add(terminal.Terminal);

            foreach (var attribute in terminal.Terminal.Attributes)
            {
                if (uniqueAttributes.Select(x => x.Id).Contains(attribute.AttributeId)) continue;
                uniqueAttributes.Add(attribute.Attribute);
            }
        }

        var g = new Graph();

        foreach (var attribute in uniqueAttributes)
        {
            g.Merge(await _attributeTypeConverter.ConvertTypeToGraph(attribute));
        }

        foreach (var terminal in uniqueTerminals)
        {
            g.Merge(await _terminalTypeConverter.ConvertTypeToGraph(terminal));
        }

        g.Merge(await _blockTypeConverter.ConvertTypeToGraph(block));

        return GetJsonLd(g, JsonLdConstants.BlockFrame);
    }

    public async Task<JObject> ConvertToJsonLd(TerminalType terminal)
    {
        var uniqueAttributes = new List<AttributeType>();

        foreach (var attribute in terminal.Attributes)
        {
            if (uniqueAttributes.Select(x => x.Id).Contains(attribute.AttributeId)) continue;
            uniqueAttributes.Add(attribute.Attribute);
        }

        var g = new Graph();

        foreach (var attribute in uniqueAttributes)
        {
            g.Merge(await _attributeTypeConverter.ConvertTypeToGraph(attribute));
        }

        g.Merge(await _terminalTypeConverter.ConvertTypeToGraph(terminal));

        return GetJsonLd(g, JsonLdConstants.TerminalFrame);
    }

    public async Task<JObject> ConvertToJsonLd(AttributeType attribute)
    {
        var g = new Graph();

        g.Merge(await _attributeTypeConverter.ConvertTypeToGraph(attribute));

        return GetJsonLd(g, JsonLdConstants.AttributeFrame);
    }
}