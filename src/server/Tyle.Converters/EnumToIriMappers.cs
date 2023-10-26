using Tyle.Converters.Iris;
using Tyle.Core.Attributes;
using Tyle.Core.Common;
using Tyle.Core.Terminals;

namespace Tyle.Converters;

public static class EnumToIriMappers
{
    public static Uri GetAttributeQualifier(ProvenanceQualifier? qualifier) => qualifier switch
    {
        ProvenanceQualifier.CalculatedQualifier => Imf.CalculatedQualifier,
        ProvenanceQualifier.MeasuredQualifier => Imf.MeasuredQualifier,
        ProvenanceQualifier.SpecifiedQualifier => Imf.SpecifiedQualifier,
        _ => throw new ArgumentException("Unknown provenance qualifier.")
    };

    public static Uri GetAttributeQualifier(RangeQualifier? qualifier) => qualifier switch
    {
        RangeQualifier.AverageQualifier => Imf.AverageQualifier,
        RangeQualifier.MaximumQualifier => Imf.MaximumQualifier,
        RangeQualifier.MinimumQualifier => Imf.MinimumQualifier,
        RangeQualifier.NominalQualifier => Imf.NominalQualifier,
        RangeQualifier.NormalQualifier => Imf.NormalQualifier,
        _ => throw new ArgumentException("Unknown range qualifier.")
    };

    public static Uri GetAttributeQualifier(RegularityQualifier? qualifier) => qualifier switch
    {
        RegularityQualifier.AbsoluteQualifier => Imf.AbsoluteQualifier,
        RegularityQualifier.ContinuousQualifier => Imf.ContinuousQualifier,
        _ => throw new ArgumentException("Unknown regularity qualifier.")
    };

    public static Uri GetAttributeQualifier(ScopeQualifier? qualifier) => qualifier switch
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

    public static Uri GetAspect(Aspect? aspect) => aspect switch
    {
        Aspect.Function => Imf.FunctionAspect,
        Aspect.Product => Imf.ProductAspect,
        Aspect.Location => Imf.LocationAspect,
        Aspect.Installed => Imf.InstalledAspect,
        _ => throw new ArgumentException("Unknown aspect.")
    };

    public static Uri GetTerminalQualifier(Direction direction) => direction switch
    {
        Direction.Input => Imf.InputFlow,
        Direction.Output => Imf.OutputFlow,
        _ => throw new ArgumentException("Unknown or invalid direction.")
    };

    public static Uri GetHasTerminalPredicate(Direction direction) => direction switch
    {
        Direction.Bidirectional => Imf.HasTerminal,
        Direction.Input => Imf.HasInputTerminal,
        Direction.Output => Imf.HasOutputTerminal,
        _ => throw new ArgumentException("Unknown direction.")
    };
}