using Tyle.Converters.Iris;
using Tyle.Core.Attributes;
using Tyle.Core.Blocks;
using Tyle.Core.Common;
using Tyle.Core.Terminals;
using VDS.RDF;

namespace Tyle.Converters;

public static class RdfTripleBuilder
{
    public static void AddMetadataTriples(this IGraph g, INode typeNode, ImfType type, UserData creator, IEnumerable<UserData> contributors)
    {
        var (rdfType, rdfsSubClassOf) = GetImfClass(type);

        g.Assert(new Triple(typeNode, g.CreateUriNode(Rdf.Type), g.CreateUriNode(Sh.NodeShape)));
        g.Assert(new Triple(typeNode, g.CreateUriNode(Rdf.Type), g.CreateUriNode(Rdfs.Class)));
        g.Assert(new Triple(typeNode, g.CreateUriNode(Rdf.Type), g.CreateUriNode(rdfType)));
        g.Assert(new Triple(typeNode, g.CreateUriNode(Rdfs.SubClassOf), g.CreateUriNode(rdfsSubClassOf)));

        g.Assert(new Triple(typeNode, g.CreateUriNode(Rdfs.Label), g.CreateLiteralNode(type.Name)));

        if (type.Description != null)
        {
            g.Assert(new Triple(typeNode, g.CreateUriNode(DcTerms.Description), g.CreateLiteralNode(type.Description)));
        }

        g.Assert(new Triple(typeNode, g.CreateUriNode(Pav.Version), g.CreateLiteralNode(type.Version)));
        g.Assert(new Triple(typeNode, g.CreateUriNode(DcTerms.Created), g.CreateLiteralNode(type.CreatedOn.ToString("o"), Xsd.DateTime)));
        g.Assert(new Triple(typeNode, g.CreateUriNode(DcTerms.Modified), g.CreateLiteralNode(type.LastUpdateOn.ToString("o"), Xsd.DateTime)));

        if (creator.Name != null || creator.Email != null)
        {
            var creatorNode = g.CreateBlankNode();
            if (creator.Name != null)
            {
                g.Assert(new Triple(creatorNode, g.CreateUriNode(Foaf.Name), g.CreateLiteralNode(creator.Name)));
            }
            if (creator.Email != null)
            {
                g.Assert(new Triple(creatorNode, g.CreateUriNode(Foaf.MBox), g.CreateLiteralNode(creator.Email)));
            }

            g.Assert(new Triple(typeNode, g.CreateUriNode(DcTerms.Creator), creatorNode));
        }

        foreach (var contributor in contributors)
        {
            var contributorNode = g.CreateBlankNode();
            if (contributor.Name != null)
            {
                g.Assert(new Triple(contributorNode, g.CreateUriNode(Foaf.Name), g.CreateLiteralNode(contributor.Name)));
            }
            if (contributor.Email != null)
            {
                g.Assert(new Triple(contributorNode, g.CreateUriNode(Foaf.MBox), g.CreateLiteralNode(contributor.Email)));
            }

            g.Assert(new Triple(typeNode, g.CreateUriNode(DcTerms.Contributor), contributorNode));
        }
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

    private static (Uri, Uri) GetImfClass(ImfType type)
    {
        return type switch
        {
            AttributeType => (Imf.AttributeType, Imf.Attribute),
            BlockType => (Imf.BlockType, Imf.Block),
            TerminalType => (Imf.TerminalType, Imf.Terminal),
            _ => throw new ArgumentException("Unknown IMF type.")
        };
    }
}