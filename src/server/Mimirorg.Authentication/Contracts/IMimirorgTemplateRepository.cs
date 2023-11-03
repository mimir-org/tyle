using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Application;
using Mimirorg.Authentication.Models.Client;
using Mimirorg.Authentication.Models.Domain;
using Tyle.Core.Common;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgTemplateRepository
{
    Task<MimirorgMailAm> CreateCodeVerificationMail(MimirorgUser user, string secret);
    Task<MimirorgMailAm> CreateObjectStateEmail(MimirorgUserCm sendToUser, MimirorgUserCm fromUser, State state, string objectName, string objectTypeName);
    Task<MimirorgMailAm> CreateUserRegistrationEmail(MimirorgUserCm sendToUser, MimirorgUserCm fromUser);
}