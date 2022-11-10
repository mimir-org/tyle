namespace TypeLibrary.Data.Models
{
    public class PurposeLibDm
    {
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Source { get; set; }

        public string Id => Iri?[(Iri.LastIndexOf('/') + 1)..];
    }
}