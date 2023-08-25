using System;
using System.Collections.Generic;

namespace TypeLibrary.Data.Models;

public class AttributeGroupLibDm
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<AttributeGroupMappingLibDm> Attributes { get; set; } = null!;
}
