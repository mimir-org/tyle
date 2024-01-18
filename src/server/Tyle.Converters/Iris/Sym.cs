namespace Tyle.Converters.Iris;

public class Sym
{
    private const string NameSpace = "http://example.equinor.com/symbol#";

    public static readonly Uri HasConnectionPoint = new($"{NameSpace}hasConnectionPoint");
    public static readonly Uri HasSerialization = new($"{NameSpace}hasSerialization");
    public static readonly Uri Height = new($"{NameSpace}height");
    public static readonly Uri IsPointOn = new($"{NameSpace}isPointOn");
    public static readonly Uri PositionX = new($"{NameSpace}positionX");
    public static readonly Uri PositionY = new($"{NameSpace}positionY");
    public static readonly Uri Symbol = new($"{NameSpace}Symbol");
    public static readonly Uri Width = new($"{NameSpace}width");
}