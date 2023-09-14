using Tyle.Core.Common.Domain;

namespace Tyle.Core.Attributes.Domain;

public class PredicateReference : RdlReference
{
    public PredicateReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}