using System.Security.Principal;
using AspNetCore.Totp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.ApplicationModels;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;

namespace Mimirorg.Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<MimirorgUser> _userManager;
        private readonly AuthSettings _authSettings;

        public UserService(UserManager<MimirorgUser> userManager, IOptions<AuthSettings> authSettings)
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
        public async Task<QrCode> CreateUser(UserAm userAm)
        {
            if(_authSettings == null)
                throw new MimirorgConfigurationException("Missing configuration for auth settings");

            var validation = userAm.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't register: {userAm.Email}", validation);

            var existingUser = await _userManager.FindByEmailAsync(userAm.Email);
            if(existingUser != null)
                throw new MimirorgInvalidOperationException($"Couldn't register: {userAm.Email}");

            var user = userAm.ToMimirorgUser();
            var result = await _userManager.CreateAsync(user, userAm.Password);
            if (!result.Succeeded)
                throw new MimirorgInvalidOperationException($"Couldn't register: {userAm.Email}");

            var totpSetupGenerator = new TotpSetupGenerator();
            var totpSetup = totpSetupGenerator.Generate(_authSettings.ApplicationName, user.UserName, user.SecurityStamp, _authSettings.QrWidth, _authSettings.QrHeight);

            return new QrCode
            {
                Code = totpSetup.QrCodeImage,
                ManualCode = totpSetup.ManualSetupKey
            };
        }

        public async Task<UserCm> GetUser(IPrincipal principal)
        {
            if (principal?.Identity?.Name == null)
                throw new MimirorgNotFoundException("Couldn't find current user");

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null)
                throw new MimirorgNotFoundException("Couldn't find current user");

            return new UserCm
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber
            };
        }
    }
}
