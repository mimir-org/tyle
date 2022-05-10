namespace Mimirorg.TypeLibrary.Models.Client
{
    public class SymbolLibCm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public ICollection<string> ContentReferences { get; set; }
        public string Data { get; set; }

        public string Kind => nameof(SymbolLibCm);
    }
}