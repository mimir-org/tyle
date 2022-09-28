using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Gmail.v1;
using Google.Apis.Util.Store;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;

namespace Mimirorg.Authentication.Repositories
{
    public class MimirorgEmailRepository : IMimirorgEmailRepository
    {
        private readonly MimirorgAuthSettings _authSettings;

        public MimirorgEmailRepository(IOptions<MimirorgAuthSettings> authSettings)
        {
            _authSettings = authSettings?.Value;
        }

        public async Task SendEmail(MimeMessage email)
        {
            if (_authSettings == null || string.IsNullOrEmpty(_authSettings.EmailKey) || string.IsNullOrEmpty(_authSettings.EmailSecret) || string.IsNullOrEmpty(_authSettings.Email))
                throw new MimirorgConfigurationException("Missing configuration for email");

            var codeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                DataStore = new FileDataStore(AppDomain.CurrentDomain.BaseDirectory),
                Scopes = new[] { GmailService.Scope.MailGoogleCom },
                ClientSecrets = new ClientSecrets
                {
                    ClientId = _authSettings.EmailKey,
                    ClientSecret = _authSettings.EmailSecret
                }
            });

            var codeReceiver = new LocalServerCodeReceiver();
            var authCode = new AuthorizationCodeInstalledApp(codeFlow, codeReceiver);
            var credential = await authCode.AuthorizeAsync(_authSettings.Email, CancellationToken.None);

            if (authCode.ShouldRequestAuthorizationCode(credential.Token))
            {
                await credential.RefreshTokenAsync(CancellationToken.None);
            }

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

            var oauth2 = new SaslMechanismOAuth2(credential.UserId, credential.Token.AccessToken);
            await client.AuthenticateAsync(oauth2);

            await client.SendAsync(email);
            await client.DisconnectAsync(true);
        }
    }
}
