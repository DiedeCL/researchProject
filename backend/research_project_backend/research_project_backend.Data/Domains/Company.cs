using System.Collections.Generic;

namespace research_project_backend.Data.Domains
{
    public class Company 
    {
        public Company()
        {
            InternshipAssignments= new List<InternshipAssignment>();
        }
        public int CompanyId { get; set; }
        public string NameCompany { get; set; }
        public Address AddressCompany { get; set; }
        public int AmountOfPersonnel { get; set; }
        public int AmountOfITPersonnel { get; set; }
        public ContactCompany ContactCompany { get; set; }
        public CompanyPromoter Promoter { get; set; }
        public User User { get; set; }

        public List<InternshipAssignment> InternshipAssignments { get; set; }
        
    }
}