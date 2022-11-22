namespace TypeLibrary.Data.Models
{
    public class UnitLibDm
    {
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Symbol { get; set; }
        public string Source { get; set; }
        public bool IsDefault { get; set; }

        public string Id => Iri?[(Iri.LastIndexOf('/') + 1)..];
    }
}