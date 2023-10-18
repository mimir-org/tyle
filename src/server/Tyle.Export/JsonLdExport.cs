using Newtonsoft.Json.Linq;
using Tyle.Core.Attributes;
using Tyle.Export.Iris;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Writing;

namespace Tyle.Export;

public static class JsonLdExport
{
    public static JObject ConvertAttributeType(AttributeType attribute)
    {
        var g = new Graph();

        var attributeNode = g.CreateUriNode(new Uri($"http://tyle.imftools.com/attribute/{attribute.Id}"));

        // Add metadata

        g.AddLiteralTriple(attributeNode, Rdfs.Label, attribute.Name);
        g.AddLiteralTriple(attributeNode, DcTerms.Description, attribute.Description);
        g.AddLiteralTriple(attributeNode, Pav.Version, attribute.Version);
        g.AddLiteralTriple(attributeNode, DcTerms.Created, attribute.CreatedOn);

        // TODO: Created by, contributed by, last update on

        // Add predicate reference

        if (attribute.Predicate != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                Imf.Predicate,
                Sh.HasValue,
                g.CreateUriNode(attribute.Predicate.Iri));
        }

        // Add unit references

        if (attribute.UnitMaxCount == 0)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                Imf.Uom,
                Sh.MaxCount,
                g.CreateLiteralNode(attribute.UnitMaxCount.ToString(), Xsd.Integer));
        }
        else
        {
            if (attribute.Units.Count == 1 && attribute.UnitMinCount == 1)
            {
                g.AddShaclPropertyTriple(
                    attributeNode,
                    Imf.Uom,
                    Sh.HasValue,
                    g.CreateUriNode(attribute.Units.First().Unit.Iri));
            }
            else if (attribute.Units.Count > 0)
            {
                g.AddShaclPropertyTriple(
                    attributeNode,
                    Imf.Uom,
                    Sh.In,
                    g.AssertList(new List<INode>(attribute.Units.Select(x => g.CreateUriNode(x.Unit.Iri)))),
                    out var propertyNode);

                g.Assert(new Triple(
                    propertyNode,
                    g.CreateUriNode(Sh.MinCount),
                    g.CreateLiteralNode(attribute.UnitMinCount.ToString(), Xsd.Integer)));
            }
            else if (attribute.UnitMinCount == 1)
            {
                g.AddShaclPropertyTriple(
                    attributeNode,
                    Imf.Uom,
                    Sh.MinCount,
                    g.CreateLiteralNode(attribute.UnitMinCount.ToString(), Xsd.Integer));
            }
        }

        // Add attribute qualifiers

        if (attribute.ProvenanceQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                Imf.HasAttributeQualifier,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetImfQualifier(attribute.ProvenanceQualifier)));
        }

        if (attribute.RangeQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                Imf.HasAttributeQualifier,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetImfQualifier(attribute.RangeQualifier)));
        }

        if (attribute.RegularityQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                Imf.HasAttributeQualifier,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetImfQualifier(attribute.RegularityQualifier)));
        }

        if (attribute.ScopeQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                Imf.HasAttributeQualifier,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetImfQualifier(attribute.ScopeQualifier)));
        }

        // Add value constraint

        if (attribute.ValueConstraint != null)
        {
            switch (attribute.ValueConstraint.ConstraintType)
            {
                case ConstraintType.HasValue:

                    g.AddShaclPropertyTriple(
                        attributeNode,
                        Imf.Value,
                        Sh.HasValue,
                        g.CreateLiteralNode(attribute.ValueConstraint.Value, EnumToIriMappers.GetDataType(attribute.ValueConstraint.DataType)));

                    break;

                case ConstraintType.In:

                    g.AddShaclPropertyTriple(
                        attributeNode,
                        Imf.Value,
                        Sh.In,
                        g.AssertList(new List<INode>(attribute.ValueConstraint.ValueList.Select(x =>
                            g.CreateLiteralNode(x.EntryValue, EnumToIriMappers.GetDataType(attribute.ValueConstraint.DataType))))),
                        out var inPropertyNode);


                    g.Assert(new Triple(
                        inPropertyNode,
                        g.CreateUriNode(Sh.MinCount),
                        g.CreateLiteralNode(attribute.ValueConstraint.MinCount.ToString(), Xsd.Integer)));

                    break;

                case ConstraintType.DataType:

                    g.AddShaclPropertyTriple(
                        attributeNode,
                        Imf.Value,
                        Sh.DataType,
                        g.CreateUriNode(EnumToIriMappers.GetDataType(attribute.ValueConstraint.DataType)),
                        out var dataTypePropertyNode);

                    g.Assert(new Triple(
                        dataTypePropertyNode,
                        g.CreateUriNode(Sh.MinCount),
                        g.CreateLiteralNode(attribute.ValueConstraint.MinCount.ToString(), Xsd.Integer)));

                    break;

                case ConstraintType.Pattern:

                    g.AddShaclPropertyTriple(
                        attributeNode,
                        Imf.Value,
                        Sh.Pattern,
                        g.CreateUriNode(attribute.ValueConstraint.Pattern),
                        out var patternPropertyNode);

                    g.Assert(new Triple(
                        patternPropertyNode,
                        g.CreateUriNode(Sh.MinCount),
                        g.CreateLiteralNode(attribute.ValueConstraint.MinCount.ToString(), Xsd.Integer)));

                    break;

                case ConstraintType.Range:

                    g.AddShaclPropertyTriple(
                        attributeNode,
                        Imf.Value,
                        Sh.MinCount,
                        g.CreateLiteralNode(attribute.ValueConstraint.MinCount.ToString(), Xsd.Integer),
                        out var rangePropertyNode);

                    if (attribute.ValueConstraint.MinValue != null)
                    {
                        g.Assert(new Triple(
                            rangePropertyNode,
                            g.CreateUriNode(Sh.MinInclusive),
                            g.CreateLiteralNode(attribute.ValueConstraint.MinValue.ToString(), EnumToIriMappers.GetDataType(attribute.ValueConstraint.DataType))));
                    }

                    if (attribute.ValueConstraint.MaxValue != null)
                    {
                        g.Assert(new Triple(
                            rangePropertyNode,
                            g.CreateUriNode(Sh.MaxInclusive),
                            g.CreateLiteralNode(attribute.ValueConstraint.MaxValue.ToString(), EnumToIriMappers.GetDataType(attribute.ValueConstraint.DataType))));
                    }

                    break;
            }
        }

        var store = new TripleStore();
        store.Add(g);

        var jsonLdWriter = new JsonLdWriter();
        var serializedGraph = jsonLdWriter.SerializeStore(store);
        var jsonString = $"{{ \"@graph\": {serializedGraph} }}";

        const string context = """
                               {
                               "@context": [
                                       "http://jsonld-context.dyreriket.xyz/rdfs.json",
                                       "http://jsonld-context.dyreriket.xyz/sh.json",
                                       "http://jsonld-context.dyreriket.xyz/pav.json",
                                       "http://jsonld-context.dyreriket.xyz/dc.json",
                                       "http://jsonld-context.dyreriket.xyz/skos.json",
                                       "https://imf-lab.gitlab.io/imf-ontology/out/json/imf-context.json",
                                       {
                                           "@version": 1.1,
                                           "dc": "http://purl.org/dc/elements/1.1/",
                                           "ex": "http://example.com/example/",
                                           "foaf": "http://xmlns.com/foaf/0.1/",
                                           "imf": "http://ns.imfid.org/imf#",
                                           "owl": "http://www.w3.org/2002/07/owl#",
                                           "pav": "http://purl.org/pav/",
                                           "pca-plm": "http://rds.posccaesar.org/ontology/plm/rdl/",
                                           "rdf": "http://www.w3.org/1999/02/22-rdf-syntax-ns#",
                                           "rdfs": "http://www.w3.org/2000/01/rdf-schema#",
                                           "sh": "http://www.w3.org/ns/shacl#",
                                           "skos": "http://www.w3.org/2004/02/skos/core#",
                                           "vann": "http://purl.org/vocab/vann/",
                                           "vs": "http://www.w3.org/2003/06/sw-vocab-status/ns#",
                                           "xsd": "http://www.w3.org/2001/XMLSchema#"
                                       }
                               ]
                               }
                               """;

        const string frame = """
                             {
                             "@context": [
                                     "http://jsonld-context.dyreriket.xyz/rdfs.json",
                                     "http://jsonld-context.dyreriket.xyz/sh.json",
                                     "http://jsonld-context.dyreriket.xyz/pav.json",
                                     "http://jsonld-context.dyreriket.xyz/dc.json",
                                     "http://jsonld-context.dyreriket.xyz/skos.json",
                                     "https://imf-lab.gitlab.io/imf-ontology/out/json/imf-context.json",
                                     {
                                         "@version": 1.1,
                                         "dc": "http://purl.org/dc/elements/1.1/",
                                         "ex": "http://example.com/example/",
                                         "foaf": "http://xmlns.com/foaf/0.1/",
                                         "imf": "http://ns.imfid.org/imf#",
                                         "owl": "http://www.w3.org/2002/07/owl#",
                                         "pav": "http://purl.org/pav/",
                                         "pca-plm": "http://rds.posccaesar.org/ontology/plm/rdl/",
                                         "rdf": "http://www.w3.org/1999/02/22-rdf-syntax-ns#",
                                         "rdfs": "http://www.w3.org/2000/01/rdf-schema#",
                                         "sh": "http://www.w3.org/ns/shacl#",
                                         "skos": "http://www.w3.org/2004/02/skos/core#",
                                         "vann": "http://purl.org/vocab/vann/",
                                         "vs": "http://www.w3.org/2003/06/sw-vocab-status/ns#",
                                         "xsd": "http://www.w3.org/2001/XMLSchema#"
                                     }
                             ],
                             "sh:property": {}
                             }
                             """;


        var flattened = JsonLdProcessor.Flatten(JToken.Parse(jsonString), JToken.Parse(context), new JsonLdProcessorOptions());
        var result = JsonLdProcessor.Frame(flattened, JToken.Parse(frame), new JsonLdProcessorOptions());

        return result;
    }
}
