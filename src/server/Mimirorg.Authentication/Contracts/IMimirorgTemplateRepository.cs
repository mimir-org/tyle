using Mimirorg.Authentication.Models.Domain;
using Mimirorg.TypeLibrary.Models.Application;

namespace Mimirorg.Authentication.Contracts;

public interface IMimirorgTemplateRepository
{
    Task<MimirorgMailAm> CreateCodeVerificationMail(MimirorgUser user, string secret);
}