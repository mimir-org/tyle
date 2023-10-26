using Newtonsoft.Json.Linq;
using Tyle.Core.Attributes;
using Tyle.Core.Blocks;
using Tyle.Core.Terminals;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Writing;

namespace Tyle.Converters;

public static class TypeToJsonLdWriter
{
    private static JObject GetJsonLd(IGraph g)
    {
        var store = new TripleStore();

        store.Add(g);

        var jsonLdWriter = new JsonLdWriter();
        var serializedGraph = jsonLdWriter.SerializeStore(store);
        var jsonString = $"{{ \"@graph\": {serializedGraph} }}";

        var result = JsonLdProcessor.Frame(JToken.Parse(jsonString), JToken.Parse(JsonLdConstants.Frame), new JsonLdProcessorOptions());

        return result;
    }

    public static JObject ToJsonLd(this BlockType block)
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
            g.AddAttributeType(attribute);
        }

        foreach (var terminal in uniqueTerminals)
        {
            g.AddTerminalType(terminal);
        }

        g.AddBlockType(block);

        return GetJsonLd(g);
    }

    public static JObject ToJsonLd(this TerminalType terminal)
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
            g.AddAttributeType(attribute);
        }

        g.AddTerminalType(terminal);

        return GetJsonLd(g);
    }

    public static JObject ToJsonLd(this AttributeType attribute)
    {
        var g = new Graph();

        g.AddAttributeType(attribute);

        return GetJsonLd(g);
    }
}