using Mimirorg.Common.Enums;

namespace Mimirorg.Common.Contracts;

public interface IVersionObject
{
    State State { get; set; }
    int FirstVersionId { get; set; }
    string Version { get; set; }
}