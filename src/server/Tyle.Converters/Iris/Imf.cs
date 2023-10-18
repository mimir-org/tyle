namespace Tyle.Converters.Iris;

public class Imf
{
    private const string NameSpace = "http://ns.imfid.org/imf#";

    public static readonly Uri Predicate = new($"{NameSpace}predicate");
    public static readonly Uri Uom = new($"{NameSpace}uom");
    public static readonly Uri HasAttributeQualifier = new($"{NameSpace}hasAttributeQualifier");
    public static readonly Uri CalculatedQualifier = new($"{NameSpace}calculatedQualifier");
    public static readonly Uri MeasuredQualifier = new($"{NameSpace}measuredQualifier");
    public static readonly Uri SpecifiedQualifier = new($"{NameSpace}specifiedQualifier");
    public static readonly Uri AverageQualifier = new($"{NameSpace}averageQualifier");
    public static readonly Uri MaximumQualifier = new($"{NameSpace}maximumQualifier");
    public static readonly Uri MinimumQualifier = new($"{NameSpace}minimumQualifier");
    public static readonly Uri NominalQualifier = new($"{NameSpace}nominalQualifier");
    public static readonly Uri NormalQualifier = new($"{NameSpace}normalQualifier");
    public static readonly Uri AbsoluteQualifier = new($"{NameSpace}absoluteQualifier");
    public static readonly Uri ContinuousQualifier = new($"{NameSpace}continuousQualifier");
    public static readonly Uri DesignQualifier = new($"{NameSpace}designQualifier");
    public static readonly Uri OperatingQualifier = new($"{NameSpace}operatingQualifier");
    public static readonly Uri Value = new($"{NameSpace}value");
}
