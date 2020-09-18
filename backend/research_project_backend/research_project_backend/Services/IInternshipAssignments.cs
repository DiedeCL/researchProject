using System.Collections.Generic;
using research_project_backend.Data.Domains;

namespace research_project_backend.Services
{
    public interface IInternshipAssignments
    {
        IEnumerable<InternshipAssignment> GetAllInternshipAssignments();
        IEnumerable<InternshipAssignment> GetAllInternshipAssignmentsByCompanyId(int id);

        void AddInternshipAssignment(InternshipAssignment internshipAssignment);

        InternshipAssignment GetInternshipAssignmentById(InternshipAssignment internshipAssignment);
    }
}