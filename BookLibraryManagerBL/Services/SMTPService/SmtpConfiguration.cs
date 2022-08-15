using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerBL.Services.SMTPService
{
    public class SmtpConfiguration
    {
        public string SenderMail { get; set; }
        public string SenderPassword { get; set; }
        public string SenderName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
