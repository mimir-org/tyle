using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;

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

            var m = new MimeMessage();
            m.From.Add(MailboxAddress.Parse(email.FromEmail));
            m.To.Add(MailboxAddress.Parse(email.ToEmail));
            m.Subject = email.Subject;
            m.Body = new TextPart(TextFormat.Html) { Text = "<h1>Test Email</h1>" };

            try
            {
                var secrets = new ClientSecrets
                {
                    ClientId = "841546746384-bgjdkduv88cvrn8sfakia6bqrl9thl8f.apps.googleusercontent.com",
                    ClientSecret = "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"
                };

                var googleCredentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, new List<string> { "https://mail.google.com/" }, email.FromEmail, CancellationToken.None);
                if (googleCredentials.Token.IsExpired(SystemClock.Default))
                {
                    await googleCredentials.RefreshTokenAsync(CancellationToken.None);
                }

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    var oauth2 = new SaslMechanismOAuth2(googleCredentials.UserId, googleCredentials.Token.AccessToken);
                    await client.AuthenticateAsync(oauth2);

                    await client.SendAsync(m);
                    await client.DisconnectAsync(true);
                }

                

            }
            catch (Exception e)
            {
                var test = e.Message;
                throw;
            }

            //var client = new SendGridClient(_authSettings.EmailSecret);
            //var from = new EmailAddress(email.FromEmail, email.FromName);
            //var subject = email.Subject;
            //var to = new EmailAddress(email.ToEmail, email.ToName);
            //var plainTextContent = email.PlainTextContent;
            //var htmlContent = email.HtmlContent;
            //var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}