namespace Tyle.Converters.Iris;

public class Xsd
{
    private const string NameSpace = "http://www.w3.org/2001/XMLSchema#";

    public static readonly Uri DateTime = new($"{NameSpace}dateTime");
    public static readonly Uri String = new($"{NameSpace}string");
    public static readonly Uri Decimal = new($"{NameSpace}decimal");
    public static readonly Uri Integer = new($"{NameSpace}integer");
    public static readonly Uri Boolean = new($"{NameSpace}boolean");
}