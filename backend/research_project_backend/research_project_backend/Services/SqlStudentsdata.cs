using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using research_project_backend.Data;
using research_project_backend.Data.Domains;

namespace research_project_backend.Services
{
    public class SqlStudentsData : IStudent
    {
        private readonly MijnStagesDbContext _mijnStagesDbContext;
        private readonly UserManager<User> _userManager;

        public SqlStudentsData(MijnStagesDbContext mijnStagesDbContext, UserManager<User> userManager)
        {
            _mijnStagesDbContext = mijnStagesDbContext;
            _userManager = userManager;
        }

        public IEnumerable<User> GetAllStudents()
        {
            return _mijnStagesDbContext.Users.ToList().Where(u => _userManager.GetRolesAsync(u).Result.FirstOrDefault().Equals(Role.Student)).ToList();
        }

        public User GetStudentById(int id)
        {
            return _mijnStagesDbContext.Users.FirstOrDefault(s => s.Id == id);
        }
    }
}