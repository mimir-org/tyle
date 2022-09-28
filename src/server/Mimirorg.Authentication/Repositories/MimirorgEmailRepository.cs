using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Mimirorg.Authentication.Repositories
{
    public class MimirorgEmailRepository : IMimirorgEmailRepository
    {
        private readonly MimirorgAuthSettings _authSettings;
        private readonly ILogger<MimirorgEmailRepository> _logger;

        public MimirorgEmailRepository(IOptions<MimirorgAuthSettings> authSettings, ILogger<MimirorgEmailRepository> logger)
        {
            _logger = logger;
            _authSettings = authSettings?.Value;
        }

        public async Task SendEmail(MimirorgMail email)
        {
            if (_authSettings == null || string.IsNullOrEmpty(_authSettings.EmailKey) || string.IsNullOrEmpty(_authSettings.EmailSecret))
                throw new MimirorgConfigurationException("Missing configuration for email");

            var client = new SendGridClient(_authSettings.EmailSecret);
            var from = new EmailAddress(email.FromEmail, email.FromName);
            var subject = email.Subject;
            var to = new EmailAddress(email.ToEmail, email.ToName);
            var plainTextContent = email.PlainTextContent;
            var htmlContent = email.HtmlContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}