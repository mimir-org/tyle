using Newtonsoft.Json.Linq;
using Tyle.Core.Attributes;
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

        g.AddLiteralTriple(attributeNode, OntologyConstants.Label, attribute.Name);
        g.AddLiteralTriple(attributeNode, OntologyConstants.Description, attribute.Description);
        g.AddLiteralTriple(attributeNode, OntologyConstants.Version, attribute.Version);
        g.AddLiteralTriple(attributeNode, OntologyConstants.Created, attribute.CreatedOn);

        // TODO: Created by, contributed by, last update on

        if (attribute.Predicate != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                OntologyConstants.ImfPredicate,
                OntologyConstants.HasValue,
                g.CreateUriNode(attribute.Predicate.Iri));
        }

        if (attribute.UnitMaxCount == 0)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                OntologyConstants.ImfUom,
                OntologyConstants.MaxCount,
                g.CreateLiteralNode(attribute.UnitMaxCount.ToString(), OntologyConstants.Integer));
        }
        else
        {
            if (attribute.Units.Count == 1 && attribute.UnitMinCount == 1)
            {
                g.AddShaclPropertyTriple(
                    attributeNode,
                    OntologyConstants.ImfUom,
                    OntologyConstants.HasValue,
                    g.CreateUriNode(attribute.Units.First().Unit.Iri));
            }
            else if (attribute.Units.Count > 0)
            {
                var unitPropertyNode = g.CreateBlankNode();

                g.Assert(new Triple(
                    unitPropertyNode, 
                    g.CreateUriNode(OntologyConstants.Path),
                    g.CreateUriNode(OntologyConstants.ImfUom)));

                g.Assert(new Triple(
                    unitPropertyNode,
                    g.CreateUriNode(OntologyConstants.MinCount),
                    g.CreateLiteralNode(attribute.UnitMinCount.ToString(), OntologyConstants.Integer)));

                g.Assert(new Triple(
                    unitPropertyNode,
                    g.CreateUriNode(OntologyConstants.In),
                    g.AssertList(new List<INode>(attribute.Units.Select(x => g.CreateUriNode(x.Unit.Iri))))));

                g.Assert(new Triple(attributeNode, g.CreateUriNode(OntologyConstants.Property), unitPropertyNode));
            }
            else if (attribute.UnitMinCount == 1)
            {
                g.AddShaclPropertyTriple(
                    attributeNode,
                    OntologyConstants.ImfUom,
                    OntologyConstants.MinCount,
                    g.CreateLiteralNode(attribute.UnitMinCount.ToString(), OntologyConstants.Integer));
            }
        }

        if (attribute.ProvenanceQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                OntologyConstants.ImfHasAttributeQualifier,
                OntologyConstants.HasValue,
                g.CreateUriNode(GetImfQualifier(attribute.ProvenanceQualifier)));
        }

        if (attribute.RangeQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                OntologyConstants.ImfHasAttributeQualifier,
                OntologyConstants.HasValue,
                g.CreateUriNode(GetImfQualifier(attribute.RangeQualifier)));
        }

        if (attribute.RegularityQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                OntologyConstants.ImfHasAttributeQualifier,
                OntologyConstants.HasValue,
                g.CreateUriNode(GetImfQualifier(attribute.RegularityQualifier)));
        }

        if (attribute.ScopeQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                OntologyConstants.ImfHasAttributeQualifier,
                OntologyConstants.HasValue,
                g.CreateUriNode(GetImfQualifier(attribute.ScopeQualifier)));
        }

        if (attribute.ValueConstraint != null)
        {
            switch (attribute.ValueConstraint.ConstraintType)
            {
                case ConstraintType.HasValue:
                    g.AddShaclPropertyTriple(
                        attributeNode,
                        OntologyConstants.ImfValue,
                        OntologyConstants.HasValue,
                        g.CreateLiteralNode(attribute.ValueConstraint.Value, GetDataType(attribute.ValueConstraint.DataType)));
                    break;
                case ConstraintType.In:
                    g.AddShaclPropertyTriple(
                        attributeNode,
                        OntologyConstants.ImfValue,
                        OntologyConstants.In,
                        g.AssertList(new List<INode>(attribute.ValueConstraint.ValueList.Select(x =>
                            g.CreateLiteralNode(x.EntryValue, GetDataType(attribute.ValueConstraint.DataType))))));
                    break;
                case ConstraintType.DataType:
                    g.AddShaclPropertyTriple(
                        attributeNode,
                        OntologyConstants.ImfValue,
                        OntologyConstants.DataType,
                        g.CreateUriNode(GetDataType(attribute.ValueConstraint.DataType)));
                    break;
                case ConstraintType.Pattern:
                    g.AddShaclPropertyTriple(
                        attributeNode,
                        OntologyConstants.ImfValue,
                        OntologyConstants.Pattern,
                        g.CreateUriNode(attribute.ValueConstraint.Pattern));
                    break;
                case ConstraintType.Range:
                    if (attribute.ValueConstraint.MinValue != null)
                    {
                        g.AddShaclPropertyTriple(
                            attributeNode,
                            OntologyConstants.ImfValue,
                            OntologyConstants.MinInclusive,
                            g.CreateLiteralNode(attribute.ValueConstraint.MinValue.ToString(), GetDataType(attribute.ValueConstraint.DataType)));
                    }
                    if (attribute.ValueConstraint.MaxValue != null)
                    {
                        g.AddShaclPropertyTriple(
                            attributeNode,
                            OntologyConstants.ImfValue,
                            OntologyConstants.MaxInclusive,
                            g.CreateLiteralNode(attribute.ValueConstraint.MaxValue.ToString(), GetDataType(attribute.ValueConstraint.DataType)));
                    }
                    break;
            }
        }

        var store = new TripleStore();
        store.Add(g);

        var jsonLdWriter = new JsonLdWriter();
        var test = jsonLdWriter.SerializeStore(store);
        var jsonString = $"{{ \"@graph\": {test} }}";

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

    private static Uri GetImfQualifier(ProvenanceQualifier? qualifier) => qualifier switch
    {
        ProvenanceQualifier.CalculatedQualifier => OntologyConstants.ImfCalculatedQualifier,
        ProvenanceQualifier.MeasuredQualifier => OntologyConstants.ImfMeasuredQualifier,
        ProvenanceQualifier.SpecifiedQualifier => OntologyConstants.ImfSpecifiedQualifier
    };

    private static Uri GetImfQualifier(RangeQualifier? qualifier) => qualifier switch
    {
        RangeQualifier.AverageQualifier => OntologyConstants.ImfAverageQualifier,
        RangeQualifier.MaximumQualifier => OntologyConstants.ImfMaximumQualifier,
        RangeQualifier.MinimumQualifier => OntologyConstants.ImfMinimumQualifier,
        RangeQualifier.NominalQualifier => OntologyConstants.ImfNominalQualifier,
        RangeQualifier.NormalQualifier => OntologyConstants.ImfNormalQualifier
    };

    private static Uri GetImfQualifier(RegularityQualifier? qualifier) => qualifier switch
    {
        RegularityQualifier.AbsoluteQualifier => OntologyConstants.ImfAbsoluteQualifier,
        RegularityQualifier.ContinuousQualifier => OntologyConstants.ImfContinuousQualifier
    };

    private static Uri GetImfQualifier(ScopeQualifier? qualifier) => qualifier switch
    {
        ScopeQualifier.DesignQualifier => OntologyConstants.ImfDesignQualifier,
        ScopeQualifier.OperatingQualifier => OntologyConstants.ImfOperatingQualifier
    };

    private static Uri GetDataType(XsdDataType dataType) => dataType switch
    {
        XsdDataType.String => OntologyConstants.String,
        XsdDataType.Decimal => OntologyConstants.Decimal,
        XsdDataType.Integer => OntologyConstants.Integer,
        XsdDataType.Boolean => OntologyConstants.Boolean
    };
}
