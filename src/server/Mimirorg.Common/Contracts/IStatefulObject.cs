using Mimirorg.Common.Enums;

namespace Mimirorg.Common.Contracts;

public interface IStatefulObject
{
    State State { get; set; }
}