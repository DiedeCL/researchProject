using System.Linq;
using research_project_backend.Data;
using research_project_backend.Data.Domains;

namespace research_project_backend.Services
{
    public class SqlCompanyData : ICompany
    {
        private readonly MijnStagesDbContext _mijnStagesDbContext;

        public SqlCompanyData(MijnStagesDbContext mijnStagesDbContext)
        {
            _mijnStagesDbContext = mijnStagesDbContext;
        }
        public Company GetCompanyById(int id)
        {
            return _mijnStagesDbContext.Companies.FirstOrDefault(c => c.CompanyId == id);
        }
    }
}