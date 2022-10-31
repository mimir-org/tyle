namespace Mimirorg.TypeLibrary.Models.Client
{
    public class UnitLibCm
    {
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Symbol { get; set; }
        public string Source { get; set; }
        public bool IsDefault { get; set; }

        public string Kind => nameof(UnitLibCm);
        public string Id => Iri?[(Iri.LastIndexOf('/') + 1)..];
    }
}