namespace Tyle.Converters.Iris;

public class DcTerms
{
    private const string NameSpace = "http://purl.org/dc/terms/";

    public static readonly Uri Description = new($"{NameSpace}description");
    public static readonly Uri Created = new($"{NameSpace}created");
    public static readonly Uri Creator = new($"{NameSpace}creator");
    public static readonly Uri Contributor = new($"{NameSpace}contributor");
    public static readonly Uri Modified = new($"{NameSpace}modified");
    public static readonly Uri Identifier = new($"{NameSpace}identifier");
}