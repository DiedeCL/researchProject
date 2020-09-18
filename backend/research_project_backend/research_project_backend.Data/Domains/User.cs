using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;

namespace research_project_backend.Data.Domains
{
    public class User : IdentityUser<int>
    {
        public int? CompanyId { get; set; }


    }
}