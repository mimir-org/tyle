using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System.Security.Principal;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgUserService
{
    /// <summary>
    /// Register an user
    /// </summary>
    /// <param name="userAm"></param>
    /// <returns></returns>
    /// <exception cref="MimirorgConfigurationException"></exception>
    /// <exception cref="MimirorgBadRequestException"></exception>
    /// <exception cref="MimirorgInvalidOperationException"></exception>
    /// <exception cref="MimirorgDuplicateException"></exception>
    Task<MimirorgUserCm> CreateUser(MimirorgUserAm userAm);

    /// <summary>
    /// Get user from principal
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task<MimirorgUserCm> GetUser(IPrincipal principal);

    /// <summary>
    /// Get user from id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>MimirorgUserCm</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task<MimirorgUserCm> GetUser(string id);

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>IEnumerable(MimirorgUserCm)</returns>
    Task<List<MimirorgUserCm>> GetUsers();

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="userAm">New user information</param>
    /// <returns>UserCm</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgInvalidOperationException"></exception>
    Task<MimirorgUserCm> UpdateUser(MimirorgUserAm userAm);

    /// <summary>
    /// Gets all companies that the principal can access given a specific permission level
    /// </summary>
    /// <param name="principal"></param>
    /// <param name="permission"></param>
    /// <returns>A collection of company ids</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task<ICollection<int>> GetCompaniesForUser(IPrincipal principal, MimirorgPermission permission);

    /// <summary>
    /// Setup two factor 
    /// </summary>
    /// <param name="verifyEmail"></param>
    /// <returns>Returns QR code for two factor app</returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="MimirorgConfigurationException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgInvalidOperationException"></exception>
    Task<MimirorgQrCodeCm> GenerateTwoFactor(MimirorgVerifyAm verifyEmail);

    /// <summary>
    /// A method that generates a login code and sending the generated code to user as mail.
    /// </summary>
    /// <param name="email">The email address for the user secret token</param>
    /// <returns>A completed task</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgInvalidOperationException">Throws if user does not exist</exception>
    Task GenerateChangePasswordSecret(string email);

    /// <summary>
    /// Change the password on user
    /// </summary>
    /// <param name="changePassword">Object information for resetting password</param>
    /// <returns>A completed task</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException">Throws if user or token not exist</exception>
    Task<bool> ChangePassword(MimirorgChangePasswordAm changePassword);

    /// <summary>
    /// Cleanup tokens and not confirmed users
    /// </summary>
    /// <remarks>All users that has not any valid verify token and is not confirmed will be deleted,
    /// with all the user tokens. Also invalid tokens will be deleted.</remarks>
    /// <returns>The number of deleted users and tokens</returns>
    Task<(int deletedUsers, int deletedTokens)> RemoveUnconfirmedUsersAndTokens();

    /// <summary>
    /// Verify email account from verify code
    /// </summary>
    /// <param name="verifyEmail">The email verify data</param>
    /// <returns>bool</returns>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgInvalidOperationException"></exception>
    /// <exception cref="Tyle.Core.Common.Exceptions.MimirorgNotFoundException"></exception>
    Task<bool> VerifyAccount(MimirorgVerifyAm verifyEmail);
}