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

        var nameTriple = new Triple(
            attributeNode,
            g.CreateUriNode(OntologyConstants.Label),
            g.CreateLiteralNode(attribute.Name));
        g.Assert(nameTriple);

        var descriptionTriple = new Triple(
            attributeNode,
            g.CreateUriNode(OntologyConstants.Description),
            g.CreateLiteralNode(attribute.Description));
        g.Assert(descriptionTriple);

        var versionTriple = new Triple(
            attributeNode,
            g.CreateUriNode(OntologyConstants.Version),
            g.CreateLiteralNode(attribute.Version));
        g.Assert(versionTriple);

        var createdTriple = new Triple(
            attributeNode,
            g.CreateUriNode(OntologyConstants.Created),
            g.CreateLiteralNode(attribute.CreatedOn.ToString(), OntologyConstants.DateTime));
        g.Assert(createdTriple);

        // TODO: Created by, contributed by, last update on

        if (attribute.Predicate != null)
        {
            g.AddHasValueTriple(attributeNode, OntologyConstants.ImfPredicate, attribute.Predicate.Iri);
        }

        // TODO: Include logic surrounding unit min count and max count

        if (attribute.Units.Count > 0)
        {
            if (attribute.Units.Count == 1)
            {
                g.AddHasValueTriple(attributeNode, OntologyConstants.ImfUom, attribute.Units.First().Unit.Iri);
            }
            else
            {
                g.AddInTriple(attributeNode, OntologyConstants.ImfUom, attribute.Units.Select(x => x.Unit.Iri));
            }
        }

        if (attribute.ProvenanceQualifier != null)
        {
            g.AddHasValueTriple(attributeNode, OntologyConstants.ImfHasAttributeQualifier, GetImfQualifier(attribute.ProvenanceQualifier));
        }

        if (attribute.RangeQualifier != null)
        {
            g.AddHasValueTriple(attributeNode, OntologyConstants.ImfHasAttributeQualifier, GetImfQualifier(attribute.RangeQualifier));
        }

        if (attribute.RegularityQualifier != null)
        {
            g.AddHasValueTriple(attributeNode, OntologyConstants.ImfHasAttributeQualifier, GetImfQualifier(attribute.RegularityQualifier));
        }

        if (attribute.ScopeQualifier != null)
        {
            g.AddHasValueTriple(attributeNode, OntologyConstants.ImfHasAttributeQualifier, GetImfQualifier(attribute.ScopeQualifier));
        }

        if (attribute.ValueConstraint != null)
        {
            switch (attribute.ValueConstraint.ConstraintType)
            {
                case ConstraintType.HasValue:
                    g.AddHasValueTriple(attributeNode, OntologyConstants.ImfValue, attribute.ValueConstraint.Value!, GetDataType(attribute.ValueConstraint.DataType));
                    break;
                case ConstraintType.In:
                    g.AddInTriple(attributeNode, OntologyConstants.ImfValue, attribute.ValueConstraint.ValueList.Select(x => x.EntryValue), GetDataType(attribute.ValueConstraint.DataType));
                    break;
                case ConstraintType.DataType:
                    g.AddDataTypeTriple(attributeNode, OntologyConstants.ImfValue, GetDataType(attribute.ValueConstraint.DataType));
                    break;
                case ConstraintType.Pattern:
                    g.AddPatternTriple(attributeNode, OntologyConstants.ImfValue, attribute.ValueConstraint.Pattern!);
                    break;
                case ConstraintType.Range:
                    g.AddRangeTriples(attributeNode, OntologyConstants.ImfValue, attribute.ValueConstraint.MinValue.ToString(), attribute.ValueConstraint.MaxValue.ToString(), GetDataType(attribute.ValueConstraint.DataType));
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
