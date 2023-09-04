using System;

namespace TypeLibrary.Data.Models;

public class TerminalAttributeTypeReference
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public TerminalType Terminal { get; set; } = null!;
    public AttributeType Attribute { get; set; } = null!;
}