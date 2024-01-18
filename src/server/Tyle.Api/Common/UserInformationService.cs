using System.Security.Claims;
using Mimirorg.Authentication.Contracts;
using Tyle.Application.Common;

namespace Tyle.Api.Common;

public class UserInformationService : IUserInformationService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMimirorgUserService _userService;

    public UserInformationService(IHttpContextAccessor contextAccessor, IMimirorgUserService userService)
    {
        _contextAccessor = contextAccessor;
        _userService = userService;
    }

    public async Task<string?> GetEmail(string userId)
    {
        var user = await _userService.GetUser(userId);

        if (user == null)
        {
            return null;
        }

        return user.Email;
    }

    public async Task<string?> GetFullName(string userId)
    {
        var user = await _userService.GetUser(userId);

        if (user == null)
        {
            return null;
        }

        return $"{user.FirstName} {user.LastName}";
    }

    public string GetUserId()
    {
        return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}