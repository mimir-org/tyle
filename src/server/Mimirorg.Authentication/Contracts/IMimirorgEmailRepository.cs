using Mimirorg.Authentication.Models.Domain;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgEmailRepository
    {
        Task SendEmail(MimirorgMail email);
    }
}