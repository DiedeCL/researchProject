using Microsoft.AspNetCore.Identity;

namespace research_project_backend.Data.Domains
{
    public class Role : IdentityRole<int>
    {
        public const string Student = "Student";
        public const string Teacher = "Teacher";
        public const string Company = "CompanyPromoter";
        public const string InternshipCoordinator = "Internshipcoordinator";

    }
}
