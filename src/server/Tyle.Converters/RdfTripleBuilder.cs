using Tyle.Converters.Iris;
using Tyle.Core.Common;
using VDS.RDF;

namespace Tyle.Converters;

public static class RdfTripleBuilder
{
    public static void AddMetadataTriples(this IGraph g, INode typeNode, ImfType type)
    {
        g.Assert(new Triple(typeNode, g.CreateUriNode(Rdfs.Label), g.CreateLiteralNode(type.Name)));
        g.Assert(new Triple(typeNode, g.CreateUriNode(DcTerms.Description), g.CreateLiteralNode(type.Description)));
        g.Assert(new Triple(typeNode, g.CreateUriNode(Pav.Version), g.CreateLiteralNode(type.Version)));
        g.Assert(new Triple(typeNode, g.CreateUriNode(DcTerms.Created), g.CreateLiteralNode(type.CreatedOn.ToString(), Xsd.DateTime)));

        // TODO: Created by, contributed by, last update on
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