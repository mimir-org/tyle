using System.Security.Principal;
using AspNetCore.Totp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Content;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;

namespace Mimirorg.Authentication.Services
{
    public class MimirorgUserService : IMimirorgUserService
    {
        private readonly UserManager<MimirorgUser> _userManager;
        private readonly MimirorgAuthSettings _authSettings;

        public MimirorgUserService(UserManager<MimirorgUser> userManager, IOptions<MimirorgAuthSettings> authSettings)
        {
            _userManager = userManager;
            _authSettings = authSettings?.Value;
        }

        /// <summary>
        /// Register an user
        /// </summary>
        /// <param name="userAm"></param>
        /// <returns></returns>
        /// <exception cref="MimirorgConfigurationException"></exception>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgInvalidOperationException"></exception>
        /// <exception cref="MimirorgDuplicateException"></exception>
        public async Task<MimirorgQrCodeCm> CreateUser(MimirorgUserAm userAm)
        {
            if(_authSettings == null)
                throw new MimirorgConfigurationException("Missing configuration for auth settings");

            var validation = userAm.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't register: {userAm.Email}", validation);

            var existingUser = await _userManager.FindByEmailAsync(userAm.Email);
            if(existingUser != null)
                throw new MimirorgDuplicateException($"Couldn't register: {userAm.Email}. There is already an user with same username");

            var user = userAm.ToDomainModel();
            var result = await _userManager.CreateAsync(user, userAm.Password);
            if (!result.Succeeded)
                throw new MimirorgInvalidOperationException($"Couldn't register: {userAm.Email}");

            var totpSetupGenerator = new TotpSetupGenerator();
            var totpSetup = totpSetupGenerator.Generate(_authSettings.ApplicationName, user.UserName, user.SecurityStamp, _authSettings.QrWidth, _authSettings.QrHeight);

            return new MimirorgQrCodeCm
            {
                Code = totpSetup.QrCodeImage,
                ManualCode = totpSetup.ManualSetupKey
            };
        }

        /// <summary>
        /// Get user from principal
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<MimirorgUserCm> GetUser(IPrincipal principal)
        {
            if (principal?.Identity?.Name == null)
                throw new MimirorgNotFoundException("Couldn't find current user");

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null)
                throw new MimirorgNotFoundException("Couldn't find current user");

            return user.ToContentModel();
        }

        /// <summary>
        /// Get user from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserCm</returns>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<MimirorgUserCm> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new MimirorgNotFoundException($"Couldn't find user with id {id}");

            return user.ToContentModel();
        }

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
        public async Task<MimirorgUserCm> UpdateUser(string id, MimirorgUserAm userAm)
        {
            if (_authSettings == null)
                throw new MimirorgConfigurationException("Missing configuration for auth settings");

            var validation = userAm.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't update: {userAm.Email}", validation);

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new MimirorgNotFoundException($"Couldn't find user with id {id}");

            // TODO: Map user data, we need to have own password object to validate updates...

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                throw new MimirorgInvalidOperationException($"Could not update user with email {userAm.Email}");

            var updatedUser = await GetUser(id);
            return updatedUser;
        }

        /// <summary>
        /// Delete an user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgInvalidOperationException("Can't delete a user without an id");

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new MimirorgNotFoundException($"Couldn't find user with id {id}");

            return await _userManager.DeleteAsync(user) == IdentityResult.Success;
        }
    }
}
