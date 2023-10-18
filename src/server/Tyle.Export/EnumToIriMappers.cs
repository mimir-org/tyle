using Tyle.Core.Attributes;
using Tyle.Export.Iris;

namespace Tyle.Export;

public static class EnumToIriMappers
{
    public static Uri GetImfQualifier(ProvenanceQualifier? qualifier) => qualifier switch
    {
        ProvenanceQualifier.CalculatedQualifier => Imf.CalculatedQualifier,
        ProvenanceQualifier.MeasuredQualifier => Imf.MeasuredQualifier,
        ProvenanceQualifier.SpecifiedQualifier => Imf.SpecifiedQualifier,
        _ => throw new ArgumentException("Unknown provenance qualifier.")
    };

    public static Uri GetImfQualifier(RangeQualifier? qualifier) => qualifier switch
    {
        RangeQualifier.AverageQualifier => Imf.AverageQualifier,
        RangeQualifier.MaximumQualifier => Imf.MaximumQualifier,
        RangeQualifier.MinimumQualifier => Imf.MinimumQualifier,
        RangeQualifier.NominalQualifier => Imf.NominalQualifier,
        RangeQualifier.NormalQualifier => Imf.NormalQualifier,
        _ => throw new ArgumentException("Unknown range qualifier.")
    };

    public static Uri GetImfQualifier(RegularityQualifier? qualifier) => qualifier switch
    {
        RegularityQualifier.AbsoluteQualifier => Imf.AbsoluteQualifier,
        RegularityQualifier.ContinuousQualifier => Imf.ContinuousQualifier,
        _ => throw new ArgumentException("Unknown regularity qualifier.")
    };

    public static Uri GetImfQualifier(ScopeQualifier? qualifier) => qualifier switch
    {
        ScopeQualifier.DesignQualifier => Imf.DesignQualifier,
        ScopeQualifier.OperatingQualifier => Imf.OperatingQualifier,
        _ => throw new ArgumentException("Unknown scope qualifier.")
    };

    public static Uri GetDataType(XsdDataType dataType) => dataType switch
    {
        XsdDataType.String => Xsd.String,
        XsdDataType.Decimal => Xsd.Decimal,
        XsdDataType.Integer => Xsd.Integer,
        XsdDataType.Boolean => Xsd.Boolean,
        _ => throw new ArgumentException("Unknown XSD data type.")
    };
}
