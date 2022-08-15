using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerBL.Services.SMTPService
{
    public class MailInfo
    {
        public string Email { get; set; }
        public string ClientName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
