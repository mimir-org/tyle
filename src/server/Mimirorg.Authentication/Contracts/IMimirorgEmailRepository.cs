using MimeKit;

namespace Mimirorg.Authentication.Contracts
{
    public interface IMimirorgEmailRepository
    {
        Task SendEmail(MimeMessage email);
        Task SendMailServer(MimeMessage email);
    }
}