using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Models.Application;

namespace Mimirorg.Authentication.Repositories
{
    public class MimirorgTemplateRepository : IMimirorgTemplateRepository
    {
        private readonly MimirorgAuthSettings _authSettings;

        public MimirorgTemplateRepository(IOptions<MimirorgAuthSettings> authSettings)
        {
            _authSettings = authSettings?.Value;
        }

        public Task<MimirorgMailAm> CreateCodeVerificationMail(MimirorgUser user, string secret)
        {
            if (_authSettings == null || string.IsNullOrEmpty(_authSettings.EmailKey) || string.IsNullOrEmpty(_authSettings.EmailSecret) || string.IsNullOrEmpty(_authSettings.Email))
                throw new MimirorgConfigurationException("Missing configuration for email");

            var mail = new MimirorgMailAm
            {
                FromEmail = _authSettings.Email,
                FromName = _authSettings.ApplicationName,
                ToEmail = user.Email,
                ToName = $"{user.FirstName} {user.LastName}",
                Subject = "Your verification code",
                HtmlContent = $@"<div><h1>Your verification code</h1><p>Hi {user.FirstName} {user.LastName},</p><br /><br /><p>Your code: {secret}</p></div>"
            };

            return Task.FromResult(mail);
        }
    }
}