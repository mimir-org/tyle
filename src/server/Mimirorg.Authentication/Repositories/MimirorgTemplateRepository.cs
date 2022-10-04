using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Extensions;
using Mimirorg.Common.Models;

namespace Mimirorg.Authentication.Repositories
{
    public class MimirorgTemplateRepository : IMimirorgTemplateRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MimirorgAuthSettings _authSettings;

        public MimirorgTemplateRepository(IHttpContextAccessor httpContextAccessor, IOptions<MimirorgAuthSettings> authSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _authSettings = authSettings?.Value;
        }

        public Task<MimirorgMail> CreateEmailConfirmationTemplate(MimirorgUser user, MimirorgToken token)
        {
            var appBaseUrl = string.IsNullOrWhiteSpace(_authSettings?.ApplicationUrl) ? _httpContextAccessor.GetBaseUrl() : _authSettings?.ApplicationUrl;
            var url = $"{appBaseUrl}/v1/mimirorgauthenticate/verify/{Uri.EscapeDataString(token.Secret)}";

            var email = new MimirorgMail
            {
                Subject = "You need to confirm your email address",
                ToEmail = user.Email,
                ToName = $"{user.FirstName} {user.LastName}",
                FromEmail = "orgmimir@gmail.com",
                // ReSharper disable once StringLiteralTypo
                FromName = "Tyle",
                HtmlContent = null,
                PlainTextContent = $"Email activation link:\n\n\n {url}"
            };

            return Task.FromResult(email);
        }
    }
}