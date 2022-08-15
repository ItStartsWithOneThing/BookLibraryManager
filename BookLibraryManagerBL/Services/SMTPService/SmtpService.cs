using EASendMail;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryManagerBL.Services.SMTPService
{
    public class SmtpService : ISmtpService
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public SmtpService(IOptions<SmtpConfiguration> options)
        {
            _smtpConfiguration = options.Value;
        }

        public async Task SendMail(MailInfo mailInfo)
        {

            using (SmtpClient smtpClient = new SmtpClient())
            {
                SmtpMail Mail = new SmtpMail("TryIt");

                Mail.From = new MailAddress(_smtpConfiguration.SenderName, _smtpConfiguration.SenderMail);
                Mail.To.Add(new MailAddress(mailInfo.ClientName, mailInfo.Email));
                Mail.Subject = mailInfo.Subject;
                Mail.TextBody = mailInfo.Body;


                SmtpServer Server = new SmtpServer(_smtpConfiguration.Host);

                Server.User = _smtpConfiguration.SenderMail;
                Server.Password = _smtpConfiguration.SenderPassword;
                Server.Port = _smtpConfiguration.Port;

                Server.ConnectType = SmtpConnectType.ConnectSSLAuto;

                await smtpClient.SendMailAsync(Server, Mail);
            }
        }
    }
}
