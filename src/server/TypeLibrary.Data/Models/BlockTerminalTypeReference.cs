using System;
using Mimirorg.TypeLibrary.Enums;
using VDS.RDF;

namespace TypeLibrary.Data.Models;

public class BlockTerminalTypeReference
{
    public Guid Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Direction Direction { get; set; }
    public Guid BlockId { get; set; }
    public BlockType Block { get; set; } = null!;
    public Guid TerminalId { get; set; }
    public TerminalType Terminal { get; set; } = null!;

    public string GetHash()
    {
        return $"{MinCount}-{MaxCount}-{Direction}-{TerminalId}".GetSha256Hash();
    }
}