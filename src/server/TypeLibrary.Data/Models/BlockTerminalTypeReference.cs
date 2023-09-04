using System;
using Mimirorg.TypeLibrary.Enums;
using VDS.RDF;

namespace TypeLibrary.Data.Models;

public class BlockTerminalTypeReference
{
    public int Id { get; set; }
    public int MinCount { get; set; }
    public int? MaxCount { get; set; }
    public Direction Direction { get; set; }
    public BlockType Block { get; set; } = null!;
    public TerminalType Terminal { get; set; } = null!;

    public string GetHash()
    {
        return $"{MinCount}-{MaxCount}-{Direction}-{Terminal.Id}".GetSha256Hash();
    }
}