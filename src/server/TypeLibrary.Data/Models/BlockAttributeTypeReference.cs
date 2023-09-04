using System;

namespace TypeLibrary.Data.Models;

public class BlockAttributeTypeReference
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public BlockType Block { get; set; } = null!;
    public AttributeType Attribute { get; set; } = null!;
}