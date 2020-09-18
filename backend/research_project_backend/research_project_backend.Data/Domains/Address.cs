namespace research_project_backend.Data.Domains
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Township { get; set; }
        public string PostalNumber { get; set; }
        public int? CompanyId { get; set; }



    }
}