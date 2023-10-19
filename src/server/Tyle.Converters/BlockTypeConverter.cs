using Newtonsoft.Json.Linq;
using Tyle.Core.Blocks;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Writing;

namespace Tyle.Converters;

public static class BlockTypeConverter
{
    public static JObject ToJsonLd(this BlockType block)
    {
        var g = new Graph();

        var blockNode = g.CreateUriNode(new Uri($"http://tyle.imftools.com/blocks/{block.Id}"));

        // Add metadata

        g.AddMetadataTriples(blockNode, block);

        var store = new TripleStore();
        store.Add(g);

        var jsonLdWriter = new JsonLdWriter();
        var serializedGraph = jsonLdWriter.SerializeStore(store);
        var jsonString = $"{{ \"@graph\": {serializedGraph} }}";


        var flattened = JsonLdProcessor.Flatten(JToken.Parse(jsonString), JToken.Parse(JsonLdConstants.Context), new JsonLdProcessorOptions());
        var result = JsonLdProcessor.Frame(flattened, JToken.Parse(JsonLdConstants.Frame), new JsonLdProcessorOptions());

        return result;
    }
}
