using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;

namespace Mimirorg.Authentication.Repositories
{
    public class MimirorgTemplateRepository : IMimirorgTemplateRepository
    {
        private readonly MimirorgAuthSettings _authSettings;

        public MimirorgTemplateRepository(IOptions<MimirorgAuthSettings> authSettings)
        {
            _authSettings = authSettings?.Value;
        }

        public Task<MimeMessage> CreateCodeVerificationMail(MimirorgUser user, string secret)
        {
            if (_authSettings == null || string.IsNullOrEmpty(_authSettings.EmailKey) || string.IsNullOrEmpty(_authSettings.EmailSecret) || string.IsNullOrEmpty(_authSettings.Email))
                throw new MimirorgConfigurationException("Missing configuration for email");

            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_authSettings.Email));
            mail.To.Add(MailboxAddress.Parse(user.Email));
            mail.Subject = "Your verification code";
            mail.Body = new TextPart(TextFormat.Html)
            {
                Text = $@"
                <div>
                    <h1>Your verification code</h1>
                    <p>Hi {user.FirstName} {user.LastName},</p>
                    <br /><br />
                    <p>Your code: {secret}</p>                    
                </div>
                "
            };

            return Task.FromResult(mail);
        }
    }
}