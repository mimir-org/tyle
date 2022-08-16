using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mimirorg.Setup
{
    internal class IntegrationTestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public IntegrationTestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, "integration_user@runir.com"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, "integration_user@runir.com"),
                new Claim(JwtRegisteredClaimNames.GivenName, "Integration"),
                new Claim(JwtRegisteredClaimNames.FamilyName, "User"),
                new Claim(JwtRegisteredClaimNames.Email, "integration_user@runir.com"),
                new Claim(ClaimTypes.Role, "Administrator"),
            };
            var identity = new ClaimsIdentity(claims, "IntegrationUser");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "IntegrationUser");
            var result = AuthenticateResult.Success(ticket);
            return Task.FromResult(result);
        }
    }
}