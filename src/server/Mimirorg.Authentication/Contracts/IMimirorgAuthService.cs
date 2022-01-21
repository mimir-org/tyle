using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Content;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgAuthService
    {
        #region Authentication

        Task<ICollection<MimirorgTokenCm>> Authenticate(MimirorgAuthenticateAm authenticate);
        Task<ICollection<MimirorgTokenCm>> Authenticate(string secret);
        Task LockUser(string userId);

        #endregion

        #region Authorization

        Task<ICollection<MimirorgRoleCm>> GetAllRoles();
        Task<ICollection<MimirorgPermissionCm>> GetAllPermissions();
        Task<bool> AddUserToRole(MimirorgUserRoleAm userRole);
        Task<bool> RemoveUserFromRole(MimirorgUserRoleAm userRole);
        Task<bool> SetPermissions(MimirorgUserPermissionAm userPermission);

        #endregion
    }
}
