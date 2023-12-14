namespace Tyle.External.Model
{
    public class SymbolFromCL
    {
        public string Iri { get; set; }
        public string Label { get; set; }
        public string Description { get; set; } = "";
        public string Path { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public List<ConnectionPoint> ConnectionPoints { get; set; } = new();
    }

    public class ConnectionPoint
    {
        public string Identifier { get; set; } = "";
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public string ConnectorDirection { get; set; } = "";
    }

}