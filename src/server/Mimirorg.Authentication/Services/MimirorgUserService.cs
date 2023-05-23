using System.Security.Principal;
using AspNetCore.Totp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Constants;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using System.Security.Cryptography;
using System.Text;
using AspNetCore.Totp.Interface.Models;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Services;

public class MimirorgUserService : IMimirorgUserService
{
    private readonly UserManager<MimirorgUser> _userManager;
    private readonly MimirorgAuthSettings _authSettings;
    private readonly IMimirorgTokenRepository _tokenRepository;
    private readonly IMimirorgEmailRepository _emailRepository;
    private readonly IMimirorgTemplateRepository _templateRepository;
    private readonly IMimirorgCompanyService _mimirorgCompanyService;
    private readonly IMimirorgAuthService _mimirorgAuthService;

    public MimirorgUserService(UserManager<MimirorgUser> userManager, IOptions<MimirorgAuthSettings> authSettings, IMimirorgTokenRepository tokenRepository, IMimirorgEmailRepository emailRepository, IMimirorgTemplateRepository templateRepository, IMimirorgCompanyService mimirorgCompanyService, IMimirorgAuthService mimirorgAuthService)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
        _emailRepository = emailRepository;
        _templateRepository = templateRepository;
        _authSettings = authSettings?.Value;
        _mimirorgCompanyService = mimirorgCompanyService;
        _mimirorgAuthService = mimirorgAuthService;
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

        var userCm = user.ToContentModel();

        var companies = await _mimirorgCompanyService.GetAllCompanies();
        var permissions = await _mimirorgAuthService.GetAllPermissions();
        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);
        userCm.ResolvePermissions(roles, claims, companies, permissions);
        userCm.ResolveRoles(roles, claims, companies, permissions);

        return userCm;
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

        var userCm = user.ToContentModel();

        var companies = await _mimirorgCompanyService.GetAllCompanies();
        var permissions = await _mimirorgAuthService.GetAllPermissions();
        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);
        userCm.ResolvePermissions(roles, claims, companies, permissions);
        userCm.ResolveRoles(roles, claims, companies, permissions);

        return userCm;
    }

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="id">Id of user to update</param>
    /// <param name="firstName">New first name</param>
    /// <param name="lastName">New last name</param>
    /// <returns>UserCm</returns>
    /// <exception cref="MimirorgNotFoundException"></exception>
    /// <exception cref="MimirorgInvalidOperationException"></exception>
    public async Task<MimirorgUserCm> UpdateUser(MimirorgUserAm userAm)
    {
        var user = await _userManager.FindByEmailAsync(userAm.Email);

        if (user == null)
            throw new MimirorgNotFoundException("The user was not found");

        user.FirstName = userAm.FirstName;
        user.LastName = userAm.LastName;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            throw new MimirorgInvalidOperationException($"Couldn't update user with username {user.UserName}. Error: {result.Errors.ConvertToString()}");

        return user.ToContentModel();
    }

    /// <summary>
    /// Gets all companies that the principal can access given a specific permission level
    /// </summary>
    /// <param name="principal"></param>
    /// <param name="permission"></param>
    /// <returns>A collection of company ids</returns>
    /// <exception cref="MimirorgNotFoundException"></exception>
    public async Task<ICollection<int>> GetCompaniesForUser(IPrincipal principal, MimirorgPermission permission)
    {
        if (principal?.Identity?.Name == null)
            throw new MimirorgNotFoundException("Couldn't find current user");

        var user = await GetUser(principal);
        var userPermissions = user.Permissions.Where(x => x.Value == permission);
        var userCompanies = userPermissions.Select(x => x.Key);

        return userCompanies.ToList();
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
    public async Task<MimirorgUserCm> CreateUser(MimirorgUserAm userAm)
    {
        if (_authSettings == null)
            throw new MimirorgConfigurationException("Missing configuration for auth settings");

        var existingUser = await _userManager.FindByEmailAsync(userAm.Email);
        if (existingUser == null)
            return await CreateNewUser(userAm);

        if (!existingUser.EmailConfirmed)
        {
            return await UpdateTempUser(userAm, existingUser);
        }
        throw new MimirorgDuplicateException($"Couldn't register: {userAm.Email}. There is already an user with same username");
    }

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
    public async Task<MimirorgQrCodeCm> GenerateTwoFactor(MimirorgVerifyAm verifyEmail)
    {
        if (_authSettings == null)
            throw new MimirorgConfigurationException("Missing configuration for auth settings");

        var regToken = await _tokenRepository.FindBy(x => x.Secret == verifyEmail.Code && x.Email == verifyEmail.Email).FirstOrDefaultAsync(x => x.TokenType == MimirorgTokenType.ChangeTwoFactor);

        if (regToken == null)
            throw new MimirorgNotFoundException("Could not verify account");

        var user = await _userManager.FindByEmailAsync(regToken.Email);
        if (user == null)
            throw new MimirorgNotFoundException("Could not verify account");

        if (!user.EmailConfirmed)
            throw new MimirorgInvalidOperationException("Could not enable two factor auth before user is confirmed");

        var totpSetup = CreateTotp(user);
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
            throw new MimirorgInvalidOperationException($"Couldn't verify account by email. Error: {result.Errors.ConvertToString()}");

        _tokenRepository.Attach(regToken, EntityState.Deleted);
        await _tokenRepository.SaveAsync();

        return new MimirorgQrCodeCm
        {
            Code = totpSetup.QrCodeImage,
            ManualCode = totpSetup.ManualSetupKey
        };
    }

    /// <summary>
    /// A method that generates change password secrets and sending the generated code to user as mail.
    /// </summary>
    /// <param name="email">The email address for the user secret token</param>
    /// <returns>A completed task</returns>
    /// <exception cref="MimirorgInvalidOperationException">Throws if user does not exist</exception>
    public async Task GenerateChangePasswordSecret(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            throw new MimirorgInvalidOperationException($"Couldn't generate secret for user with email: {email}");

        var alreadyExistToken = await _tokenRepository.Exist(x => x.TokenType == MimirorgTokenType.ChangePassword);

        if (alreadyExistToken)
            throw new MimirorgInvalidOperationException($"You can't create multiple change password secrets with same email: {email}");

        var secret = await CreateUserToken(user, new List<MimirorgTokenType> { MimirorgTokenType.ChangePassword, MimirorgTokenType.ChangeTwoFactor });
        var emailTemplate = await _templateRepository.CreateCodeVerificationMail(user, secret);
        await _emailRepository.SendEmail(emailTemplate);
    }

    /// <summary>
    /// Change the password on user
    /// </summary>
    /// <param name="changePassword">Object information for resetting password</param>
    /// <returns>A completed task</returns>
    /// <exception cref="MimirorgNotFoundException">Throws if user or token not exist</exception>
    public async Task<bool> ChangePassword(MimirorgChangePasswordAm changePassword)
    {
        var regToken = await _tokenRepository.FindBy(x =>
                x.Secret == changePassword.Code &&
                x.Email == changePassword.Email)
            .FirstOrDefaultAsync(x => x.TokenType == MimirorgTokenType.ChangePassword);

        if (regToken == null)
            throw new MimirorgNotFoundException("Could not verify account");

        var user = await _userManager.FindByEmailAsync(regToken.Email);

        if (user == null)
            throw new MimirorgNotFoundException("Could not verify account");

        _tokenRepository.Attach(regToken, EntityState.Deleted);
        await _tokenRepository.SaveAsync();

        if (user.PasswordHash != null)
            await _userManager.RemovePasswordAsync(user);

        var result = await _userManager.AddPasswordAsync(user, changePassword.Password);
        return result.Succeeded;
    }

    /// <summary>
    /// Cleanup tokens and not confirmed users
    /// </summary>
    /// <remarks>All users that has not any valid verify token and is not confirmed will be deleted,
    /// with all the user tokens. Also invalid tokens will be deleted.</remarks>
    /// <returns>The number of deleted users and tokens</returns>
    public async Task<(int deletedUsers, int deletedTokens)> RemoveUnconfirmedUsersAndTokens()
    {
        var allTokens = _tokenRepository.GetAll().ToList();
        var allNotConfirmedUsers = _userManager.Users.Where(x => !x.EmailConfirmed).ToList();
        var allNotConfirmedUsersWithValidToken = allNotConfirmedUsers.Where(x => allTokens.Any(y => y.ClientId == x.Id && y.ValidTo > DateTime.UtcNow && y.TokenType == MimirorgTokenType.VerifyEmail)).ToList();

        // This users should be deleted
        var deleteUsers = allNotConfirmedUsers.Where(x => allNotConfirmedUsersWithValidToken.All(y => x.Id != y.Id)).ToList();
        var deleteUserTokens = allTokens.Where(x => deleteUsers.Any(y => y.Id == x.ClientId)).ToList();
        var deleteInvalidTokens = allTokens.Where(x => x.ValidTo < DateTime.UtcNow).ToList();

        var deleteTokens = deleteUserTokens.Union(deleteInvalidTokens).ToList();

        foreach (var token in deleteTokens)
            _tokenRepository.Attach(token, EntityState.Deleted);

        await _tokenRepository.SaveAsync();

        foreach (var user in deleteUsers)
            await _userManager.DeleteAsync(user);

        return (deleteUsers.Count, deleteTokens.Count);
    }

    /// <summary>
    /// Verify email account from verify code
    /// </summary>
    /// <param name="verifyEmail">The email verify data</param>
    /// <returns>bool</returns>
    /// <exception cref="MimirorgInvalidOperationException"></exception>
    /// <exception cref="MimirorgNotFoundException"></exception>
    public async Task<bool> VerifyAccount(MimirorgVerifyAm verifyEmail)
    {
        var regToken = await _tokenRepository.FindBy(x => x.Secret == verifyEmail.Code && x.Email == verifyEmail.Email).FirstOrDefaultAsync(x => x.TokenType == MimirorgTokenType.VerifyEmail);

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

    #region Private methods

    /// <summary>
    /// Create user token
    /// </summary>
    /// <param name="user">Actual user</param>
    /// <param name="tokenTypes">Token type</param>
    /// <returns>Token string</returns>
    /// <remarks>Generates a 6 long digit for verification code token</remarks>
    private async Task<string> CreateUserToken(MimirorgUser user, IEnumerable<MimirorgTokenType> tokenTypes)
    {

        var generator = new Random();
        var secret = generator.Next(0, 1000000).ToString("D6");

        var deleteTokens = _tokenRepository.GetAll()
            .Where(x => x.ClientId == user.Id || DateTime.UtcNow > x.ValidTo).ToList();

        foreach (var t in deleteTokens)
            _tokenRepository.Attach(t, EntityState.Deleted);

        await _tokenRepository.SaveAsync();

        foreach (var tokenType in tokenTypes)
        {
            var token = new MimirorgToken
            {
                ClientId = user.Id,
                Email = user.Email,
                Secret = secret,
                TokenType = tokenType,
                ValidTo = DateTime.UtcNow.AddHours(1)
            };
            _tokenRepository.Attach(token, EntityState.Added);
        }

        await _tokenRepository.SaveAsync();

        return secret;
    }

    /// <summary>
    /// Create a Sha256Hash
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private static string ComputeSha256Hash(string data)
    {
        // Create a SHA256   
        using var sha256Hash = SHA256.Create();

        // ComputeHash - returns byte array  
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

        // Convert byte array to a string   
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }
        return builder.ToString();
    }

    /// <summary>
    /// Create default user data
    /// </summary>
    /// <param name="user">Actual user</param>
    /// <returns>Completed task</returns>
    /// <remarks>Create default data for user, if the user is the first registered user, and is in development environment.</remarks>
    private async Task CreateDefaultUserData(MimirorgUser user)
    {
        // If this is the first registered user and environment is Development, create a dummy organization
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var isDevelopment = !string.IsNullOrWhiteSpace(environment) && environment.ToLower() == "development";

        if (!isDevelopment)
            return;

        try
        {
            var numberOfUsers = await _userManager.Users.CountAsync();
            var numberOfCompanies = (await _mimirorgCompanyService.GetAllCompanies()).Count();

            if (numberOfUsers != 1 || numberOfCompanies != 0)
                return;

            var company = await _mimirorgCompanyService.CreateCompany(new MimirorgCompanyAm
            {
                Name = "Mimirorg Company",
                Description = "Mimirorg is the open source community for Mimir and Tyle",
                DisplayName = "Mimirorg Company",
                Secret = ComputeSha256Hash("Mimirorg Company"),
                ManagerId = user.Id,
                Domain = "runir.net",
                Logo = @"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48c3ZnIGlkPSJMYXllcl8xIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHZpZXdCb3g9IjAgMCA2NTAgMjUwIj48ZGVmcz48c3R5bGU+LmNscy0xe2ZpbGw6I2Y2ZjZmNjt9LmNscy0ye2ZpbGw6I2U2MDA3ZTt9PC9zdHlsZT48L2RlZnM+PHBhdGggY2xhc3M9ImNscy0xIiBkPSJNMTAwLjg0LDE0NS45MmMwLC44Mi0uNDgsMS41Ny0xLjI0LDEuODktMy4xNiwxLjMxLTcuNjYsMi4wOC0xMC45NywyLjA4LTE3LjU0LDAtMjkuMi0xMi42Ni0yOS4yLTI4Ljg2czExLjY2LTI4Ljc1LDI5LjItMjguNzVjMy4yLDAsNy43NiwuNzYsMTAuOTMsMS45OSwuNzgsLjMsMS4yOCwxLjA3LDEuMjgsMS45MXYxMS4xYzAsMS41MS0xLjU3LDIuNDktMi45MywxLjg1LTIuNTUtMS4yMS01LjctMi4wOC05LjI4LTIuMDgtOC4xLDAtMTMuODgsNi4zMy0xMy44OCwxMy45OXM1Ljc3LDE0LjEsMTMuODgsMTQuMWMzLjUsMCw2LjcyLS44Nyw5LjI5LTIuMTIsMS4zNS0uNjUsMi45MiwuMzUsMi45MiwxLjg2djExLjA1WiIvPjxwYXRoIGNsYXNzPSJjbHMtMSIgZD0iTTEzNS4yOCw5Mi4yN2MxNi4yMSwwLDI4Ljc1LDEyLjU0LDI4Ljc1LDI4Ljc1cy0xMi41NCwyOC44Ni0yOC43NSwyOC44Ni0yOC43NS0xMi42Ni0yOC43NS0yOC44NiwxMi41NC0yOC43NSwyOC43NS0yOC43NVptMCw0Mi44NWM3Ljc3LDAsMTMuMzItNi40NCwxMy4zMi0xNC4xcy01LjU1LTEzLjk5LTEzLjMyLTEzLjk5LTEzLjQzLDYuMzMtMTMuNDMsMTMuOTksNS43NywxNC4xLDEzLjQzLDE0LjFaIi8+PHBhdGggY2xhc3M9ImNscy0xIiBkPSJNMTg2LjUxLDk5LjkzYzIuODktNC40NCw3LjU1LTcuNjYsMTQuMS03LjY2LDcuNzcsMCwxMy44OCwzLjU1LDE2Ljk5LDkuOTksMi42Ni01LjY2LDcuOTktOS45OSwxNi4zMi05Ljk5LDEwLjY2LDAsMTguOTgsOC40NCwxOC45OCwyMC4zMnYzNC4wM2MwLDEuMTMtLjkyLDIuMDUtMi4wNSwyLjA1aC0xMC45OWMtMS4xMywwLTIuMDUtLjkyLTIuMDUtMi4wNXYtMjkuN2MwLTYuMjItMy41NS05Ljg4LTguODgtOS44OHMtOS4yMSwzLjY2LTkuMjEsOS44OHYyOS43YzAsMS4xMy0uOTIsMi4wNS0yLjA1LDIuMDVoLTEwLjk5Yy0xLjEzLDAtMi4wNS0uOTItMi4wNS0yLjA1di0yOS43YzAtNi4yMi0zLjU1LTkuODgtOC44OC05Ljg4cy04Ljg4LDMuMzMtOS4yMSw5LjF2MzAuNDhjMCwxLjEzLS45MiwyLjA1LTIuMDUsMi4wNWgtMTAuOTljLTEuMTMsMC0yLjA1LS45Mi0yLjA1LTIuMDV2LTUxLjE4YzAtMS4xMywuOTItMi4wNSwyLjA1LTIuMDVoMTAuOTljMS4xMywwLDIuMDUsLjkyLDIuMDUsMi4wNXY0LjVaIi8+PHBhdGggY2xhc3M9ImNscy0xIiBkPSJNMjc5LjA3LDk5LjkzYzIuODktNC40NCw3LjU1LTcuNjYsMTQuMS03LjY2LDcuNzcsMCwxMy44OCwzLjU1LDE2Ljk5LDkuOTksMi42Ni01LjY2LDcuOTktOS45OSwxNi4zMi05Ljk5LDEwLjY2LDAsMTguOTgsOC40NCwxOC45OCwyMC4zMnYzNC4wM2MwLDEuMTMtLjkyLDIuMDUtMi4wNSwyLjA1aC0xMC45OWMtMS4xMywwLTIuMDUtLjkyLTIuMDUtMi4wNXYtMjkuN2MwLTYuMjItMy41NS05Ljg4LTguODgtOS44OHMtOS4yMSwzLjY2LTkuMjEsOS44OHYyOS43YzAsMS4xMy0uOTIsMi4wNS0yLjA1LDIuMDVoLTEwLjk5Yy0xLjEzLDAtMi4wNS0uOTItMi4wNS0yLjA1di0yOS43YzAtNi4yMi0zLjU1LTkuODgtOC44OC05Ljg4cy04Ljg4LDMuMzMtOS4yMSw5LjF2MzAuNDhjMCwxLjEzLS45MiwyLjA1LTIuMDUsMi4wNWgtMTAuOTljLTEuMTMsMC0yLjA1LS45Mi0yLjA1LTIuMDV2LTUxLjE4YzAtMS4xMywuOTItMi4wNSwyLjA1LTIuMDVoMTAuOTljMS4xMywwLDIuMDUsLjkyLDIuMDUsMi4wNXY0LjVaIi8+PHBhdGggY2xhc3M9ImNscy0xIiBkPSJNMzg4LjEyLDE0MS43OGMtMi43OCw0Ljc3LTcuNTUsOC4xLTE0LjU0LDguMS0xMS42NiwwLTE5LjMyLTguODgtMTkuMzItMjAuNzZ2LTMzLjdjMC0xLjEzLC45Mi0yLjA1LDIuMDUtMi4wNWgxMC45OWMxLjEzLDAsMi4wNSwuOTIsMi4wNSwyLjA1djI5LjM3YzAsNi4yMiwzLjU1LDEwLjMyLDkuMzMsMTAuMzJzOS4xLTMuNzcsOS40NC05LjU1di0zMC4xNGMwLTEuMTMsLjkyLTIuMDUsMi4wNS0yLjA1aDEwLjk5YzEuMTMsMCwyLjA1LC45MiwyLjA1LDIuMDV2NTEuMThjMCwxLjEzLS45MiwyLjA1LTIuMDUsMi4wNWgtMTAuOTljLTEuMTMsMC0yLjA1LS45Mi0yLjA1LTIuMDV2LTQuODNaIi8+PHBhdGggY2xhc3M9ImNscy0xIiBkPSJNNDI4Ljk3LDEwMC4zN2MyLjkyLTQuNjUsNy40MS03LjkzLDE0LTguMSwxMS4wOC0uMjksMjAuMDgsOC45NiwyMC4wOCwyMC4wNHYzNC4zYzAsMS4xMy0uOTIsMi4wNS0yLjA1LDIuMDVoLTExLjFjLTEuMTMsMC0yLjA1LS45Mi0yLjA1LTIuMDV2LTI5LjM3YzAtNi4xMS0zLjY2LTEwLjIxLTkuNDQtMTAuMjFzLTkuMSwzLjY2LTkuNDQsOS41NXYzMC4wM2MwLDEuMTMtLjkyLDIuMDUtMi4wNSwyLjA1aC0xMC45OWMtMS4xMywwLTIuMDUtLjkyLTIuMDUtMi4wNXYtNTEuMThjMC0xLjEzLC45Mi0yLjA1LDIuMDUtMi4wNWgxMC45OWMxLjEzLDAsMi4wNSwuOTIsMi4wNSwyLjA1djQuOTRaIi8+PHBhdGggY2xhc3M9ImNscy0xIiBkPSJNNTA1LjAxLDE0Ni42MXYtNDAuMzZoLTYuMzhjLTEuMTMsMC0yLjA1LS45Mi0yLjA1LTIuMDV2LTguNzdjMC0xLjEzLC45Mi0yLjA1LDIuMDUtMi4wNWg3LjI3di0xMC40NmMwLS45NiwuNjYtMS43OSwxLjYtMmwxMC4xMS0yLjI5YzEuMjgtLjI5LDIuNTEsLjY5LDIuNTEsMnYxMi43NWg3LjI3YzEuMTMsMCwyLjA1LC45MiwyLjA1LDIuMDV2OC43N2MwLDEuMTMtLjkyLDIuMDUtMi4wNSwyLjA1aC03LjI3djQwLjM2YzAsMS4xMy0uOTIsMi4wNS0yLjA1LDIuMDVoLTEwLjk5Yy0xLjEzLDAtMi4wNS0uOTItMi4wNS0yLjA1WiIvPjxwYXRoIGNsYXNzPSJjbHMtMSIgZD0iTTU1My44NiwxNDQuNDVsLTE5LjItNDguMjZjLS41NC0xLjM1LC40Ni0yLjgxLDEuOTEtMi44MWgxMi41MmMuODYsMCwxLjYzLC41NCwxLjkzLDEuMzVsMTEuMTcsMzAuNjIsMTEuODItMzAuNjZjLjMxLS43OSwxLjA3LTEuMzEsMS45MS0xLjMxaDEyLjZjMS40NywwLDIuNDYsMS41LDEuODksMi44NWwtMzEuMTMsNzMuOTRjLS4zMiwuNzYtMS4wNywxLjI2LTEuODksMS4yNmgtMTIuOTVjLTEuNSwwLTIuNS0xLjU2LTEuODYtMi45MmwxMS4yOC0yNC4wNVoiLz48Zz48cGF0aCBjbGFzcz0iY2xzLTEiIGQ9Ik00ODUuNTgsMTQ4LjY3aC0xMC4yMWMtMS4zNSwwLTIuNDUtMS4zLTIuNDUtMi45di00OS40OWMwLTEuNiwxLjEtMi45LDIuNDUtMi45aDEwLjIxYzEuMzUsMCwyLjQ1LDEuMywyLjQ1LDIuOXY0OS40OWMwLDEuNi0xLjEsMi45LTIuNDUsMi45WiIvPjxwYXRoIGNsYXNzPSJjbHMtMiIgZD0iTTQ4My41NCw5MC41NGwtNS45NS0uMDhjLTMuNDgsMC01LjMxLTQuMTMtMi45Ny02LjcxbDMuMzgtMy41NWMuNzYtLjg0LDEuODQtMS4zMSwyLjk2LTEuMzFoMTMuMTFjLjg0LDAsMS4yOCwxLC43MiwxLjYybC04LjY1LDkuMDdjLS43MywuNjItMS42NSwuOTYtMi42MSwuOTZaIi8+PC9nPjwvc3ZnPg==",
                HomePage = @"https://github.com/mimir-org/mimir"
            });

            await _userManager.AddToRoleAsync(user, MimirorgDefaultRoles.Administrator);

            // Create hooks
            var cacheKeys = EnumExtensions.AsEnumerable<CacheKey>().ToList();
            foreach (var cacheKey in cacheKeys)
            {
                var hook = new MimirorgHookAm
                {
                    CompanyId = company.Id,
                    Iri = "http://mimirserver/v1.0/common/cache/invalidate",
                    Key = cacheKey
                };

                _ = await _mimirorgCompanyService.CreateHook(hook);
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    /// <summary>
    /// Update temporary created user
    /// </summary>
    /// <param name="userAm">User data</param>
    /// <param name="existingUser">Existing user</param>
    /// <returns>The updated user</returns>
    /// <exception cref="MimirorgInvalidOperationException">Throws if user could not be updated or password could be changed</exception>
    /// <remarks>This is the workflow for register new user, if the user did not fulfilled the registration the first time</remarks>
    private async Task<MimirorgUserCm> UpdateTempUser(MimirorgUserAm userAm, MimirorgUser existingUser)
    {
        existingUser.UpdateDomainModel(userAm);
        var result = await _userManager.UpdateAsync(existingUser);

        if (!result.Succeeded)
            throw new MimirorgInvalidOperationException($"Couldn't update user: {userAm.Email}. Error: {result.Errors.ConvertToString()}");

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
        result = await _userManager.ResetPasswordAsync(existingUser, resetToken, userAm.Password);

        if (!result.Succeeded)
            throw new MimirorgInvalidOperationException($"Couldn't update user password: {userAm.Email}. Error: {result.Errors.ConvertToString()}");

        // Create an email verification token and send email to user
        await CreateAndSendUserTokens(existingUser, new List<MimirorgTokenType> { MimirorgTokenType.VerifyEmail, MimirorgTokenType.ChangeTwoFactor });
        return existingUser.ToContentModel();
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="userAm">User data</param>
    /// <returns>The created user</returns>
    /// <exception cref="MimirorgInvalidOperationException">Throws if user could not be created</exception>
    /// <remarks>This is the workflow for register new user, if the user did not fulfilled the registration the first time</remarks>
    private async Task<MimirorgUserCm> CreateNewUser(MimirorgUserAm userAm)
    {
        var user = userAm.ToDomainModel();

        if (userAm.CompanyId > 0)
        {
            var currentCompany = await _mimirorgCompanyService.GetCompanyById(userAm.CompanyId);
            user.CompanyName = currentCompany?.DisplayName ?? currentCompany?.Name;
        }

        user.EmailConfirmed = !_authSettings.RequireConfirmedAccount;
        user.TwoFactorEnabled = !_authSettings.RequireConfirmedAccount;

        var result = await _userManager.CreateAsync(user, userAm.Password);
        if (!result.Succeeded)
            throw new MimirorgInvalidOperationException($"Couldn't register: {userAm.Email}. Error: {result.Errors.ConvertToString()}");

        // Create an email verification token and send email to user
        await CreateAndSendUserTokens(user, new List<MimirorgTokenType> { MimirorgTokenType.VerifyEmail, MimirorgTokenType.ChangeTwoFactor });

        // If this is the first registered user and environment is Development, create a dummy organization
        await CreateDefaultUserData(user);
        return user.ToContentModel();
    }

    /// <summary>
    /// Generate and send user tokens to the user
    /// </summary>
    /// <param name="user">Current user</param>
    /// <param name="types">A collection of types</param>
    /// <returns>Completed task</returns>
    private async Task CreateAndSendUserTokens(MimirorgUser user, IEnumerable<MimirorgTokenType> types)
    {
        // Create an email verification token and send email to user
        if (!_authSettings.RequireConfirmedAccount)
            return;

        var secret = await CreateUserToken(user, types);
        var email = await _templateRepository.CreateCodeVerificationMail(user, secret);
        await _emailRepository.SendEmail(email);
    }

    /// <summary>
    /// Create a totp
    /// </summary>
    /// <param name="user">The user you want to create totp for</param>
    /// <returns>Totp setup object</returns>
    /// <exception cref="MimirorgConfigurationException">Throws if exception is thrown</exception>
    /// <remarks>This method will generate and set the Security Hash on the object.
    /// It will also set TwoFactorEnabled as true on the user object</remarks>
    private TotpSetup CreateTotp(MimirorgUser user)
    {
        if (_authSettings?.ApplicationName == null)
            throw new MimirorgConfigurationException("Missing configuration for auth settings");

        var securityKey = $"{Guid.NewGuid()}{MimirorgSecurity.SecurityStamp}{Guid.NewGuid()}";
        user.SecurityHash = securityKey.CreateSha512();
        user.TwoFactorEnabled = true;

        var totpSetupGenerator = new TotpSetupGenerator();
        return totpSetupGenerator.Generate(_authSettings.ApplicationName, user.Email, user.SecurityHash, _authSettings?.QrWidth ?? 300, _authSettings?.QrHeight ?? 300);
    }

    #endregion
}