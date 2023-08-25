using System;

namespace TypeLibrary.Data.Models;

public class TerminalAttributeLibDm
{
    public Guid Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Guid TerminalId { get; set; }
    public TerminalLibDm Terminal { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeLibDm Attribute { get; set; } = null!;
}