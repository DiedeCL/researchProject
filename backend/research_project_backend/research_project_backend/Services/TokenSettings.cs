using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace research_project_backend
{
    public class TokenSettings
    {
        public string Audience { get; set; }
        public int ExpirationTimeInMinutes { get; set; }
        public string Issuer { get; set; }
        public string Key { get; set; }
    }
}
