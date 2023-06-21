using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Repositories;

public class MimirorgTemplateRepository : IMimirorgTemplateRepository
{
    private readonly MimirorgAuthSettings _authSettings;

    public MimirorgTemplateRepository(IOptions<MimirorgAuthSettings> authSettings)
    {
        _authSettings = authSettings?.Value;
    }

    public Task<MimirorgMailAm> CreateCodeVerificationMail(MimirorgUser user, string secret)
    {
        if (_authSettings == null || string.IsNullOrEmpty(_authSettings.Email))
            throw new MimirorgConfigurationException("Missing configuration for email");

        if (user == null)
            return Task.FromResult(new MimirorgMailAm());

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

    public Task<MimirorgMailAm> CreateObjectStateEmail(MimirorgUserCm sendToUser, MimirorgUserCm fromUser, State state, string objectName, string objectTypeName)
    {
        if (_authSettings == null || string.IsNullOrEmpty(_authSettings.Email))
            throw new MimirorgConfigurationException("Missing configuration for email");

        if (sendToUser == null || fromUser == null)
            return Task.FromResult(new MimirorgMailAm());

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

    public Task<MimirorgMailAm> CreateUserRegistrationEmail(MimirorgUserCm sendToUser, MimirorgUserCm fromUser)
    {
        if (_authSettings == null || string.IsNullOrEmpty(_authSettings.Email))
            throw new MimirorgConfigurationException("Missing configuration for email");

        if (sendToUser == null || fromUser == null)
            return Task.FromResult(new MimirorgMailAm());

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

    public Task<MimirorgMailAm> CreateUserPermissionEmail(MimirorgUserCm sendToUser, MimirorgUserCm fromUser, MimirorgPermission permission, string companyName, bool isPermissionRemoval)
    {
        if (_authSettings == null || string.IsNullOrEmpty(_authSettings.Email))
            throw new MimirorgConfigurationException("Missing configuration for email");

        if (sendToUser == null || fromUser == null)
            return Task.FromResult(new MimirorgMailAm());

        var subject = isPermissionRemoval
            ? $@"Tyle {permission.ToString().ToLower()} permission for {companyName} removed"
            : $@"Tyle {permission.ToString().ToLower()} permission for {companyName} granted";

        var htmlContent = isPermissionRemoval
            ? $@"<div><h1>Tyle removed <i>{permission.ToString().ToLower()}</i> permission for <i>{companyName}</i></h1><p>Hi {sendToUser.FirstName} {sendToUser.LastName},</p><br /><br /><p>The user <i>{fromUser.FirstName} {fromUser.LastName}</i> with email <i>{fromUser.Email}</i> has removed your <i>{permission.ToString().ToLower()}</i> permission for <i>{companyName}</i>.</p></div>"
            : $@"<div><h1>Tyle granted <i>{permission.ToString().ToLower()}</i> permission for <i>{companyName}</i></h1><p>Hi {sendToUser.FirstName} {sendToUser.LastName},</p><br /><br /><p>The user <i>{fromUser.FirstName} {fromUser.LastName}</i> with email <i>{fromUser.Email}</i> has granted you <i>{permission.ToString().ToLower()}</i> permission for <i>{companyName}</i>.</p></div>";


        return Task.FromResult(new MimirorgMailAm
        {
            FromEmail = _authSettings.Email,
            FromName = _authSettings.ApplicationName,
            ToEmail = sendToUser.Email,
            ToName = $"{sendToUser.FirstName} {sendToUser.LastName}",
            Subject = subject,
            HtmlContent = htmlContent
        });
    }
}