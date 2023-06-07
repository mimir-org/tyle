using Microsoft.Extensions.Options;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Models;
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

        string subject;
        string content;

        switch (state)
        {
            case State.Draft:
                return Task.FromResult(new MimirorgMailAm());

            case State.Approve:
                subject = $"Tyle {objectTypeName} approval request";
                content = $"User {fromUser.FirstName} {fromUser.LastName} with email {fromUser.Email} request approval for the {objectTypeName} {objectName}. This can be done in Tyle under 'Settings' and 'Approval'.";
                break;

            case State.Approved:
                subject = $"Tyle {objectTypeName} is approved";
                content = $"The {objectTypeName} {objectName} is approved.)";
                break;

            case State.Delete:
                subject = $"Tyle {objectTypeName} delete request";
                content = $"User {fromUser.FirstName} {fromUser.LastName} with email {fromUser.Email} request delete for the {objectTypeName} {objectName}. This can be done in Tyle under 'Settings' and 'Approval'.";
                break;

            case State.Deleted:
                subject = $"Tyle {objectTypeName} is deleted";
                content = $"The {objectTypeName} {objectName} is deleted.)";
                break;

            default:
                throw new ArgumentOutOfRangeException($"Switch with state '{state}' not found");
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

    public Task<MimirorgMailAm> CreateUserRegistrationEmail(MimirorgUserCm sendToUser, MimirorgUserCm newUser)
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
            HtmlContent = $@"<div><h1>Tyle has a new user</h1><p>Hi {sendToUser.FirstName} {sendToUser.LastName},</p><br /><br /><p>The user {newUser.FirstName} {newUser.LastName} with email {newUser.Email} just created an account.</p><p>The user needs an appropriate access level. This can be set in Tyle under 'Settings' and 'Access'.</p></div>"
        });
    }
}