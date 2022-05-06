using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Content;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Authentication.Models.Enums;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;

namespace Mimirorg.Authentication.Repositories
{
    public class MimirorgTokenRepository : GenericRepository<MimirorgAuthenticationContext, MimirorgToken>, IMimirorgTokenRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<MimirorgUser> _userManager;
        private readonly MimirorgAuthSettings _authSettings;

        public MimirorgTokenRepository(MimirorgAuthenticationContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<MimirorgUser> userManager, IOptions<MimirorgAuthSettings> authSettings) : base(dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _authSettings = authSettings?.Value;
        }

        /// <summary>
        /// Create a new access token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        /// <exception cref="MimirorgConfigurationException"></exception>
        public async Task<MimirorgTokenCm> CreateAccessToken(MimirorgUser user, DateTime current)
        {
            if (_authSettings == null)
                throw new MimirorgConfigurationException("Missing configuration for auth settings");

            var userClaims = await GetUserClaims(user);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }.Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var expires = current.AddMinutes(_authSettings.JwtExpireMinutes);

            var token = new JwtSecurityToken(
                _authSettings.JwtIssuer,
                _authSettings.JwtAudience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new MimirorgTokenCm
            {
                ClientId = user.Id,
                ValidTo = token.ValidTo,
                Secret = accessToken,
                TokenType = MimirorgTokenType.AccessToken
            };
        }

        /// <summary>
        /// Create a new refresh token and delete all old tokens
        /// </summary>
        /// <param name="user"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        /// <exception cref="MimirorgConfigurationException"></exception>
        public async Task<MimirorgTokenCm> CreateRefreshToken(MimirorgUser user, DateTime current)
        {
            if (_authSettings == null)
                throw new MimirorgConfigurationException("Missing configuration for auth settings");

            var expires = current.AddMinutes(_authSettings.JwtRefreshExpireMinutes);
            var refreshToken = ($"{Guid.NewGuid()}{user.Email}{Guid.NewGuid()}").CreateSha512();
            var token = new MimirorgToken
            {
                ClientId = user.Id,
                Email = user.Email,
                Secret = refreshToken,
                ValidTo = expires,
                TokenType = MimirorgTokenType.RefreshToken
            };

            var oldTokens = FindBy(x => x.ClientId == user.Id, false).Where(x => x.TokenType == MimirorgTokenType.RefreshToken).ToList();
            foreach (var oldToken in oldTokens)
            {
                Attach(oldToken, EntityState.Deleted);
            }

            await SaveAsync();

            Attach(token, EntityState.Added);
            await SaveAsync();

            return token.ToContentModel();
        }

        #region Private methods


        /// <summary>
        /// Get all claims for user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<List<Claim>> GetUserClaims(MimirorgUser user)
        {
            var allRoles = _roleManager.Roles.ToList();
            var userRoles = _userManager.GetRolesAsync(user).Result.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoleClaims = new List<Claim>();

            foreach (var role in userRoles)
            {
                var currentRole = allRoles.FirstOrDefault(x => x.Name.Equals(role.Value));
                if (currentRole == null)
                    continue;

                var claims = await _roleManager.GetClaimsAsync(currentRole);
                userRoleClaims.AddRange(claims);
            }

            return userRoles.Union(userClaims).Union(userRoleClaims).ToList();
        }

        #endregion


    }
}
