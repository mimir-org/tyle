using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models;

public class PredicateReference
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Iri { get; set; }
    public ReferenceSource Source { get; set; } = ReferenceSource.UserSubmission;

    public ICollection<AttributeType> Attributes { get; } = new List<AttributeType>();
}