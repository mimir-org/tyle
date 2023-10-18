namespace Tyle.Converters.Iris;

public class Imf
{
    private const string NameSpace = "http://ns.imfid.org/imf#";

    // Classes
    public static readonly Uri Attribute = new($"{NameSpace}Attribute");
    public static readonly Uri Block = new($"{NameSpace}Block");
    public static readonly Uri Terminal = new($"{NameSpace}Terminal");

    // Object Properties
    public static readonly Uri Classifier = new($"{NameSpace}classifier");
    public static readonly Uri HasAspect = new($"{NameSpace}hasAspect");
    public static readonly Uri HasAttribute = new($"{NameSpace}hasAttribute");
    public static readonly Uri HasAttributeQualifier = new($"{NameSpace}hasAttributeQualifier");
    public static readonly Uri HasInputTerminal = new($"{NameSpace}hasInputTerminal");
    public static readonly Uri HasOutputTerminal = new($"{NameSpace}hasOutputTerminal");
    public static readonly Uri HasTerminal = new($"{NameSpace}hasTerminal");
    public static readonly Uri HasTerminalQualifier = new($"{NameSpace}hasTerminalQualifier");
    public static readonly Uri Medium = new($"{NameSpace}medium");
    public static readonly Uri Predicate = new($"{NameSpace}predicate");
    public static readonly Uri Purpose = new($"{NameSpace}purpose");
    public static readonly Uri Symbol = new($"{NameSpace}symbol");
    public static readonly Uri Uom = new($"{NameSpace}uom");

    // Data Properties
    public static readonly Uri Value = new($"{NameSpace}value");

    // Individuals
    public static readonly Uri AbsoluteQualifier = new($"{NameSpace}absoluteQualifier");
    public static readonly Uri AverageQualifier = new($"{NameSpace}averageQualifier");
    public static readonly Uri CalculatedQualifier = new($"{NameSpace}calculatedQualifier");
    public static readonly Uri ContinuousQualifier = new($"{NameSpace}continuousQualifier");
    public static readonly Uri DesignQualifier = new($"{NameSpace}designQualifier");
    public static readonly Uri FunctionAspect = new($"{NameSpace}functionAspect");
    public static readonly Uri InputFlow = new($"{NameSpace}inputFlow");
    public static readonly Uri InstalledAspect = new($"{NameSpace}installedAspect");
    public static readonly Uri LocationAspect = new($"{NameSpace}locationAspect");
    public static readonly Uri MaximumQualifier = new($"{NameSpace}maximumQualifier");
    public static readonly Uri MeasuredQualifier = new($"{NameSpace}measuredQualifier");
    public static readonly Uri MinimumQualifier = new($"{NameSpace}minimumQualifier");
    public static readonly Uri NominalQualifier = new($"{NameSpace}nominalQualifier");
    public static readonly Uri NormalQualifier = new($"{NameSpace}normalQualifier");
    public static readonly Uri OperatingQualifier = new($"{NameSpace}operatingQualifier");
    public static readonly Uri OutputFlow = new($"{NameSpace}outputFlow");
    public static readonly Uri ProductAspect = new($"{NameSpace}productAspect");
    public static readonly Uri SpecifiedQualifier = new($"{NameSpace}specifiedQualifier");
}