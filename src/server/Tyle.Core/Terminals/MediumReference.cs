using Tyle.Core.Common;

namespace Tyle.Core.Terminals;

public class MediumReference : RdlReference
{
    public MediumReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}