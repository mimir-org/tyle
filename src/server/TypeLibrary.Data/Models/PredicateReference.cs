using System.Collections.Generic;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Models;

public class PredicateReference : GenericReference
{
    public PredicateReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}