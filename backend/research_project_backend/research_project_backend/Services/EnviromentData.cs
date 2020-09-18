using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using research_project_backend.Data;
using research_project_backend.Data.Domains;
using Environment = research_project_backend.Data.Domains.Environment;

namespace research_project_backend.Services
{
    public class EnviromentData : IEnviromentsData
    {
        private readonly MijnStagesDbContext _mijnStagesDbContext;

        public EnviromentData(MijnStagesDbContext mijnStagesDbContext)
        {
            _mijnStagesDbContext = mijnStagesDbContext;
        }

        public List<Environment> GetEnviromentsByAssignmentId(int id)
        {
           return _mijnStagesDbContext.Environment.ToList().Where(e => e.InternshipAssignmentId == id).ToList();
        }
    }   
}
