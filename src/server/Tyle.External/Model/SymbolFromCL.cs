namespace Tyle.External.Model
{
    public class SymbolFromCL
    {
        public Uri Iri { get; set; }
        public string Label { get; set; }
        public string Description { get; set; } = "";
        public string Path { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public List<ConnectionPointFromCL> ConnectionPoints { get; set; } = new();
    }

    public class ConnectionPointFromCL
    {
        public string Identifier { get; set; } = "";
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }

}