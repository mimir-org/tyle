using Microsoft.AspNetCore.Identity;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Constants;

namespace Mimirorg.Authentication.Factories;

public class MimirorgAuthFactory : IMimirorgAuthFactory
{
    public ICollection<IdentityRole> DefaultRoles { get; }

    public MimirorgAuthFactory()
    {
        DefaultRoles = CreateDefaultRoles().ToList();
    }

    #region Private methods

    private static IEnumerable<IdentityRole> CreateDefaultRoles()
    {
        yield return new IdentityRole
        {
            Id = "76392a20-acbe-491d-bb52-a67cf58dd108",
            Name = MimirorgDefaultRoles.Administrator,
            NormalizedName = MimirorgDefaultRoles.Administrator.ToUpper()
        };
        yield return new IdentityRole
        {
            Id = "ceaf8cc1-5e35-4493-8341-1838b46e6f6b",
            Name = MimirorgDefaultRoles.Reviewer,
            NormalizedName = MimirorgDefaultRoles.Reviewer.ToUpper()
        };
        yield return new IdentityRole
        {
            Id = "58e158cf-08b6-477c-82a8-a4d44a854be1",
            Name = MimirorgDefaultRoles.Contributor,
            NormalizedName = MimirorgDefaultRoles.Contributor.ToUpper()
        };
        yield return new IdentityRole
        {
            Id = "c62d0f40-27c3-455b-8b12-56b272a87386",
            Name = MimirorgDefaultRoles.Reader,
            NormalizedName = MimirorgDefaultRoles.Reader.ToUpper()
        };
    }

    #endregion
}