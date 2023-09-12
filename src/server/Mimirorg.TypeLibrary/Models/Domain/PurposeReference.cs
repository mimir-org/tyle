using Mimirorg.TypeLibrary.Enums;

namespace Mimirorg.TypeLibrary.Models.Domain;

public class PurposeReference
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Iri { get; set; }
    public ReferenceSource Source { get; set; } = ReferenceSource.UserSubmission;

    public ICollection<BlockType> Blocks { get; set; } = new List<BlockType>();
    public ICollection<TerminalType> Terminals { get; set; } = new List<TerminalType>();
}