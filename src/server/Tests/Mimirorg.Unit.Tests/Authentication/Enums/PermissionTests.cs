using Mimirorg.Authentication.Enums;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Authentication.Enums;

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
    public void HasFlag_OnPermissionLevel_GivesOnlyCurrentLevelAndBelow(MimirorgPermission userPermission, MimirorgPermission permission, bool result)
    {
        Assert.Equal(result, userPermission.HasFlag(permission));
    }
}