namespace Tyle.Core.Common;

public class ClassifierReference : RdlReference
{
    public ClassifierReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}