using System;
using System.Collections.Generic;
using System.Text;

namespace BookLibraryManagerBL.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int LifetimeInSeconds { get; set; }
        public string Salt { get; set; }
    }

}
