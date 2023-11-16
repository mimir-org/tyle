using Newtonsoft.Json.Linq;
using Tyle.Converters.Iris;
using Tyle.Core.Attributes;
using Tyle.Core.Blocks;
using Tyle.Core.Terminals;
using VDS.RDF;
using VDS.RDF.JsonLd;

namespace Tyle.Converters;

public static class BlockTypeConverter
{
    public static void AddBlockType(this IGraph g, BlockType block)
    {
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

        // Add notation

        if (block.Notation != null)
        {
            g.AddShaclPropertyTriple(
                blockNode,
                Skos.Notation,
                Sh.HasValue,
                g.CreateLiteralNode(block.Notation));
        }

        // Add symbol

        if (block.Symbol != null)
        {
            g.AddShaclPropertyTriple(
                blockNode,
                Imf.Symbol,
                Sh.HasValue,
                g.CreateUriNode(block.Symbol.Iri));
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

            if (terminal.ConnectionPoint != null)
            {
                var isPointOnNode = g.CreateBlankNode();
                g.Assert(new Triple(isPointOnNode, g.CreateUriNode(Sh.Path), g.CreateUriNode(Sym.IsPointOn)));
                g.Assert(new Triple(isPointOnNode, g.CreateUriNode(Sh.HasValue), g.CreateUriNode(block.Symbol!.Iri)));

                var identifierNode = g.CreateBlankNode();
                g.Assert(new Triple(identifierNode, g.CreateUriNode(Sh.Path), g.CreateUriNode(DcTerms.Identifier)));
                g.Assert(new Triple(identifierNode, g.CreateUriNode(Sh.HasValue), g.CreateLiteralNode(terminal.ConnectionPoint.Identifier)));

                var innerNode = g.CreateBlankNode();
                g.Assert(new Triple(innerNode, g.CreateUriNode(Sh.Property), isPointOnNode));
                g.Assert(new Triple(innerNode, g.CreateUriNode(Sh.Property), identifierNode));

                var outerNode = g.CreateBlankNode();
                g.AddShaclPropertyTriple(outerNode, Imf.Symbol, Sh.Node, innerNode);

                g.Assert(propertyNode, g.CreateUriNode(Sh.Node), outerNode);
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
    }
}