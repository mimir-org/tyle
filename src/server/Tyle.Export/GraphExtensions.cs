using Tyle.Core.Attributes;
using VDS.RDF;

namespace Tyle.Export;

public static class GraphExtensions
{
    public static void AddHasValueTriple(this IGraph g, INode root, Uri path, Uri value)
    {
        var valueNode = g.CreateUriNode(value);
        g.AddShaclTriple(root, path, OntologyConstants.HasValue, valueNode);
    }

    public static void AddHasValueTriple(this IGraph g, INode root, Uri path, string value, Uri dataType)
    {
        var valueNode = g.CreateLiteralNode(value, dataType);
        g.AddShaclTriple(root, path, OntologyConstants.HasValue, valueNode);
    }

    public static void AddInTriple(this IGraph g, INode root, Uri path, IEnumerable<Uri> values)
    {
        var list = g.AssertList(new List<INode>(values.Select(g.CreateUriNode)));
        g.AddShaclTriple(root, path, OntologyConstants.In, list);
    }

    public static void AddInTriple(this IGraph g, INode root, Uri path, IEnumerable<string> values, Uri dataType)
    {
        var list = g.AssertList(new List<INode>(values.Select(x => g.CreateLiteralNode(x, dataType))));
        g.AddShaclTriple(root, path, OntologyConstants.In, list);
    }

    public static void AddDataTypeTriple(this IGraph g, INode root, Uri path, Uri dataType)
    {
        var valueNode = g.CreateUriNode(dataType);
        g.AddShaclTriple(root, path, OntologyConstants.DataType, valueNode);
    }

    public static void AddPatternTriple(this IGraph g, INode root, Uri path, string pattern)
    {
        var valueNode = g.CreateLiteralNode(pattern);
        g.AddShaclTriple(root, path, OntologyConstants.Pattern, valueNode);
    }

    public static void AddRangeTriples(this IGraph g, INode root, Uri path, string? minValue, string? maxValue, Uri dataType)
    {
        if (minValue != null)
        {
            var valueNode = g.CreateLiteralNode(minValue, dataType);
            g.AddShaclTriple(root, path, OntologyConstants.MinInclusive, valueNode);
        }
        if (maxValue != null)
        {
            var valueNode = g.CreateLiteralNode(maxValue, dataType);
            g.AddShaclTriple(root, path, OntologyConstants.MaxInclusive, valueNode);
        }
    }

    private static void AddShaclTriple(this IGraph g, INode root, Uri path, Uri constraint, INode value)
    {
        var propertyNode = g.CreateBlankNode();

        var pathTriple = new Triple(propertyNode, g.CreateUriNode(OntologyConstants.Path), g.CreateUriNode(path));
        g.Assert(pathTriple);

        var shaclTriple = new Triple(propertyNode, g.CreateUriNode(constraint), value);
        g.Assert(shaclTriple);

        var rootTriple = new Triple(root, g.CreateUriNode(OntologyConstants.Property), propertyNode);
        g.Assert(rootTriple);
    }
}
