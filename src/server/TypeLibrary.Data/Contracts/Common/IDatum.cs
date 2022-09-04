namespace TypeLibrary.Data.Contracts.Common
{
    public interface IDatum
    {
        string Id { get; set; }
        string Name { get; set; }
        string Iri { get; set; }
        string TypeReferences { get; set; }
        string Description { get; set; }
    }
}