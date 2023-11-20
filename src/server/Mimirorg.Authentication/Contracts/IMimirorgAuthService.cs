using System.Security.Authentication;
using System.Security.Claims;
using Mimirorg.Authentication.Enums;
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
    /// Checks if the user has access to the action
    /// </summary>
    /// <param name="user"></param>
    /// <param name="createdNameFromDb"></param>
    /// <param name="stateFromDb"></param>
    /// <returns></returns>
    public bool HasUserPermissionToModify(ClaimsPrincipal? user, string createdNameFromDb, State stateFromDb);

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

    #endregion
}