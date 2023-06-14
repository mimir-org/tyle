namespace Mimirorg.Common.Contracts;

public interface IVersionObject : IStatefulObject
{
    string FirstVersionId { get; set; }
    string Version { get; set; }
}