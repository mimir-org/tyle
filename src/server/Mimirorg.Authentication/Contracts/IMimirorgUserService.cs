using System.Security.Principal;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Client;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgUserService
{
    /// <summary>
    /// Register an user
    /// </summary>
    /// <param name="userRequest"></param>
    /// <returns></returns>
    /// <exception cref="MimirorgConfigurationException"></exception>
    /// <exception cref="MimirorgBadRequestException"></exception>
    /// <exception cref="MimirorgInvalidOperationException"></exception>
    /// <exception cref="MimirorgDuplicateException"></exception>
    Task<UserView> CreateUser(UserRequest userRequest);

    /// <summary>
    /// Get user from principal
    /// </summary>
    /// <param name="principal"></param>
    /// <returns></returns>
    /// <exception cref="MimirorgNotFoundException"></exception>
    Task<UserView> GetUser(IPrincipal principal);

    /// <summary>
    /// Get user from id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>MimirorgUserCm</returns>
    /// <exception cref="MimirorgNotFoundException"></exception>
    Task<UserView> GetUser(string id);

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>IEnumerable(MimirorgUserCm)</returns>
    Task<List<UserView>> GetUsers();

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="userRequest">New user information</param>
    /// <returns>UserCm</returns>
    /// <exception cref="MimirorgNotFoundException"></exception>
    /// <exception cref="MimirorgInvalidOperationException"></exception>
    Task<UserView> UpdateUser(UserRequest userRequest);

    /// <summary>
    /// Setup two factor 
    /// </summary>
    /// <param name="verifyEmail"></param>
    /// <returns>Returns QR code for two factor app</returns>
    /// <exception cref="NotImplementedException"></exception>
    /// <exception cref="MimirorgConfigurationException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="MimirorgNotFoundException"></exception>
    /// <exception cref="MimirorgInvalidOperationException"></exception>
    Task<QrCodeView> GenerateTwoFactor(VerifyRequest verifyEmail);

    /// <summary>
    /// A method that generates a login code and sending the generated code to user as mail.
    /// </summary>
    /// <param name="email">The email address for the user secret token</param>
    /// <returns>A completed task</returns>
    /// <exception cref="MimirorgInvalidOperationException">Throws if user does not exist</exception>
    Task GenerateChangePasswordSecret(string email);

    /// <summary>
    /// Change the password on user
    /// </summary>
    /// <param name="changePassword">Object information for resetting password</param>
    /// <returns>A completed task</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if user or token not exist</exception>
    Task<bool> ChangePassword(ChangePasswordRequest changePassword);

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
    /// <exception cref="MimirorgInvalidOperationException"></exception>
    /// <exception cref="MimirorgNotFoundException"></exception>
    Task<bool> VerifyAccount(VerifyRequest verifyEmail);
}