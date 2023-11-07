using System.Globalization;
using Newtonsoft.Json.Linq;
using Tyle.Converters.Iris;
using Tyle.Core.Attributes;
using VDS.RDF;
using VDS.RDF.JsonLd;
using VDS.RDF.Writing;

namespace Tyle.Converters;

public static class AttributeTypeConverter
{
    public static void AddAttributeType(this IGraph g, AttributeType attribute)
    {
        var attributeNode = g.CreateUriNode(new Uri($"http://tyle.imftools.com/attributes/{attribute.Id}"));

        // Add metadata

        g.AddMetadataTriples(attributeNode, attribute);

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
                g.CreateUriNode(EnumToIriMappers.GetAttributeQualifier(attribute.ProvenanceQualifier)));
        }

        if (attribute.RangeQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                Imf.HasAttributeQualifier,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetAttributeQualifier(attribute.RangeQualifier)));
        }

        if (attribute.RegularityQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                Imf.HasAttributeQualifier,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetAttributeQualifier(attribute.RegularityQualifier)));
        }

        if (attribute.ScopeQualifier != null)
        {
            g.AddShaclPropertyTriple(
                attributeNode,
                Imf.HasAttributeQualifier,
                Sh.HasValue,
                g.CreateUriNode(EnumToIriMappers.GetAttributeQualifier(attribute.ScopeQualifier)));
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
                        g.CreateLiteralNode(attribute.ValueConstraint.Pattern),
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
                            g.CreateLiteralNode(((decimal) attribute.ValueConstraint.MinValue).ToString(CultureInfo.InvariantCulture), EnumToIriMappers.GetDataType(attribute.ValueConstraint.DataType))));
                    }

                    if (attribute.ValueConstraint.MaxValue != null)
                    {
                        g.Assert(new Triple(
                            rangePropertyNode,
                            g.CreateUriNode(Sh.MaxInclusive),
                            g.CreateLiteralNode(((decimal) attribute.ValueConstraint.MaxValue).ToString(CultureInfo.InvariantCulture), EnumToIriMappers.GetDataType(attribute.ValueConstraint.DataType))));
                    }

                    break;
            }
        }
    }
}