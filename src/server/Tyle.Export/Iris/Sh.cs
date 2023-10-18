namespace Tyle.Export.Iris;

public class Sh
{
    private const string NameSpace = "http://www.w3.org/ns/shacl#";

    public static readonly Uri Property = new($"{NameSpace}property");
    public static readonly Uri Path = new($"{NameSpace}path");
    public static readonly Uri HasValue = new($"{NameSpace}hasValue");
    public static readonly Uri In = new($"{NameSpace}in");
    public static readonly Uri DataType = new($"{NameSpace}datatype");
    public static readonly Uri Pattern = new($"{NameSpace}pattern");
    public static readonly Uri MinInclusive = new($"{NameSpace}minInclusive");
    public static readonly Uri MaxInclusive = new($"{NameSpace}maxInclusive");
    public static readonly Uri MinCount = new($"{NameSpace}minCount");
    public static readonly Uri MaxCount = new($"{NameSpace}maxCount");
}
