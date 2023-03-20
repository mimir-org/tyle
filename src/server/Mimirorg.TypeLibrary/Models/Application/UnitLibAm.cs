using System.ComponentModel.DataAnnotations;
using TypeScriptBuilder;

namespace Mimirorg.TypeLibrary.Models.Application;

public class UnitLibAm
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Iri { get; set; }

    public string Symbol { get; set; }

    [TSExclude]
    public string Source { get; set; }

    [TSExclude]
    public bool IsDefault { get; set; }

    [TSExclude]
    public string Id => Iri?[(Iri.LastIndexOf('/') + 1)..];
}