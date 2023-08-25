using System;

namespace TypeLibrary.Data.Models;

public class BlockAttributeLibDm
{
    public Guid Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Guid BlockId { get; set; }
    public BlockLibDm Block { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set; } = null!;
}