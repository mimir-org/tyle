namespace Mimirorg.TypeLibrary.Models.Client
{
    public class RdsLibCm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Iri { get; set; }
        public string Kind => nameof(RdsLibCm);
    }
}
