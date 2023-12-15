using System.Security.Authentication;
using System.Security.Claims;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Client;
using Tyle.Core.Common;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgAuthService
{
    #region Authentication

    /// <summary>
    /// Authenticate an user from username, password and app code
    /// </summary>
    /// <param name="authenticate">MimirorgAuthenticateAm</param>
    /// <returns>ICollection&lt;MimirorgTokenCm&gt;</returns>
    /// <exception cref="MimirorgBadRequestException"></exception>
    /// <exception cref="AuthenticationException"></exception>
    Task<ICollection<TokenView>> Authenticate(AuthenticateRequest authenticate);

    /// <summary>
    /// Create a token from refresh token
    /// </summary>
    /// <param name="secret">string</param>
    /// <returns>ICollection&lt;MimirorgTokenCm&gt;</returns>
    /// <exception cref="AuthenticationException"></exception>
    Task<ICollection<TokenView>> Authenticate(string secret);

    /// <summary>
    /// Remove the current user's authentication tokens
    /// </summary>
    /// <param name="secret">string</param>
    /// <returns></returns>
    Task Logout(string secret);

    /// <summary>
    /// Checking if user has permission to delete the type
    /// </summary>
    /// <param name="user"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public bool CanDelete(ClaimsPrincipal? user, ImfType type);
    /// <summary>
    /// Checking if user has permission to update the state
    /// </summary>
    /// <param name="user"></param>
    /// <param name="newState"></param>
    /// <returns></returns>
    public bool CanChangeState(ClaimsPrincipal? user, State newState);
    #endregion

    #region Authorization

    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>ICollection&lt;MimirorgRoleCm&gt;</returns>
    Task<ICollection<RoleView>> GetAllRoles();

    /// <summary>
    /// Add an user to a role
    /// </summary>
    /// <param name="userRole">MimirorgUserRoleAm</param>
    /// <returns>bool</returns>
    /// <exception cref="MimirorgBadRequestException"></exception>
    /// <exception cref="MimirorgNotFoundException"></exception>
    Task<bool> AddUserToRole(UserRoleRequest userRole);

    /// <summary>
    /// Remove user from a role
    /// </summary>
    /// <param  name="userRole">MimirorgUserRoleAm</param>
    /// <returns>bool</returns>
    /// <exception cref="MimirorgBadRequestException"></exception>
    /// <exception cref="MimirorgNotFoundException"></exception>
    Task<bool> RemoveUserFromRole(UserRoleRequest userRole);

    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>ICollection&lt;MimirorgRoleCm&gt;</returns>
    Task<bool> DeleteUserRoles(UserRoleRequest userRole);

    #endregion
}