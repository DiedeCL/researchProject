using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using research_project_backend.Data.Domains;
using Environment = research_project_backend.Data.Domains.Environment;

namespace research_project_backend.Services
{
    public interface IEnviromentsData
    {
        List<Environment> GetEnviromentsByAssignmentId(int id);
    }
}
