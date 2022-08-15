using System.Threading.Tasks;

namespace BookLibraryManagerBL.Services.SMTPService
{
    public interface ISmtpService
    {
        Task SendMail(MailInfo mailInfo);
    }
}