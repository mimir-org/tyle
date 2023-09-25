using Tyle.Core.Common;

namespace Tyle.Application.Common;

public interface IUserService
{
    /// <summary>
    /// Get the current user of the application.
    /// </summary>
    /// <returns>A user struct with information on the current user.</returns>
    public Task<User> GetCurrentUser();
}
