using System.Security.Claims;
using Tyle.Application.Common;

namespace Tyle.Api.Common;

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