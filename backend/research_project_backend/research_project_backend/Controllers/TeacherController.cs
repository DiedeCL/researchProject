using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using research_project_backend.Models;
using research_project_backend.Services;
using research_project_backend.Services.Interfaces;
using System.Linq;
using research_project_backend.Data.Domains;

namespace research_project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherData _teacherData;
        private readonly IInternshipAssignments _internshipAssignments;

        public TeacherController(ITeacherData teacherData, IInternshipAssignments internshipAssignments)
        {
            _teacherData = teacherData;
            _internshipAssignments = internshipAssignments;
        }

        [HttpPost("GetAllTeachersWithAssignedAssignment")]
        public IActionResult GetAllTeachersWithAssignedAssignment([FromBody] TeacherAssignmentModel model)
        {
            var assignedTeachers = _internshipAssignments.GetAllAssignedTeachersByAssigmentId(model.assignmentId);

            if (assignedTeachers == null) return BadRequest();

            var teachers = _teacherData.GetAllTeachers().ToList().Select(teacher => new ReturnAssignedTeacherData
            {
                Id = teacher.Id,
                Email = teacher.Email,
                EmployeeId = teacher.TeacherNumber,
                FirstName = teacher.FirstName,
                Name = teacher.Name,
                IsAssinged = assignedTeachers.Contains(teacher)
            });

            return Ok(teachers);
        }
    }
}
