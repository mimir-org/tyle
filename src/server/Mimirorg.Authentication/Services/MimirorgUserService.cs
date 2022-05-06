using System.Security.Principal;
using AspNetCore.Totp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Constants;
using Mimirorg.Authentication.Models.Content;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Authentication.Models.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;

namespace Mimirorg.Authentication.Services
{
    public class MimirorgUserService : IMimirorgUserService
    {
        private readonly UserManager<MimirorgUser> _userManager;
        private readonly MimirorgAuthSettings _authSettings;
        private readonly IMimirorgTokenRepository _tokenRepository;
        private readonly IMimirorgEmailRepository _emailRepository;
        private readonly IMimirorgTemplateRepository _templateRepository;

        public MimirorgUserService(UserManager<MimirorgUser> userManager, IOptions<MimirorgAuthSettings> authSettings, IMimirorgTokenRepository tokenRepository, IMimirorgEmailRepository emailRepository, IMimirorgTemplateRepository templateRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _emailRepository = emailRepository;
            _templateRepository = templateRepository;
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
            if (_authSettings == null)
                throw new MimirorgConfigurationException("Missing configuration for auth settings");

            var validation = userAm.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't register: {userAm.Email}", validation);

            var existingUser = await _userManager.FindByEmailAsync(userAm.Email);
            if (existingUser != null)
                throw new MimirorgDuplicateException($"Couldn't register: {userAm.Email}. There is already an user with same username");

            var user = userAm.ToDomainModel();
            var securityKey = $"{Guid.NewGuid()}{MimirorgSecurity.SecurityStamp}{Guid.NewGuid()}";
            user.SecurityHash = securityKey.CreateSha512();

            var result = await _userManager.CreateAsync(user, userAm.Password);
            if (!result.Succeeded)
                throw new MimirorgInvalidOperationException($"Couldn't register: {userAm.Email}.");

            // Create an email verification token and send email to user
            if (_authSettings.RequireConfirmedAccount)
                await SendEmailConfirmation(user);

            var totpSetupGenerator = new TotpSetupGenerator();
            var totpSetup = totpSetupGenerator.Generate(_authSettings.ApplicationName, user.Id, user.SecurityHash, _authSettings.QrWidth, _authSettings.QrHeight);

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

        #region Private methods

        private async Task SendEmailConfirmation(MimirorgUser user)
        {
            var secret = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var token = new MimirorgToken
            {
                ClientId = user.Id,
                Email = user.Email,
                Secret = secret,
                TokenType = MimirorgTokenType.VerifyEmail,
                ValidTo = DateTime.Now.AddHours(1)
            };

            var oldTokens = _tokenRepository.GetAll().Where(x => (x.ClientId == user.Id && x.TokenType == MimirorgTokenType.VerifyEmail) || DateTime.Now > x.ValidTo).ToList();

            foreach (var t in oldTokens)
            {
                _tokenRepository.Attach(t, EntityState.Deleted);
            }

            await _tokenRepository.CreateAsync(token);
            await _tokenRepository.SaveAsync();

            var email = await _templateRepository.CreateEmailConfirmationTemplate(user, token);
            await _emailRepository.SendEmail(email);
        }

        #endregion
    }
}
