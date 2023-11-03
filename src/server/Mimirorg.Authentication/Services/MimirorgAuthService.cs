using AspNetCore.Totp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Authentication.Models.Domain;
using System.Security.Authentication;
using System.Security.Claims;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Authentication.Models;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Client;
using Tyle.Core.Common;

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

        if (!await _userManager.IsInRoleAsync(user, currentRole.NormalizedName!))
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

        if (await _userManager.IsInRoleAsync(user, currentRole.NormalizedName!))
        {
            return await _userManager.RemoveFromRoleAsync(user, currentRole.NormalizedName) == IdentityResult.Success;
        }

        return true;
    }

    #endregion

    #region Private Methods

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

    #endregion
}