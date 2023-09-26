using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System.Security.Authentication;

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
    Task<ICollection<MimirorgTokenCm>> Authenticate(MimirorgAuthenticateAm authenticate);

    /// <summary>
    /// Create a token from refresh token
    /// </summary>
    /// <param name="secret">string</param>
    /// <returns>ICollection&lt;MimirorgTokenCm&gt;</returns>
    /// <exception cref="AuthenticationException"></exception>
    Task<ICollection<MimirorgTokenCm>> Authenticate(string secret);

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
    /// <exception cref="Tyle.Core.Exceptions.MimirorgBadRequestException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task<bool> AddUserToRole(MimirorgUserRoleAm userRole);

    /// <summary>
    /// Remove user from a role
    /// </summary>
    /// <param  name="userRole">MimirorgUserRoleAm</param>
    /// <returns>bool</returns>
    /// <exception cref="Tyle.Core.Exceptions.MimirorgBadRequestException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task<bool> RemoveUserFromRole(MimirorgUserRoleAm userRole);

    /// <summary>
    /// Get all permissions
    /// </summary>
    /// <returns>ICollection&lt;MimirorgPermissionCm&gt;</returns>
    Task<ICollection<MimirorgPermissionCm>> GetAllPermissions();

    /// <summary>
    /// Set user permission for a specific company.
    /// </summary>
    /// <param name="userPermission">MimirorgUserPermissionAm</param>
    /// <returns>Completed task</returns>
    /// <exception cref="Tyle.Core.Exceptions.MimirorgBadRequestException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task SetPermission(MimirorgUserPermissionAm userPermission);

    /// <summary>
    /// Remove user permission for a specific company.
    /// </summary>
    /// <param name="userPermission">MimirorgUserPermissionAm</param>
    /// <returns>Completed task</returns>
    /// <exception cref="Tyle.Core.Exceptions.MimirorgBadRequestException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task RemovePermission(MimirorgUserPermissionAm userPermission);

    /// <summary>
    /// Check if user has permission to change the state for a given company
    /// </summary>
    /// <param name="companyId">The id of the company</param>
    /// <param name="newState">The state to check for permission</param>
    /// <returns>True if has access, otherwise it returns false</returns>
    /// <exception cref="ArgumentOutOfRangeException">If not a valid state</exception>
    Task<bool> HasAccess(int companyId, State newState);

    /// <summary>
    /// Check if user has permission to delete an item
    /// </summary>
    /// <param name="state">The state of the item</param>
    /// <param name="createdById">The id of the person who created the item</param>
    /// <param name="companyId">The id of the item company</param>
    /// <returns>True if the user can delete the item</returns>
    Task<bool> CanDelete(State state, string createdById, int companyId);

    #endregion
}