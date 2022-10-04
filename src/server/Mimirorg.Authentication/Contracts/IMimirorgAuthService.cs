using System.Security.Authentication;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Contracts
{
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
        Task<ICollection<MimirorgTokenCm>> Authenticate(MimirorgAuthenticateAm authenticate);

        /// <summary>
        /// Create a token from refresh token
        /// </summary>
        /// <param name="secret">string</param>
        /// <returns>ICollection&lt;MimirorgTokenCm&gt;</returns>
        /// <exception cref="AuthenticationException"></exception>
        Task<ICollection<MimirorgTokenCm>> Authenticate(string secret);

        /// <summary>
        /// Validate security code
        /// </summary>
        /// <param name="user">The user that should be validated</param>
        /// <param name="code">The security code</param>
        /// <remarks>If user is not set two factor to be enabled,
        /// the method will return </remarks>
        /// <returns>Returns true if code is valid</returns>
        bool ValidateSecurityCode(MimirorgUser user, string code);

        /// <summary>
        /// Remove the current user's authentication tokens
        /// </summary>
        /// <param name="secret">string</param>
        /// <returns></returns>
        Task Logout(string secret);

        #endregion

        #region Authorization

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>ICollection&lt;MimirorgRoleCm&gt;</returns>
        Task<ICollection<MimirorgRoleCm>> GetAllRoles();

        /// <summary>
        /// Add an user to a role
        /// </summary>
        /// <param name="userRole">MimirorgUserRoleAm</param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        Task<bool> AddUserToRole(MimirorgUserRoleAm userRole);

        /// <summary>
        /// Remove user from a role
        /// </summary>
        /// <param  name="userRole">MimirorgUserRoleAm</param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        Task<bool> RemoveUserFromRole(MimirorgUserRoleAm userRole);

        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <returns>ICollection&lt;MimirorgPermissionCm&gt;</returns>
        Task<ICollection<MimirorgPermissionCm>> GetAllPermissions();

        /// <summary>
        /// Set user permission for a specific company and user.
        /// </summary>
        /// <paramref name="userPermission"></paramref>
        /// <returns>Return a boolean status value</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        Task<bool> SetPermissions(MimirorgUserPermissionAm userPermission);

        Task<bool> HasAccess(int companyId, State state);

        #endregion
    }
}