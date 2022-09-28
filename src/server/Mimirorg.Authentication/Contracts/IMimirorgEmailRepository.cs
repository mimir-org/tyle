using MimeKit;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgEmailRepository
    {
        Task SendEmail(MimeMessage email);
    }
}