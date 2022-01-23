using Microsoft.AspNetCore.Http;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Extensions;

namespace Mimirorg.Authentication.Repositories
{
    public class MimirorgTemplateRepository : IMimirorgTemplateRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MimirorgTemplateRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<MimirorgMail> CreateEmailConfirmationTemplate(MimirorgUser user, MimirorgToken token)
        {
            var appBaseUrl = _httpContextAccessor.GetBaseUrl();
            var url = $"{appBaseUrl}/v1/mimirorgauthenticate/verify/{Uri.EscapeDataString(token.Secret)}";

            var email = new MimirorgMail
            {
                Subject = "You need to confirm your email address",
                ToEmail = user.Email,
                ToName = $"{user.FirstName} {user.LastName}",
                FromEmail = "orgmimir@gmail.com",
                FromName = "Mimirorg Type Library",
                HtmlContent = null,
                PlainTextContent = $"Email activation link:\n\n\n {url}"
            };

            return Task.FromResult(email);
        }
    }
}
