using Mimirorg.Test.Setup;
using Mimirorg.Test.Setup.Fixtures;
using Mimirorg.TypeLibrary.Enums;
using Xunit;

namespace Mimirorg.Test.Unit.Models;

public class PermissionTests : UnitTest<MimirorgCommonFixture>
{
    public PermissionTests(MimirorgCommonFixture fixture) : base(fixture)
    {
    }

    [Theory]
    [InlineData(MimirorgPermission.Manage, MimirorgPermission.Manage, true)]
    [InlineData(MimirorgPermission.Manage, MimirorgPermission.Approve, true)]
    [InlineData(MimirorgPermission.Manage, MimirorgPermission.Write, true)]
    [InlineData(MimirorgPermission.Manage, MimirorgPermission.Read, true)]
    [InlineData(MimirorgPermission.Approve, MimirorgPermission.Manage, false)]
    [InlineData(MimirorgPermission.Approve, MimirorgPermission.Approve, true)]
    [InlineData(MimirorgPermission.Approve, MimirorgPermission.Write, true)]
    [InlineData(MimirorgPermission.Approve, MimirorgPermission.Read, true)]
    [InlineData(MimirorgPermission.Write, MimirorgPermission.Manage, false)]
    [InlineData(MimirorgPermission.Write, MimirorgPermission.Approve, false)]
    [InlineData(MimirorgPermission.Write, MimirorgPermission.Write, true)]
    [InlineData(MimirorgPermission.Write, MimirorgPermission.Read, true)]
    public void HasFlagReturnsCorrect(MimirorgPermission userPermission, MimirorgPermission permission, bool result)
    {
        Assert.Equal(result, userPermission.HasFlag(permission));
    }
}