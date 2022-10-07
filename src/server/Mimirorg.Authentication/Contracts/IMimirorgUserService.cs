using System.Security.Principal;
using Mimirorg.Common.Exceptions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Contracts
{
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
        /// <exception cref="MimirorgNotFoundException"></exception>
        Task<MimirorgUserCm> GetUser(IPrincipal principal);

        /// <summary>
        /// Get user from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserCm</returns>
        /// <exception cref="MimirorgNotFoundException"></exception>
        Task<MimirorgUserCm> GetUser(string id);

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
        Task<MimirorgQrCodeCm> GenerateTwoFactor(MimirorgVerifyAm verifyEmail);

        /// <summary>
        /// Get companies that is registered for current logged in user
        /// </summary>
        /// <returns>A collection of registered companies</returns>
        Task<ICollection<MimirorgCompanyCm>> GetUserFilteredCompanies();

        /// <summary>
        /// A method that generates a login code and sending the generated code to user as mail.
        /// </summary>
        /// <param name="generateSecret">Object information for generating secret</param>
        /// <returns>A completed task</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if model is not valid</exception>
        /// <exception cref="MimirorgInvalidOperationException">Throws if user does not exist</exception>
        Task GenerateSecret(MimirorgGenerateSecretAm generateSecret);

        /// <summary>
        /// Change the password on user
        /// </summary>
        /// <param name="changePassword">Object information for resetting password</param>
        /// <returns>A completed task</returns>
        /// <exception cref="MimirorgNotFoundException">Throws if user or token not exist</exception>
        Task<bool> ChangePassword(MimirorgChangePasswordAm changePassword);


        Task RemoveUnconfirmedUsersAndTokens();

        /// <summary>
        /// Verify email account from verify code
        /// </summary>
        /// <param name="verifyEmail">The email verify data</param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgInvalidOperationException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        Task<bool> VerifyAccount(MimirorgVerifyAm verifyEmail);
    }
}