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
        Task<MimirorgQrCodeCm> CreateUser(MimirorgUserAm userAm);

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
        /// Get companies that is registered for current logged in user
        /// </summary>
        /// <returns>A collection of registered companies</returns>
        Task<ICollection<MimirorgCompanyCm>> GetUserFilteredCompanies();

        /// <summary>
        /// Update an user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userAm"></param>
        /// <returns></returns>
        /// <exception cref="MimirorgConfigurationException"></exception>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        /// <exception cref="MimirorgInvalidOperationException"></exception>
        Task<MimirorgUserCm> UpdateUser(string id, MimirorgUserAm userAm);

        /// <summary>
        /// Delete an user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(string id);
    }
}