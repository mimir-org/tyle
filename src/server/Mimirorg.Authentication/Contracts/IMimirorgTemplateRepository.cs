using MimeKit;
using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgTemplateRepository
    {
        Task<MimeMessage> CreateCodeVerificationMail(MimirorgUser user, string secret);
    }
}