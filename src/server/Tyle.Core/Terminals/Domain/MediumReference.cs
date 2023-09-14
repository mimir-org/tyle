using Tyle.Core.Common.Domain;

namespace Tyle.Core.Terminals.Domain;

public class MediumReference : RdlReference
{
    public MediumReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}