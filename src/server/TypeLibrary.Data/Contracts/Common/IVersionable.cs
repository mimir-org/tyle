using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;

namespace TypeLibrary.Data.Contracts.Common
{
    // ReSharper disable once IdentifierTypo
    public interface IVersionable<in T>
    {
        string Version { get; set; }
        Validation HasIllegalChanges(T other);
        VersionStatus CalculateVersionStatus(T other);
    }
}
