namespace Mimirorg.Common.Contracts;

public interface IVersionObject : IStatefulObject
{
    int FirstVersionId { get; set; }
    string Version { get; set; }
}