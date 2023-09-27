using System.Security.Claims;
using TypeLibrary.Services.Common;

namespace TypeLibrary.Api.Common;

public class UserInformationService : IUserInformationService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserInformationService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetUserId()
    {
        return _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
