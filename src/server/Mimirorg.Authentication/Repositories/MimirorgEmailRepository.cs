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
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = !string.IsNullOrWhiteSpace(environment) && environment.ToLower() == "development";
            if (isDevelopment)
            {
                await SendToLocalFolder(email);
                return;
            }

            await SendMailServer(email);
        }

        public async Task SendMailServer(MimeMessage email)
        {
            using var client = new SmtpClient();
            await client.ConnectAsync("localhost", 25);
            await client.SendAsync(email);
        }

        private static async Task SendToLocalFolder(MimeMessage email)
        {
            var executingAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var rootPath = $@"{Path.GetDirectoryName(executingAssembly)}/{Guid.NewGuid()}";
            await email.WriteToAsync(rootPath);
        }
    }
}