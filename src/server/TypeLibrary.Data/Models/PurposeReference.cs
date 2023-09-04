using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models;

public class PurposeReference
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Iri { get; set; }
    public ReferenceSource Source { get; set; } = ReferenceSource.UserSubmission;
}