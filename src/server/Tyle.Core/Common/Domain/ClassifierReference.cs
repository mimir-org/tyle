namespace Tyle.Core.Common.Domain;

public class ClassifierReference : RdlReference
{
    public ClassifierReference(string name, string iri, string? description,
        ReferenceSource source = ReferenceSource.UserSubmission) : base(name, iri, description, source)
    {
    }
}