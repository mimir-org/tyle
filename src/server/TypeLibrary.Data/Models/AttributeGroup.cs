using System;
using System.Collections.Generic;

namespace TypeLibrary.Data.Models;

public class AttributeGroup
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<AttributeGroupMapping> Attributes { get; set; } = null!;
}
