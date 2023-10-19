using Newtonsoft.Json.Linq;
using System;
using Tyle.Converters.Iris;
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

        // Add classifier references

        foreach (var classifier in block.Classifiers)
        {
            g.AddShaclPropertyTriple(
                blockNode,
                Imf.Classifier,
                Sh.HasValue,
                g.CreateUriNode(classifier.Classifier.Iri));
        }

        // Add purpose reference

        if (block.Purpose != null)
        {
            g.AddShaclPropertyTriple(
                blockNode,
                Imf.Purpose,
                Sh.HasValue,
                g.CreateUriNode(block.Purpose.Iri));
        }

        // Add notation and symbol

        if (block.Notation != null)
        {
            g.AddShaclPropertyTriple(
                blockNode,
                Skos.Notation,
                Sh.HasValue,
                g.CreateLiteralNode(block.Notation));
        }

        if (block.Symbol != null)
        {
            g.AddShaclPropertyTriple(
                blockNode,
                Imf.Symbol,
                Sh.HasValue,
                g.CreateLiteralNode(block.Symbol));
        }

        // Add aspect

        if (block.Aspect != null)
        {
            g.AddShaclPropertyTriple(
                blockNode,
                Imf.HasAspect,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetAspect(block.Aspect)));
        }

        // Add terminals
        foreach (var terminal in block.Terminals)
        {
            g.AddShaclPropertyTriple(
                blockNode,
                EnumToIriMappers.GetHasTerminalPredicate(terminal.Direction),
                Sh.Node,
                g.CreateUriNode(new Uri($"http://tyle.imftools.com/terminals/{terminal.TerminalId}")),
                out var propertyNode);

            g.Assert(new Triple(
                propertyNode,
                g.CreateUriNode(Sh.MinCount),
                g.CreateLiteralNode(terminal.MinCount.ToString(), Xsd.Integer)));

            if (terminal.MaxCount != null)
            {
                g.Assert(new Triple(
                    propertyNode,
                    g.CreateUriNode(Sh.MaxCount),
                    g.CreateLiteralNode(terminal.MaxCount.ToString(), Xsd.Integer)));
            }
        }

        // Add attributes
        foreach (var attribute in block.Attributes)
        {
            g.AddShaclPropertyTriple(
                blockNode,
                Imf.HasAttribute,
                Sh.Node,
                g.CreateUriNode(new Uri($"http://tyle.imftools.com/attributes/{attribute.AttributeId}")),
                out var propertyNode);

            g.Assert(new Triple(
                propertyNode,
                g.CreateUriNode(Sh.MinCount),
                g.CreateLiteralNode(attribute.MinCount.ToString(), Xsd.Integer)));

            if (attribute.MaxCount != null)
            {
                g.Assert(new Triple(
                    propertyNode,
                    g.CreateUriNode(Sh.MaxCount),
                    g.CreateLiteralNode(attribute.MaxCount.ToString(), Xsd.Integer)));
            }
        }

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
