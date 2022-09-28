using System.Security.Authentication;
using System.Security.Claims;
using AspNetCore.Totp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
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
        private readonly IActionContextAccessor _actionContextAccessor;

        public MimirorgAuthService(RoleManager<IdentityRole> roleManager, UserManager<MimirorgUser> userManager, SignInManager<MimirorgUser> signInManager, IMimirorgTokenRepository tokenRepository, IMimirorgCompanyService mimirorgCompanyService, IActionContextAccessor actionContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
            _mimirorgCompanyService = mimirorgCompanyService;
            _actionContextAccessor = actionContextAccessor;
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

            if (!authenticate.Code.All(char.IsDigit))
                throw new AuthenticationException("Only digit is allowed in code");

            if (!int.TryParse(authenticate.Code, out var codeInt))
                throw new AuthenticationException("Only digit is allowed in code");

            var hasCorrectPin = validator.Validate(user.SecurityHash, codeInt);

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
        /// Verify email account from verify code
        /// </summary>
        /// <param name="email">User Email Address</param>
        /// <param name="code">Email Code</param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgInvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<bool> VerifyEmailAccount(string email, string code)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentNullException(nameof(code));

            var regToken = await _tokenRepository.FindBy(x => x.Secret == code && x.Email == email).FirstOrDefaultAsync(x => x.TokenType == MimirorgTokenType.VerifyEmail);

            if (regToken == null)
                throw new MimirorgNotFoundException("Could not verify account");

            var user = await _userManager.FindByEmailAsync(regToken.Email);
            if (user == null)
                throw new MimirorgNotFoundException("Could not verify account");

            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new MimirorgInvalidOperationException($"Couldn't verify account by email. Error: {result.Errors.ConvertToString()}");

            _tokenRepository.Attach(regToken, EntityState.Deleted);
            await _tokenRepository.SaveAsync();
            return result.Succeeded;
        }

        /// <summary>
        /// Remove the current user's authentication tokens
        /// </summary>
        /// <param name="secret">string</param>
        /// <returns></returns>
        public async Task Logout(string secret)
        {
            if (string.IsNullOrWhiteSpace(secret))
                return;

            var refreshToken = await _tokenRepository.FindBy((x) => x.Secret == secret && x.TokenType == MimirorgTokenType.RefreshToken).FirstOrDefaultAsync();

            if (refreshToken == null)
                return;

            await _tokenRepository.Delete(refreshToken.Id);
            await _tokenRepository.SaveAsync();

            await _signInManager.SignOutAsync();
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

        public Task<bool> HasAccess(int companyId, State state)
        {
            var permission = state switch
            {
                State.Draft => MimirorgPermission.Write,
                State.ApproveCompany => MimirorgPermission.Write,
                State.ApproveGlobal => MimirorgPermission.Write,
                State.Delete => MimirorgPermission.Write,
                State.ApprovedCompany => MimirorgPermission.Approve,
                State.ApprovedGlobal => MimirorgPermission.Approve,
                State.Deleted => MimirorgPermission.Delete,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };

            var access = _actionContextAccessor.ActionContext?.HttpContext.HasPermission(permission, companyId.ToString());
            return Task.FromResult(access ?? false);
        }



        #endregion
    }
}