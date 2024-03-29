using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Authentication.Models;
using Mimirorg.Authentication.Models.Application;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Mimirorg.Authentication.Repositories;

public class SendGridRepository : IMimirorgEmailRepository
{
    private readonly MimirorgAuthSettings _authSettings;

    public SendGridRepository(IOptions<MimirorgAuthSettings> authSettings)
    {
        _authSettings = authSettings?.Value;
    }

    public async Task SendEmail(MimirorgMailAm email)
    {
        if (string.IsNullOrWhiteSpace(_authSettings?.EmailSecret))
            throw new MimirorgConfigurationException("Missing configuration for email settings.");

        if (string.IsNullOrWhiteSpace(email?.FromEmail) || string.IsNullOrWhiteSpace(email.ToEmail))
            return;

        var client = new SendGridClient(_authSettings.EmailSecret);
        var from = new EmailAddress(email.FromEmail, email.FromName);
        var to = new EmailAddress(email.ToEmail, email.ToName);
        var msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.PlainTextContent, email.HtmlContent);
        _ = await client.SendEmailAsync(msg);
    }
}