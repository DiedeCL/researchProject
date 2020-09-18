using System.Collections.Generic;
using research_project_backend.Data.Domains;

namespace research_project_backend.Services
{
    public interface IStudent
    {
        IEnumerable<User> GetAllStudents();
        User GetStudentById(int id);

    }
}