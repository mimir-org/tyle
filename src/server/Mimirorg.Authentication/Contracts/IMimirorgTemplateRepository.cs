using Mimirorg.Authentication.Models.Domain;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgTemplateRepository
{
    Task<MimirorgMailAm> CreateCodeVerificationMail(MimirorgUser user, string secret);
    Task<MimirorgMailAm> CreateObjectStateEmail(MimirorgUserCm sendToUser, MimirorgUserCm fromUser, State state, string objectName, string objectTypeName);
    Task<MimirorgMailAm> CreateUserRegistrationEmail(MimirorgUserCm sendToUser, MimirorgUserCm fromUser);
}