using Microsoft.EntityFrameworkCore;
using research_project_backend.Data;
using research_project_backend.Data.Domains;
using System.Collections.Generic;
using System.Linq;

namespace research_project_backend.Services
{
    public class SqlInternshipAssignmentsData : IInternshipAssignments
    {
        private readonly MijnStagesDbContext _mijnStagesDbContext;

        public SqlInternshipAssignmentsData(MijnStagesDbContext mijnStagesDbContext)
        {
            _mijnStagesDbContext = mijnStagesDbContext;
        }
        public IEnumerable<InternshipAssignment> GetAllInternshipAssignments()
        {
            var assignments = _mijnStagesDbContext.Assignments.Where(a => a.Id > 0)
                .Include(a => a.Company)
                .Include(a => a.Location)
                .ToList();
            return assignments;

        }

        public IEnumerable<InternshipAssignment> GetAllInternshipAssignmentsByCompanyId(int id)
        {
            var assignments = _mijnStagesDbContext.Assignments.Where(a => a.CompanyId == id)
                .Include(a => a.Company)
                .Include(a => a.Location)
                .ToList();
            return assignments;
        }

        public void AddInternshipAssignment(InternshipAssignment internshipAssignment)
        {
            _mijnStagesDbContext.Assignments.Add(internshipAssignment);
            _mijnStagesDbContext.Companies.FirstOrDefault(c => c.CompanyId == internshipAssignment.CompanyId)
                ?.InternshipAssignments.Add(internshipAssignment);
            _mijnStagesDbContext.SaveChanges();
        }

        public InternshipAssignment GetInternshipAssignmentById(InternshipAssignment internshipAssignment)
        {
            return _mijnStagesDbContext.Assignments.FirstOrDefault(a => a.Id == internshipAssignment.Id);
        }

    }
}
