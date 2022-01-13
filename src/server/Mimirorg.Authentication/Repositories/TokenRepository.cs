using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mimirorg.Authentication.ApplicationModels;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models;
using Mimirorg.Common.Abstract;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;

namespace Mimirorg.Authentication.Repositories
{
    public class TokenRepository : GenericRepository<AuthenticationContext, RefreshToken>, ITokenRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<MimirorgUser> _userManager;
        private readonly AuthSettings _authSettings;

        public TokenRepository(AuthenticationContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<MimirorgUser> userManager, IOptions<AuthSettings> authSettings) : base(dbContext)
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
        public async Task<TokenAm> CreateAccessToken(MimirorgUser user, DateTime current)
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
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = current.AddMinutes(_authSettings.JwtExpireMinutes);

            var token = new JwtSecurityToken(
                _authSettings.JwtIssuer,
                _authSettings.JwtAudience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenAm
            {
                ClientId = user.Id,
                ValidTo = token.ValidTo,
                Secret = accessToken,
                TokenType = TokenType.AccessToken
            };
        }

        /// <summary>
        /// Create a new refresh token and delete all old tokens
        /// </summary>
        /// <param name="user"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        /// <exception cref="MimirorgConfigurationException"></exception>
        public async Task<TokenAm> CreateRefreshToken(MimirorgUser user, DateTime current)
        {
            if (_authSettings == null)
                throw new MimirorgConfigurationException("Missing configuration for auth settings");

            var expires = current.AddMinutes(_authSettings.JwtRefreshExpireMinutes);
            var refreshToken = ($"{Guid.NewGuid()}{user.Email}{Guid.NewGuid()}").CreateSha512();
            var token = new RefreshToken
            {
                ClientId = user.Id,
                Email = user.Email,
                Secret = refreshToken,
                ValidTo = expires
            };

            var oldTokens = FindBy(x => x.ClientId == user.Id, false).ToList();
            foreach (var oldToken in oldTokens)
            {
                Attach(oldToken, EntityState.Deleted);
            }

            Attach(token, EntityState.Added);
            await SaveAsync();
            return new TokenAm
            {
                ClientId = token.ClientId,
                Secret = token.Secret,
                TokenType = TokenType.RefreshToken,
                ValidTo = token.ValidTo
            };
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
