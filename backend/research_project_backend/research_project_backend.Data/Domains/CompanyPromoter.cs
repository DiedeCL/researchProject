using Microsoft.AspNetCore.Identity;

namespace research_project_backend.Data.Domains
{
    public class CompanyPromoter 
    {
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public string JobTitle { get; set; }
        public int Id { get; set; }


    }
}