namespace Mimirorg.TypeLibrary.Models.Client
{
    public class PurposeLibCm
    {
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Source { get; set; }

        public string Id => Iri?[(Iri.LastIndexOf('/') + 1)..];
        public string Kind => nameof(PurposeLibCm);
    }
}