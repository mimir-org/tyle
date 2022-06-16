using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgTemplateRepository
    {
        Task<MimirorgMail> CreateEmailConfirmationTemplate(MimirorgUser user, MimirorgToken token);
    }
}