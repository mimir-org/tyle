using System.Security.Authentication;
using AspNetCore.Totp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Authentication.ApplicationModels;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;

namespace Mimirorg.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<MimirorgUser> _userManager;
        private readonly SignInManager<MimirorgUser> _signInManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthService(UserManager<MimirorgUser> userManager, SignInManager<MimirorgUser> signInManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
        }

        /// <summary>
        /// Authenticate an user from username, password and app code
        /// </summary>
        /// <param name="authenticate"></param>
        /// <returns></returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="AuthenticationException"></exception>
        public async Task<ICollection<TokenAm>> Authenticate(AuthenticateAm authenticate)
        {
            var validation = authenticate.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't authenticate: {authenticate?.Email}", validation);

            var user = await _userManager.FindByEmailAsync(authenticate.Email);
            if (user == null)
                throw new AuthenticationException($"Couldn't find user with email {authenticate.Email}");

            if (user.IsLockedOut)
                throw new AuthenticationException($"The user account with email {authenticate.Email} is locked out.");

            if (user.ShouldBeLockedOut)
            {
                await LockUser(user);
                throw new AuthenticationException($"The user account with email {authenticate.Email} is locked out.");
            }

            var userStatus = await _signInManager.CheckPasswordSignInAsync(user, authenticate.Password, false);

            if (!userStatus.Succeeded)
                throw new AuthenticationException($"The user account with email {authenticate.Email} could not be signed in.");

            var validator = new TotpValidator(new TotpGenerator());
            var hasCorrectPin = validator.Validate(user.SecurityStamp, authenticate.Code);

            if (!hasCorrectPin)
                throw new AuthenticationException($"The user account with email {authenticate.Email} could not validate code.");

            var now = DateTime.Now.ToUniversalTime();
            var accessTokenTask = _tokenRepository.CreateAccessToken(user, now);
            var refreshTokenTask = _tokenRepository.CreateRefreshToken(user, now);

            var accessToken = await accessTokenTask;
            var refreshToken = await refreshTokenTask;

            return new List<TokenAm> { accessToken, refreshToken };
        }

        /// <summary>
        /// Create a token from refresh token
        /// </summary>
        /// <param name="secret"></param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException"></exception>
        public async Task<ICollection<TokenAm>> Authenticate(string secret)
        {
            var token = await _tokenRepository.FindBy(x => x.Secret == secret).FirstOrDefaultAsync();
            
            if(token == null)
                throw new AuthenticationException("Can't find any valid refresh token.");
            
            if (token.ValidTo < DateTime.Now.ToUniversalTime())
            {
                await _tokenRepository.Delete(token.Id);
                await _tokenRepository.SaveAsync();
                throw new AuthenticationException("Can't find any valid refresh token.");
            }

            var user = await _userManager.FindByIdAsync(token.ClientId);
            if (user == null)
            {
                await _tokenRepository.Delete(token.Id);
                await _tokenRepository.SaveAsync();
                throw new AuthenticationException("Can't find any connected user for token.");
            }

            var now = DateTime.Now.ToUniversalTime();
            var accessTokenTask = _tokenRepository.CreateAccessToken(user, now);
            var refreshTokenTask = _tokenRepository.CreateRefreshToken(user, now);

            var accessToken = await accessTokenTask;
            var refreshToken = await refreshTokenTask;

            return new List<TokenAm> { accessToken, refreshToken };
        }

        public Task LockUser(MimirorgUser user)
        {
            throw new NotImplementedException();
        }
    }
}
