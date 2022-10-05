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
using Microsoft.AspNetCore.Http;
using Mimirorg.Authentication.Extensions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Services
{
    public class MimirorgUserService : IMimirorgUserService
    {
        private readonly UserManager<MimirorgUser> _userManager;
        private readonly MimirorgAuthSettings _authSettings;
        private readonly IMimirorgTokenRepository _tokenRepository;
        private readonly IMimirorgEmailRepository _emailRepository;
        private readonly IMimirorgTemplateRepository _templateRepository;
        private readonly IMimirorgCompanyService _mimirorgCompanyService;
        private readonly IMimirorgAuthService _mimirorgAuthService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MimirorgUserService(UserManager<MimirorgUser> userManager, IOptions<MimirorgAuthSettings> authSettings, IMimirorgTokenRepository tokenRepository, IMimirorgEmailRepository emailRepository, IMimirorgTemplateRepository templateRepository, IMimirorgCompanyService mimirorgCompanyService, IMimirorgAuthService mimirorgAuthService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
            _emailRepository = emailRepository;
            _templateRepository = templateRepository;
            _authSettings = authSettings?.Value;
            _mimirorgCompanyService = mimirorgCompanyService;
            _mimirorgAuthService = mimirorgAuthService;
            _httpContextAccessor = httpContextAccessor;
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
            if (existingUser != null)
                throw new MimirorgDuplicateException($"Couldn't register: {userAm.Email}. There is already an user with same username");

            var user = userAm.ToDomainModel();
            
            var currentCompany = await _mimirorgCompanyService.GetCompanyById(userAm.CompanyId);
            user.CompanyName = currentCompany?.DisplayName ?? currentCompany?.Name;
            user.EmailConfirmed = !_authSettings.RequireConfirmedAccount;

            var result = await _userManager.CreateAsync(user, userAm.Password);
            if (!result.Succeeded)
                throw new MimirorgInvalidOperationException($"Couldn't register: {userAm.Email}. Error: {result.Errors.ConvertToString()}");

            // Create an email verification token and send email to user
            if (_authSettings.RequireConfirmedAccount)
            {
                var secret = await CreateUserToken(user, new List<MimirorgTokenType> { MimirorgTokenType.VerifyEmail, MimirorgTokenType.ChangeTwoFactor });
                var email = await _templateRepository.CreateCodeVerificationMail(user, secret);
                await _emailRepository.SendEmail(email);
            }

            // If this is the first registered user and environment is Development, create a dummy organization
            await CreateDefaultUserData(user);
            return user.ToContentModel();
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

            var companies = (await _mimirorgCompanyService.GetAllCompanies()).ToList();
            var permissions = (await _mimirorgAuthService.GetAllPermissions()).ToList();
            var permissionDictionary = await ResolveCompanies(companies, permissions, user);
            var roleDescriptions = await ResolveRoles(companies, permissions, user);
            userCm.Permissions = permissionDictionary;
            userCm.Roles = roleDescriptions;
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

            var companies = (await _mimirorgCompanyService.GetAllCompanies()).ToList();
            var permissions = (await _mimirorgAuthService.GetAllPermissions()).ToList();
            var permissionDictionary = await ResolveCompanies(companies, permissions, user);
            var roleDescriptions = await ResolveRoles(companies, permissions, user);
            userCm.Permissions = permissionDictionary;
            userCm.Roles = roleDescriptions;
            return userCm;
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
        /// Get companies that is registered for current logged in user
        /// </summary>
        /// <returns>A collection of registered companies</returns>
        public async Task<ICollection<MimirorgCompanyCm>> GetUserFilteredCompanies()
        {
            var user = await GetUser(_httpContextAccessor.GetUser());

            if (user == null)
                return new List<MimirorgCompanyCm>();

            var companies = (await _mimirorgCompanyService.GetAllCompanies()).ToList();
            companies = companies.Where(x => user.Permissions.ContainsKey(x.Id)).ToList();
            return companies;
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
        public async Task<bool> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgInvalidOperationException("Can't delete a user without an id");

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new MimirorgNotFoundException($"Couldn't find user with id {id}");

            return await _userManager.DeleteAsync(user) == IdentityResult.Success;
        }

        /// <summary>
        /// A method that generates a login code and sending the generated code to user as mail.
        /// </summary>
        /// <param name="generateSecret">Object information for generating secret</param>
        /// <returns>A completed task</returns>
        /// <exception cref="MimirorgBadRequestException">Throws if model is not valid</exception>
        /// <exception cref="MimirorgInvalidOperationException">Throws if user does not exist</exception>
        public async Task GenerateSecret(MimirorgGenerateSecretAm generateSecret)
        {
            var validation = generateSecret.ValidateObject();
            if (!validation.IsValid)
                throw new MimirorgBadRequestException("Couldn't generate secret", validation);

            var user = await _userManager.FindByEmailAsync(generateSecret.Email);
            if (user == null)
                throw new MimirorgInvalidOperationException($"Couldn't generate secret for user with email: {generateSecret.Email}");

            var secret = await CreateUserToken(user, new List<MimirorgTokenType> { generateSecret.TokenType });
            var email = await _templateRepository.CreateCodeVerificationMail(user, secret);
            await _emailRepository.SendEmail(email);
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

            if (user.PasswordHash != null)
                await _userManager.RemovePasswordAsync(user);
            
            var result = await _userManager.AddPasswordAsync(user, changePassword.Password);
            return result.Succeeded;
        }

        public IEnumerable<MimirorgUserCm> GetUsersNotConfirmed()
        {
            throw new NotImplementedException();
            //var users = _userManager.Users.Where(x => !x.EmailConfirmed && x.)
        }

        /// <summary>
        /// Verify email account from verify code
        /// </summary>
        /// <param name="data">The email verify data</param>
        /// <returns>bool</returns>
        /// <exception cref="MimirorgInvalidOperationException"></exception>
        /// <exception cref="MimirorgNotFoundException"></exception>
        public async Task<bool> VerifyAccount(MimirorgVerifyAm data)
        {
            var regToken = await _tokenRepository.FindBy(x => x.Secret == data.Code && x.Email == data.Email).FirstOrDefaultAsync(x => x.TokenType == MimirorgTokenType.VerifyEmail);

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

        private async Task<string> CreateUserToken(MimirorgUser user, IEnumerable<MimirorgTokenType> tokenTypes)
        {
            
            var generator = new Random();
            var secret = generator.Next(0, 1000000).ToString("D6");
            try
            {
                var deleteTokens = _tokenRepository.GetAll()
                    .Where(x => x.ClientId == user.Id || DateTime.Now > x.ValidTo).ToList();

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
                        ValidTo = DateTime.Now.AddHours(1)
                    };
                    _tokenRepository.Attach(token, EntityState.Added);
                }

                await _tokenRepository.SaveAsync();
            }
            catch (Exception e)
            {
                var test = e.Message;
            }

            return secret;
        }

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
                    Iris = new List<string> { "rdf.runir.net" },
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

        private async Task<ICollection<string>> ResolveRoles(ICollection<MimirorgCompanyCm> companies, ICollection<MimirorgPermissionCm> permissions, MimirorgUser user)
        {
            var roleDescriptionList = new List<string>();

            var roles = (await _userManager.GetRolesAsync(user)).ToList();
            if (roles.Any(x => x is "Administrator"))
            {
                roleDescriptionList.Add("Global administrator");
                return roleDescriptionList;
            }

            if (roles.Any(x => x is "Account Manager"))
            {
                roleDescriptionList.Add("Global account manager");
                return roleDescriptionList;
            }

            if (roles.Any(x => x is "Moderator"))
            {
                roleDescriptionList.Add("Global moderator");
                return roleDescriptionList;
            }

            if (!companies.Any())
                return roleDescriptionList;

            var claims = await _userManager.GetClaimsAsync(user);
            claims = claims.Where(x => companies.Any(y => x.Type == y.Id.ToString())).ToList();

            if (!claims.Any())
                return roleDescriptionList;

            foreach (var claim in claims)
            {
                var company = companies.FirstOrDefault(x => x.Id.ToString() == claim.Type);
                var permission = permissions.FirstOrDefault(x => x.Name == claim.Value);
                if (company != null && permission != null)
                    roleDescriptionList.Add($"{company.DisplayName ?? company.Name} {(MimirorgPermission) permission.Id}");
            }

            return roleDescriptionList;
        }

        private async Task<Dictionary<int, MimirorgPermission>> ResolveCompanies(ICollection<MimirorgCompanyCm> companies, ICollection<MimirorgPermissionCm> permissions, MimirorgUser user)
        {
            var companyList = new Dictionary<int, MimirorgPermission>();

            if (!companies.Any())
                return companyList;

            var claims = await _userManager.GetClaimsAsync(user);
            claims = claims.Where(x => companies.Any(y => x.Type == y.Id.ToString())).ToList();

            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            // Administrator or Account Manager role should give full permission to all companies
            if (roles.Any(x => x is "Administrator" or "Account Manager"))
            {
                foreach (var company in companies)
                {
                    companyList.Add(company.Id, MimirorgPermission.Manage);
                }

                return companyList;
            }

            // Moderator role should give delete permission to all companies
            if (roles.Any(x => x is "Moderator"))
            {
                foreach (var company in companies)
                {
                    companyList.Add(company.Id, MimirorgPermission.Delete);
                }

                return companyList;
            }

            foreach (var claim in claims)
            {
                var company = companies.FirstOrDefault(x => x.Id.ToString() == claim.Type);
                var permission = permissions.FirstOrDefault(x => x.Name == claim.Value);
                if (company != null && permission != null && companyList.All(x => x.Key != company.Id))
                    companyList.Add(company.Id, (MimirorgPermission) permission.Id);
            }

            return companyList;
        }

        #endregion
    }
}