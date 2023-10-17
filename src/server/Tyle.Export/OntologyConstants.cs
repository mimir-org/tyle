using VDS.RDF;

namespace Tyle.Export;

public static class OntologyConstants
{
    public static readonly Uri Label = new("http://www.w3.org/2000/01/rdf-schema#label");
    public static readonly Uri Description = new("http://purl.org/dc/terms/description");
    public static readonly Uri Version = new("http://purl.org/pav/version");
    public static readonly Uri Created = new("http://purl.org/dc/terms/created");
    public static readonly Uri Creator = new("http://purl.org/dc/terms/creator");
    public static readonly Uri Contributor = new("http://purl.org/dc/terms/contributor");
    public static readonly Uri Modified = new("http://purl.org/dc/terms/modified");

    public static readonly Uri DateTime = new("http://www.w3.org/2001/XMLSchema#dateTime");
    public static readonly Uri String = new("http://www.w3.org/2001/XMLSchema#string");
    public static readonly Uri Decimal = new("http://www.w3.org/2001/XMLSchema#decimal");
    public static readonly Uri Integer = new("http://www.w3.org/2001/XMLSchema#integer");
    public static readonly Uri Boolean = new("http://www.w3.org/2001/XMLSchema#boolean");

    public static readonly Uri Property = new("http://www.w3.org/ns/shacl#property");
    public static readonly Uri Path = new("http://www.w3.org/ns/shacl#path");
    public static readonly Uri HasValue = new("http://www.w3.org/ns/shacl#hasValue");
    public static readonly Uri In = new("http://www.w3.org/ns/shacl#in");
    public static readonly Uri DataType = new("http://www.w3.org/ns/shacl#datatype");
    public static readonly Uri Pattern = new("http://www.w3.org/ns/shacl#pattern");
    public static readonly Uri MinInclusive = new("http://www.w3.org/ns/shacl#minInclusive");
    public static readonly Uri MaxInclusive = new("http://www.w3.org/ns/shacl#maxInclusive");
    public static readonly Uri MinCount = new("http://www.w3.org/ns/shacl#minCount");
    public static readonly Uri MaxCount = new("http://www.w3.org/ns/shacl#maxCount");


    public static readonly Uri ImfPredicate = new("http://ns.imfid.org/imf#predicate");
    public static readonly Uri ImfUom = new("http://ns.imfid.org/imf#uom");
    public static readonly Uri ImfHasAttributeQualifier = new("http://ns.imfid.org/imf#hasAttributeQualifier");
    public static readonly Uri ImfCalculatedQualifier = new("http://ns.imfid.org/imf#calculatedQualifier");
    public static readonly Uri ImfMeasuredQualifier = new("http://ns.imfid.org/imf#measuredQualifier");
    public static readonly Uri ImfSpecifiedQualifier = new("http://ns.imfid.org/imf#specifiedQualifier");
    public static readonly Uri ImfAverageQualifier = new("http://ns.imfid.org/imf#averageQualifier");
    public static readonly Uri ImfMaximumQualifier = new("http://ns.imfid.org/imf#maximumQualifier");
    public static readonly Uri ImfMinimumQualifier = new("http://ns.imfid.org/imf#minimumQualifier");
    public static readonly Uri ImfNominalQualifier = new("http://ns.imfid.org/imf#nominalQualifier");
    public static readonly Uri ImfNormalQualifier = new("http://ns.imfid.org/imf#normalQualifier");
    public static readonly Uri ImfAbsoluteQualifier = new("http://ns.imfid.org/imf#absoluteQualifier");
    public static readonly Uri ImfContinuousQualifier = new("http://ns.imfid.org/imf#continuousQualifier");
    public static readonly Uri ImfDesignQualifier = new("http://ns.imfid.org/imf#designQualifier");
    public static readonly Uri ImfOperatingQualifier = new("http://ns.imfid.org/imf#operatingQualifier");
    public static readonly Uri ImfValue = new("http://ns.imfid.org/imf#value");
}
