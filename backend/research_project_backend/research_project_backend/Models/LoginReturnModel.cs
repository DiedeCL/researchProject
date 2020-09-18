using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace research_project_backend.Models
{
    public class LoginReturnModel
    {
        public string Email { get; set; }
        public IList<string> Role { get; set; }
        public string Token { get; set; }
        public int? CompanyId { get; set; }
    }
}
