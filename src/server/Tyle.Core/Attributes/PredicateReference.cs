using Tyle.Core.Common;

namespace Tyle.Core.Attributes;

public class PredicateReference : RdlReference
{
    public PredicateReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}