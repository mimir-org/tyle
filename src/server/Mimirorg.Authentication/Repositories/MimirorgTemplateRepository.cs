using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Exceptions;
using Mimirorg.Authentication.Models;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Client;
using Mimirorg.Authentication.Models.Domain;
using Tyle.Core.Common;

namespace Mimirorg.Authentication.Repositories;

public class MimirorgTemplateRepository : IMimirorgTemplateRepository
{
    private readonly MimirorgAuthSettings _authSettings;

    public MimirorgTemplateRepository(IOptions<MimirorgAuthSettings> authSettings)
    {
        _authSettings = authSettings.Value;
    }

    public Task<MimirorgMailAm> CreateCodeVerificationMail(MimirorgUser user, string secret)
    {
        if (_authSettings == null || string.IsNullOrEmpty(_authSettings.Email))
            throw new MimirorgConfigurationException("Missing configuration for email");

        var mail = new MimirorgMailAm
        {
            FromEmail = _authSettings.Email,
            FromName = _authSettings.ApplicationName,
            ToEmail = user.Email,
            ToName = $"{user.FirstName} {user.LastName}",
            Subject = "Tyle registration",
            HtmlContent = $@"<div><h1>Tyle registration</h1><p>Hi {user.FirstName} {user.LastName},</p><br /><br /><p>Here is your verification code: {secret}</p></div>"
        };

        return Task.FromResult(mail);
    }

    public Task<MimirorgMailAm> CreateObjectStateEmail(UserView sendToUser, UserView fromUser, State state, string objectName, string objectTypeName)
    {
        if (_authSettings == null || string.IsNullOrEmpty(_authSettings.Email))
            throw new MimirorgConfigurationException("Missing configuration for email");

        string subject;
        string content;
        const string tyleSettings = "This can be done in Tyle under <i>Settings</i> and <i>Approval</i>.";

        switch (state)
        {
            case State.Draft:
                subject = $"Tyle {objectTypeName} is rejected";
                content = $"The {objectTypeName} <i>{objectName}</i> is rejected and reverted to draft by <i>{fromUser.FirstName} {fromUser.LastName}</i> with email <i>{fromUser.Email}</i>";
                break;

            case State.Review:
                subject = $"Tyle {objectTypeName} approval request";
                content = $"User <i>{fromUser.FirstName} {fromUser.LastName}</i> with email <i>{fromUser.Email}</i> requests approval for the {objectTypeName} <i>{objectName}</i>. {tyleSettings} ";
                break;

            case State.Approved:
                subject = $"Tyle {objectTypeName} is approved";
                content = $"The {objectTypeName} <i>{objectName}</i> is approved by <i>{fromUser.FirstName} {fromUser.LastName}</i> with email <i>{fromUser.Email}</i>";
                break;

            default:
                throw new ArgumentOutOfRangeException($"'CreateObjectStateEmail' switch with state '{state}' not found");
        }

        return Task.FromResult(new MimirorgMailAm
        {
            FromEmail = _authSettings.Email,
            FromName = _authSettings.ApplicationName,
            ToEmail = sendToUser.Email,
            ToName = $"{sendToUser.FirstName} {sendToUser.LastName}",
            Subject = $@"{subject}",
            HtmlContent = $@"<div><h1>{subject}</h1><p>Hi {sendToUser.FirstName} {sendToUser.LastName},</p><br /><br /><p>{content}</p></div>"
        });
    }

    public Task<MimirorgMailAm> CreateUserRegistrationEmail(UserView sendToUser, UserView fromUser)
    {
        if (_authSettings == null || string.IsNullOrEmpty(_authSettings.Email))
            throw new MimirorgConfigurationException("Missing configuration for email");

        return Task.FromResult(new MimirorgMailAm
        {
            FromEmail = _authSettings.Email,
            FromName = _authSettings.ApplicationName,
            ToEmail = sendToUser.Email,
            ToName = $"{sendToUser.FirstName} {sendToUser.LastName}",
            Subject = $@"Tyle has a new user",
            HtmlContent = $@"<div><h1>Tyle has a new user</h1><p>Hi {sendToUser.FirstName} {sendToUser.LastName},</p><br /><br /><p>The user <i>{fromUser.FirstName} {fromUser.LastName}</i> with email <i>{fromUser.Email}</i> just created an account.</p><p>The user needs an appropriate access level. This can be set in Tyle under <i>Settings</i> and <i>Access</i>.</p></div>"
        });
    }
}