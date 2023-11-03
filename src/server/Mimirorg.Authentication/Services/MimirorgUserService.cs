using AspNetCore.Totp;
using AspNetCore.Totp.Interface.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Authentication.Models.Constants;
using Mimirorg.Authentication.Models.Domain;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Authentication.Models;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Client;

namespace Mimirorg.Authentication.Services;

public class MimirorgUserService : IMimirorgUserService
{
    private readonly UserManager<MimirorgUser> _userManager;
    private readonly MimirorgAuthSettings _authSettings;
    private readonly IMimirorgTokenRepository _tokenRepository;
    private readonly IMimirorgEmailRepository _emailRepository;
    private readonly IMimirorgTemplateRepository _templateRepository;
    private readonly IMimirorgAuthService _mimirorgAuthService;

    public MimirorgUserService(UserManager<MimirorgUser> userManager, IOptions<MimirorgAuthSettings> authSettings, IMimirorgTokenRepository tokenRepository, IMimirorgEmailRepository emailRepository, IMimirorgTemplateRepository templateRepository, IMimirorgAuthService mimirorgAuthService)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
        _emailRepository = emailRepository;
        _templateRepository = templateRepository;
        _authSettings = authSettings?.Value;
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

        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);

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

        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);

        return user.ToContentModel();
    }

    /// <inheritdoc />
    public async Task<List<MimirorgUserCm>> GetUsers()
    {
        var userCms = new List<MimirorgUserCm>();

        foreach (var user in _userManager.Users.ToList())
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = await _userManager.GetClaimsAsync(user);
            userCms.Add(user.ToContentModel());
        }

        return userCms;
    }

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="userAm">New last name</param>
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


        var numberOfUsers = await _userManager.Users.CountAsync();

        if (numberOfUsers != 1)
            return;
        await _userManager.AddToRoleAsync(user, MimirorgDefaultRoles.Administrator);
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

        user.EmailConfirmed = !_authSettings.RequireConfirmedAccount;
        user.TwoFactorEnabled = !_authSettings.RequireConfirmedAccount;

        var result = await _userManager.CreateAsync(user, userAm.Password);
        if (!result.Succeeded)
            throw new MimirorgInvalidOperationException($"Couldn't register: {userAm.Email}. Error: {result.Errors.ConvertToString()}");

        // Create an email verification token and send email to user
        await CreateAndSendUserTokens(user, new List<MimirorgTokenType> { MimirorgTokenType.VerifyEmail, MimirorgTokenType.ChangeTwoFactor });

        // If this is the first registered user and environment is Development, create a dummy organization
        await CreateDefaultUserData(user);

        var userCm = user.ToContentModel();

        return userCm;
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