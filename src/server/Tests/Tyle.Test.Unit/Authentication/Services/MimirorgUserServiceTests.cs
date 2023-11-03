using Microsoft.AspNetCore.Identity;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Authentication.Services;
using Moq;
using Tyle.Test.Setup;
using Tyle.Test.Setup.Fixtures;
using Xunit;

namespace Tyle.Test.Unit.Authentication.Services;

public class MimirorgUserServiceTests : UnitTest<MimirorgCommonFixture>
{

    public MimirorgUserServiceTests(MimirorgCommonFixture fixture) : base(fixture)
    {

    }

    [Fact]
    public async Task RemoveUnconfirmedUsersAndTokens_No_Remove_Confirmed_Users()
    {
        var userList = new List<MimirorgUser>
        {
            new()
            {
                Id = "User_Confirmed_1",
                Email = "UserConfirmed@test.com",
                FirstName = "Confirmed",
                LastName = "User",
                EmailConfirmed = true,
                Purpose="Purpose"
            }
        };

        var userTokens = new List<MimirorgToken>
        {
            new()
            {
                Id = 1,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            },
            new()
            {
                Id = 2,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            }
        };

        var service = SetupUserService(userList, userTokens);
        var result = await service.RemoveUnconfirmedUsersAndTokens();
        Assert.True(result.deletedUsers == 0);
        Assert.True(result.deletedTokens == 1);
    }

    [Fact]
    public async Task RemoveUnconfirmedUsersAndTokens_Remove_Not_Confirmed_User()
    {
        var userList = new List<MimirorgUser>
        {
            new()
            {
                Id = "User_Confirmed_1",
                Email = "UserConfirmed@test.com",
                FirstName = "Confirmed",
                LastName = "User",
                EmailConfirmed = false,
                Purpose="Purpose"
            }
        };

        var userTokens = new List<MimirorgToken>
        {
            new()
            {
                Id = 2,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            }
        };

        var service = SetupUserService(userList, userTokens);
        var result = await service.RemoveUnconfirmedUsersAndTokens();
        Assert.True(result.deletedUsers == 1);
        Assert.True(result.deletedTokens == 1);
    }

    [Fact]
    public async Task RemoveUnconfirmedUsersAndTokens_Not_Remove_Not_Confirmed_User_When_Token_Valid_To_Ok()
    {
        var userList = new List<MimirorgUser>
        {
            new()
            {
                Id = "User_Confirmed_1",
                Email = "UserConfirmed@test.com",
                FirstName = "Confirmed",
                LastName = "User",
                EmailConfirmed = false,
                Purpose="Purpose"
            }
        };

        var userTokens = new List<MimirorgToken>
        {
            new()
            {
                Id = 1,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            },
            new()
            {
                Id = 2,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            }
        };

        var service = SetupUserService(userList, userTokens);
        var result = await service.RemoveUnconfirmedUsersAndTokens();
        Assert.True(result.deletedUsers == 0);
        Assert.True(result.deletedTokens == 1);
    }

    [Fact]
    public async Task RemoveUnconfirmedUsersAndTokens_Remove_All_Not_Valid_Tokens()
    {
        var userList = new List<MimirorgUser>
        {
            new()
            {
                Id = "User_Confirmed_1",
                Email = "UserConfirmed@test.com",
                FirstName = "Confirmed",
                LastName = "User",
                EmailConfirmed = true,
                Purpose="Purpose"
            }
        };

        var userTokens = new List<MimirorgToken>
        {
            new()
            {
                Id = 1,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            },
            new()
            {
                Id = 2,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            }
        };

        var service = SetupUserService(userList, userTokens);
        var result = await service.RemoveUnconfirmedUsersAndTokens();
        Assert.True(result.deletedUsers == 0);
        Assert.True(result.deletedTokens == 2);
    }

    [Fact]
    public async Task RemoveUnconfirmedUsersAndTokens_Remove_All_Not_Valid_Tokens_And_User()
    {
        var userList = new List<MimirorgUser>
        {
            new()
            {
                Id = "User_Confirmed_1",
                Email = "UserConfirmed@test.com",
                FirstName = "Confirmed",
                LastName = "User",
                EmailConfirmed = false,
                Purpose="Purpose"
            },
            new()
            {
                Id = "User_Confirmed_2",
                Email = "UserConfirmed@test.com",
                FirstName = "Confirmed",
                LastName = "User",
                EmailConfirmed = false,
                Purpose="Purpose"
            }
        };

        var userTokens = new List<MimirorgToken>
        {
            new()
            {
                Id = 1,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            },
            new()
            {
                Id = 2,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            },
            new()
            {
                Id = 3,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.ChangePassword,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            },
            new()
            {
                Id = 4,
                ClientId = "User_Confirmed_2",
                TokenType = MimirorgTokenType.AccessToken,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            },
            new()
            {
                Id = 5,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.ChangeTwoFactor,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            },
            new()
            {
                Id = 6,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.RefreshToken,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            }
        };

        var service = SetupUserService(userList, userTokens);
        var result = await service.RemoveUnconfirmedUsersAndTokens();
        Assert.True(result.deletedUsers == 2);
        Assert.True(result.deletedTokens == 6);
    }

    [Fact]
    public async Task RemoveUnconfirmedUsersAndTokens_Delete_All_Tokens_For_Deleted_User()
    {
        var userList = new List<MimirorgUser>
        {
            new()
            {
                Id = "User_Confirmed_1",
                Email = "UserConfirmed@test.com",
                FirstName = "Confirmed",
                LastName = "User",
                EmailConfirmed = false,
                Purpose="Purpose"
            }
        };

        var userTokens = new List<MimirorgToken>
        {
            new()
            {
                Id = 1,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.UtcNow.AddDays(-1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            },
            new()
            {
                Id = 2,
                ClientId = "User_Confirmed_1",
                TokenType = MimirorgTokenType.ChangeTwoFactor,
                ValidTo = DateTime.UtcNow.AddDays(1),
                Email = "UserConfirmed@test.com",
                Secret = "232323"
            }
        };

        var service = SetupUserService(userList, userTokens);
        var result = await service.RemoveUnconfirmedUsersAndTokens();
        Assert.True(result.deletedUsers == 1);
        Assert.True(result.deletedTokens == 2);
    }

    private IMimirorgUserService SetupUserService(IEnumerable<MimirorgUser> users, IEnumerable<MimirorgToken> tokens)
    {
        var store = new Mock<IUserStore<MimirorgUser>>();
        var userManagerMock = new Mock<UserManager<MimirorgUser>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);
        Mock<IMimirorgTokenRepository> tokenRepositoryMock = new();

        userManagerMock.Setup(x => x.Users).Returns(users.AsQueryable());
        tokenRepositoryMock.Setup(x => x.GetAll(true)).Returns(tokens.AsQueryable);

        return new MimirorgUserService(userManagerMock.Object, null, tokenRepositoryMock.Object, null, null, null);
    }
}