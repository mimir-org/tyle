namespace Tyle.Converters.Iris;

public class Foaf
{
    private const string NameSpace = "http://xmlns.com/foaf/0.1/";

    public static readonly Uri Name = new($"{NameSpace}name");
    public static readonly Uri MBox = new($"{NameSpace}mbox");
}