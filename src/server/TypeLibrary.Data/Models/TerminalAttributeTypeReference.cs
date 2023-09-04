using System;

namespace TypeLibrary.Data.Models;

public class TerminalAttributeTypeReference
{
    public Guid Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; } = null!;
    public Guid AttributeId { get; set; }
    public AttributeType Attribute { get; set; } = null!;
}