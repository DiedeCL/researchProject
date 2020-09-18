using research_project_backend.Data.Domains;

namespace research_project_backend.Services
{
    public interface ICompany
    {
        Company GetCompanyById(int id);
    }
}