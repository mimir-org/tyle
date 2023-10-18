using Newtonsoft.Json.Linq;
using Tyle.Converters.Iris;
using Tyle.Core.Terminals;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Writing;

namespace Tyle.Converters;

public static class TerminalTypeConverter
{
    public static JObject ToJsonLd(this TerminalType terminal)
    {
        var g = new Graph();

        var terminalNode = g.CreateUriNode(new Uri($"http://tyle.imftools.com/terminals/{terminal.Id}"));

        // Add metadata

        g.AddMetadataTriples(terminalNode, terminal);

        // Add classifier references

        foreach (var classifier in terminal.Classifiers)
        {
            g.AddShaclPropertyTriple(
                terminalNode,
                Imf.Classifier,
                Sh.HasValue,
                g.CreateUriNode(classifier.Classifier.Iri));
        }

        // Add purpose reference

        if (terminal.Purpose != null)
        {
            g.AddShaclPropertyTriple(
                terminalNode,
                Imf.Purpose,
                Sh.HasValue,
                g.CreateUriNode(terminal.Purpose.Iri));
        }

        // Add notation and symbol

        if (terminal.Notation != null)
        {
            g.AddShaclPropertyTriple(
                terminalNode,
                Skos.Notation,
                Sh.HasValue,
                g.CreateLiteralNode(terminal.Notation));
        }

        if (terminal.Symbol != null)
        {
            g.AddShaclPropertyTriple(
                terminalNode,
                Imf.Symbol,
                Sh.HasValue,
                g.CreateLiteralNode(terminal.Symbol));
        }

        // Add aspect

        if (terminal.Aspect != null)
        {
            g.AddShaclPropertyTriple(
                terminalNode,
                Imf.HasAspect,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetAspect(terminal.Aspect)));
        }

        // Add medium reference

        if (terminal.Medium != null)
        {
            g.AddShaclPropertyTriple(
                terminalNode,
                Imf.Medium,
                Sh.HasValue,
                g.CreateUriNode(terminal.Medium.Iri));
        }

        // Add qualifier

        if (terminal.Qualifier != Direction.Bidirectional)
        {
            g.AddShaclPropertyTriple(
                terminalNode,
                Imf.HasTerminalQualifier,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetTerminalQualifier(terminal.Qualifier)));
        }

        // Add attributes
        foreach (var attribute in terminal.Attributes)
        {
            g.AddShaclPropertyTriple(
                terminalNode,
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
