namespace research_project_backend.Data.Domains
{
    public class ContactCompany
    {
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        
    }
}