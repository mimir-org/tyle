using System.Security.Authentication;
using System.Security.Claims;
using AspNetCore.Totp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Services
{
    public class MimirorgAuthService : IMimirorgAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<MimirorgUser> _userManager;
        private readonly SignInManager<MimirorgUser> _signInManager;
        private readonly IMimirorgTokenRepository _tokenRepository;
        private readonly IMimirorgCompanyService _mimirorgCompanyService;

        public MimirorgAuthService(RoleManager<IdentityRole> roleManager, UserManager<MimirorgUser> userManager, SignInManager<MimirorgUser> signInManager, IMimirorgTokenRepository tokenRepository, IMimirorgCompanyService mimirorgCompanyService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
            _mimirorgCompanyService = mimirorgCompanyService;
            _roleManager = roleManager;
        }

        #region Authentication

        /// <summary>
        /// Authenticate an user from username, password and app code
        /// </summary>
        /// <param name="authenticate">MimirorgAuthenticateAm</param>
        /// <returns>ICollection&lt;MimirorgTokenCm&gt;</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="AuthenticationException"></exception>
        public async Task<ICollection<MimirorgTokenCm>> Authenticate(MimirorgAuthenticateAm authenticate)
        {
            var validation = authenticate.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't authenticate: {authenticate?.Email}", validation);

            var user = await _userManager.FindByEmailAsync(authenticate.Email);
            if (user == null)
                throw new AuthenticationException($"Couldn't find user with email {authenticate.Email}");

            var userStatus = await _signInManager.CheckPasswordSignInAsync(user, authenticate.Password, true);

            if (!userStatus.Succeeded)
                throw new AuthenticationException($"The user account with email {authenticate.Email} could not be signed in.");

            var validator = new TotpValidator(new TotpGenerator());
            var hasCorrectPin = validator.Validate(user.SecurityHash, authenticate.Code);

            if (!hasCorrectPin)
                throw new AuthenticationException($"The user account with email {authenticate.Email} could not validate code.");

            var now = DateTime.Now;
            var accessToken = await _tokenRepository.CreateAccessToken(user, now);
            var refreshToken = await _tokenRepository.CreateRefreshToken(user, now);
            return new List<MimirorgTokenCm> { accessToken, refreshToken };
        }

        /// <summary>
        /// Create a token from refresh token
        /// </summary>
        /// <param name="secret">string</param>
        /// <returns>ICollection&lt;MimirorgTokenCm&gt;</returns>
        /// <exception cref="AuthenticationException"></exception>
        public async Task<ICollection<MimirorgTokenCm>> Authenticate(string secret)
        {
            var token = await _tokenRepository.FindBy(x => x.Secret == secret).FirstOrDefaultAsync();

            if (token == null)
            {
                throw new AuthenticationException("Can't find any valid refresh token.");
            }

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
            var accessToken = await _tokenRepository.CreateAccessToken(user, now);
            var refreshToken = await _tokenRepository.CreateRefreshToken(user, now);

            return new List<MimirorgTokenCm> { accessToken, refreshToken };
        }

        /// <summary>
        /// Verify account from verify token
        /// </summary>
        /// <param name="token">string</param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgInvalidOperationException"></exception>
        public async Task<bool> VerifyAccount(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return false;

            var t = Uri.UnescapeDataString(token);

            var regToken = await _tokenRepository.FindBy(x => x.Secret == t).FirstOrDefaultAsync();
            if (regToken == null)
                return false;

            var user = await _userManager.FindByEmailAsync(regToken.Email);
            if (user == null)
                return false;

            if (regToken.TokenType != MimirorgTokenType.VerifyEmail)
                throw new MimirorgInvalidOperationException("Only email verify is supported");

            var result = await _userManager.ConfirmEmailAsync(user, t);

            if (regToken.TokenType == MimirorgTokenType.VerifyPhone)
                result = await _userManager.ConfirmEmailAsync(user, t);

            if (!result.Succeeded)
                return false;

            _tokenRepository.Attach(regToken, EntityState.Deleted);
            await _tokenRepository.SaveAsync();
            return result.Succeeded;
        }

        #endregion

        #region Authorization

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>ICollection&lt;MimirorgRoleCm&gt;</returns>
        public async Task<ICollection<MimirorgRoleCm>> GetAllRoles()
        {
            var roles = _roleManager.Roles.Select(x => new MimirorgRoleCm { Id = x.Id, Name = x.Name });
            return await Task.FromResult(roles.ToList());
        }

        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <returns>ICollection&lt;MimirorgPermissionCm&gt;</returns>
        public async Task<ICollection<MimirorgPermissionCm>> GetAllPermissions()
        {
            var permissions = ((MimirorgPermission[]) Enum.GetValues(typeof(MimirorgPermission))).Select(c => new MimirorgPermissionCm { Id = (int) c, Name = c.GetDisplayName() }).ToList();
            return await Task.FromResult(permissions);
        }

        /// <summary>
        /// Add an user to a role
        /// </summary>
        /// <param name="userRole">MimirorgUserRoleAm</param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<bool> AddUserToRole(MimirorgUserRoleAm userRole)
        {
            var validation = userRole.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't add user to role: {userRole?.UserId} - {userRole?.MimirorgRoleId}", validation);

            var currentRole = await _roleManager.FindByIdAsync(userRole.MimirorgRoleId);
            if (currentRole == null)
                throw new MimirorgNotFoundException($"Couldn't find any role with id: {userRole.MimirorgRoleId}");

            var user = await _userManager.FindByIdAsync(userRole.UserId);
            if (user == null)
                throw new MimirorgNotFoundException($"Couldn't find any user with id: {userRole.UserId}");

            if (!await _userManager.IsInRoleAsync(user, currentRole.NormalizedName))
            {
                return await _userManager.AddToRoleAsync(user, currentRole.NormalizedName) == IdentityResult.Success;
            }

            return true;
        }

        /// <summary>
        /// Remove user from a role
        /// </summary>
        /// <param  name="userRole">MimirorgUserRoleAm</param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<bool> RemoveUserFromRole(MimirorgUserRoleAm userRole)
        {
            var validation = userRole.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Couldn't remove user from role: {userRole?.UserId} - {userRole?.MimirorgRoleId}", validation);

            var currentRole = await _roleManager.FindByIdAsync(userRole.MimirorgRoleId);
            if (currentRole == null)
                throw new MimirorgNotFoundException($"Couldn't find any role with id: {userRole.MimirorgRoleId}");

            var user = await _userManager.FindByIdAsync(userRole.UserId);
            if (user == null)
                throw new MimirorgNotFoundException($"Couldn't find any user with id: {userRole.UserId}");

            if (await _userManager.IsInRoleAsync(user, currentRole.NormalizedName))
            {
                return await _userManager.RemoveFromRoleAsync(user, currentRole.NormalizedName) == IdentityResult.Success;
            }

            return true;
        }

        /// <summary>
        /// Set user permission for a specific company and user.
        /// </summary>
        /// <param name="userPermission">MimirorgUserPermissionAm</param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<bool> SetPermissions(MimirorgUserPermissionAm userPermission)
        {
            var validation = userPermission.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException($"Set permissions error. Couldn't set permission for user {userPermission?.UserId}", validation);

            var user = await _userManager.FindByIdAsync(userPermission.UserId);
            if (user == null)
                throw new MimirorgNotFoundException($"Set permissions error. Couldn't find user with id {userPermission.UserId}");

            var company = await _mimirorgCompanyService.GetCompanyById(userPermission.CompanyId);
            if (company == null)
                throw new MimirorgNotFoundException($"Set permissions error. Couldn't find company with id {userPermission.CompanyId}");

            var newClaims = userPermission.Permissions
                .Select(x => (MimirorgPermission) x.Id)
                .Select(y => new Claim(company.Id.ToString(), y.ToString()))
                .ToList();

            var currentClaimsForUser = await _userManager.GetClaimsAsync(user);
            currentClaimsForUser = currentClaimsForUser.Where(x => x.Type.Equals(company.Id.ToString())).ToList();

            var status = await _userManager.RemoveClaimsAsync(user, currentClaimsForUser);
            if (!status.Succeeded)
                return status.Succeeded;

            status = await _userManager.AddClaimsAsync(user, newClaims);
            return status.Succeeded;
        }

        #endregion
    }
}