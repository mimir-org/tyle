using System;

namespace TypeLibrary.Data.Models;

public class BlockAttributeTypeReference
{
    public Guid Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
}