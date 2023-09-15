namespace Tyle.Core.Common;

public class PurposeReference : RdlReference
{
    public PurposeReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}