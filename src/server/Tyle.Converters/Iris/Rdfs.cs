namespace Tyle.Converters.Iris;

public class Rdfs
{
    private const string NameSpace = "http://www.w3.org/2000/01/rdf-schema#";

    public static readonly Uri Class = new($"{NameSpace}Class");
    public static readonly Uri Label = new($"{NameSpace}label");
    public static readonly Uri SubClassOf = new($"{NameSpace}subClassOf");
}