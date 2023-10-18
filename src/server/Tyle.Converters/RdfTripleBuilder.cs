using Tyle.Converters.Iris;
using VDS.RDF;

namespace Tyle.Converters;

public static class RdfTripleBuilder
{
    public static void AddLiteralTriple(this IGraph g, INode rdfSubject, Uri rdfPredicate, string? rdfObject)
    {
        if (rdfObject == null) return;

        g.AddLiteralTriple(rdfSubject, rdfPredicate, g.CreateLiteralNode(rdfObject));
    }

    public static void AddLiteralTriple(this IGraph g, INode rdfSubject, Uri rdfPredicate, DateTimeOffset rdfObject)
    {
        g.AddLiteralTriple(rdfSubject, rdfPredicate, g.CreateLiteralNode(rdfObject.ToString(), Xsd.DateTime));
    }

    private static void AddLiteralTriple(this IGraph g, INode rdfSubject, Uri rdfPredicate, INode rdfObject)
    {
        g.Assert(new Triple(rdfSubject, g.CreateUriNode(rdfPredicate), rdfObject));
    }

    public static void AddShaclPropertyTriple(this IGraph g, INode root, Uri path, Uri constraint, INode value)
    {

        g.AddShaclPropertyTriple(root, path, constraint, value, out _);
    }

    public static void AddShaclPropertyTriple(this IGraph g, INode root, Uri path, Uri constraint, INode value, out INode propertyNode)
    {
        propertyNode = g.CreateBlankNode();

        var pathTriple = new Triple(propertyNode, g.CreateUriNode(Sh.Path), g.CreateUriNode(path));
        g.Assert(pathTriple);

        var shaclTriple = new Triple(propertyNode, g.CreateUriNode(constraint), value);
        g.Assert(shaclTriple);

        var rootTriple = new Triple(root, g.CreateUriNode(Sh.Property), propertyNode);
        g.Assert(rootTriple);
    }
}