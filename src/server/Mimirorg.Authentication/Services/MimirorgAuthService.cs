using AspNetCore.Totp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System.Security.Authentication;
using System.Security.Claims;

namespace Mimirorg.Authentication.Services;

public class MimirorgAuthService : IMimirorgAuthService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<MimirorgUser> _userManager;
    private readonly SignInManager<MimirorgUser> _signInManager;
    private readonly IMimirorgTokenRepository _tokenRepository;
    private readonly IMimirorgCompanyService _mimirorgCompanyService;
    private readonly IActionContextAccessor _actionContextAccessor;
    private readonly MimirorgAuthSettings _authSettings;
    private readonly IMimirorgEmailRepository _emailRepository;
    private readonly IMimirorgTemplateRepository _templateRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public MimirorgAuthService(RoleManager<IdentityRole> roleManager, UserManager<MimirorgUser> userManager, SignInManager<MimirorgUser> signInManager, IMimirorgTokenRepository tokenRepository, IMimirorgCompanyService mimirorgCompanyService, IActionContextAccessor actionContextAccessor, IOptions<MimirorgAuthSettings> authSettings, IMimirorgEmailRepository emailRepository, IMimirorgTemplateRepository templateRepository, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenRepository = tokenRepository;
        _mimirorgCompanyService = mimirorgCompanyService;
        _actionContextAccessor = actionContextAccessor;
        _emailRepository = emailRepository;
        _templateRepository = templateRepository;
        _contextAccessor = contextAccessor;
        _authSettings = authSettings?.Value;
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
            throw new AuthenticationException($"The user account with email {authenticate.Email} could not be signed in. Status: {userStatus}");

        // Validate security code if user has enabled two factor
        if (!ValidateSecurityCode(user, authenticate.Code))
            throw new AuthenticationException($"The user account with email {authenticate.Email} could not validate code.");

        var now = DateTime.UtcNow;
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

        if (token.ValidTo < DateTime.UtcNow)
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

        var now = DateTime.UtcNow;
        var accessToken = await _tokenRepository.CreateAccessToken(user, now);
        var refreshToken = await _tokenRepository.CreateRefreshToken(user, now);

        return new List<MimirorgTokenCm> { accessToken, refreshToken };
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
    /// Get all permissions
    /// </summary>
    /// <returns>ICollection&lt;MimirorgPermissionCm&gt;</returns>
    public async Task<ICollection<MimirorgPermissionCm>> GetAllPermissions()
    {
        var permissions = MimirorgPermissionCm.FromPermissionEnum().ToList();
        return await Task.FromResult(permissions);
    }

    /// <summary>
    /// Set user permission for a specific company, and send an email to the user
    /// </summary>
    /// <param name="userPermission">MimirorgUserPermissionAm</param>
    /// <returns>Completed task</returns>
    /// <exception cref="MimirorgBadRequestException"></exception>
    /// <exception cref="MimirorgNotFoundException"></exception>
    public async Task SetPermission(MimirorgUserPermissionAm userPermission)
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

        var newClaim = new Claim(company.Id.ToString(), userPermission.Permission.ToString());

        var currentClaimsForUser = await _userManager.GetClaimsAsync(user);
        currentClaimsForUser = currentClaimsForUser.Where(x => x.Type.Equals(company.Id.ToString())).ToList();

        var status = await _userManager.RemoveClaimsAsync(user, currentClaimsForUser);
        if (!status.Succeeded)
            throw new MimirorgNotFoundException("Couldn't remove old permission for user");

        status = await _userManager.AddClaimsAsync(user, new List<Claim> { newClaim });
        if (!status.Succeeded)
            throw new MimirorgNotFoundException("Couldn't add new permission for user");

        await SendUserPermissionEmail(user, userPermission.Permission, company.Name, false);
    }

    /// <summary>
    /// Remove user permission for a specific company, and send an email to the user
    /// </summary>
    /// <param name="userPermission">MimirorgUserPermissionAm</param>
    /// <returns>Completed task</returns>
    /// <exception cref="MimirorgBadRequestException"></exception>
    /// <exception cref="MimirorgNotFoundException"></exception>
    public async Task RemovePermission(MimirorgUserPermissionAm userPermission)
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

        var currentClaimsForUser = await _userManager.GetClaimsAsync(user);
        currentClaimsForUser = currentClaimsForUser.Where(x => x.Type.Equals(company.Id.ToString()) && x.Value == userPermission.Permission.ToString()).ToList();

        var status = await _userManager.RemoveClaimsAsync(user, currentClaimsForUser);
        if (!status.Succeeded)
            throw new MimirorgNotFoundException("Couldn't remove permission for user");

        await SendUserPermissionEmail(user, userPermission.Permission, company.Name, true);
    }

    /// <summary>
    /// Check if user has permission to change the state for a given company
    /// </summary>
    /// <param name="companyId">The id of the company, or 0 for non-company objects</param>
    /// <param name="newState">The state to check for permission</param>
    /// <param name="currentState"></param>
    /// <returns>True if has access, otherwise it returns false</returns>
    /// <exception cref="ArgumentOutOfRangeException">If not a valid state</exception>
    public Task<bool> HasAccess(int companyId, State newState, State currentState)
    {
        var permission = newState switch
        {
            State.Draft => MimirorgPermission.Write,
            State.Approve => MimirorgPermission.Write,
            State.Delete => MimirorgPermission.Write,
            State.Approved => MimirorgPermission.Approve,
            State.Deleted => MimirorgPermission.Delete,
            State.Rejected => RejectStatePermissionNeeded(currentState),
            _ => throw new ArgumentOutOfRangeException(nameof(newState), newState, null)
        };

        var access = _actionContextAccessor.ActionContext?.HttpContext.HasPermission(permission, companyId.ToString());
        return Task.FromResult(access ?? false);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Returns needed MimirorgPermission to be able to reject a state change request
    /// </summary>
    /// <param name="currentState"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private MimirorgPermission RejectStatePermissionNeeded(State currentState)
    {
        switch (currentState)
        {
            case State.Delete:
                return MimirorgPermission.Delete;

            case State.Approve:
                return MimirorgPermission.Approve;

            default:
                throw new ArgumentOutOfRangeException($"Method 'CanRejectState' out of range. Current state is: {currentState}");
        }
    }

    /// <summary>
    /// Validate security code
    /// </summary>
    /// <param name="user">The user that should be validated</param>
    /// <param name="code">The security code</param>
    /// <remarks>If user is not set two factor to be enabled,
    /// the method will return </remarks>
    /// <returns>Returns true if code is valid</returns>
    /// <exception cref="AuthenticationException">Throws if configuration has error</exception>
    private bool ValidateSecurityCode(MimirorgUser user, string code)
    {
        if (_authSettings == null)
            throw new MimirorgConfigurationException("Application settings failure");

        if (!_authSettings.RequireConfirmedAccount)
            return true;

        if (!user.TwoFactorEnabled)
            return false;

        var validator = new TotpValidator(new TotpGenerator());

        if (!code.All(char.IsDigit))
            return false;

        if (!int.TryParse(code, out var codeInt))
            return false;

        return validator.Validate(user.SecurityHash, codeInt);
    }

    /// <summary>
    /// Sends an email to a user about permission
    /// </summary>
    /// <param name="toUser"></param>
    /// <param name="companyName"></param>
    /// <param name="permission"></param>
    /// <param name="isPermissionRemoval"></param>
    /// <returns></returns>
    private async Task SendUserPermissionEmail(MimirorgUser toUser, MimirorgPermission permission, string companyName, bool isPermissionRemoval)
    {
        /* We can not reference 'IMimirorgUserService' because that service is referencing this service.
         * If this reference is atempted it will result in a 'circular dependency' error. 
         * The 'MimirorgUserCm' objects needs to be manually constructed here.
         */

        var from = await _userManager.FindByIdAsync(_contextAccessor.GetUserId());

        if (from == null || toUser == null)
            throw new MimirorgNotFoundException("User(s) not found 'SendUserPermissionEmail'");

        var fromUser = new MimirorgUserCm
        {
            FirstName = from.FirstName,
            LastName = from.LastName,
            Email = from.Email
        };

        var sendToUser = new MimirorgUserCm
        {
            FirstName = toUser.FirstName,
            LastName = toUser.LastName,
            Email = toUser.Email
        };

        var email = await _templateRepository.CreateUserPermissionEmail(sendToUser, fromUser, permission, companyName, isPermissionRemoval);

        await _emailRepository.SendEmail(email);
    }

    #endregion
}