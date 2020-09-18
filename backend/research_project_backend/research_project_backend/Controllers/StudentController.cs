using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using research_project_backend.Data.Domains;
using research_project_backend.Services;

namespace research_project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class StudentController : ControllerBase
    {
        private readonly IStudent _student;

        public StudentController(IStudent student)
        {
            _student = student;
        }

        [HttpGet("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            var students = _student.GetAllStudents();

            if (students == null) return BadRequest();

            var studentList = students.ToList().Select(MapStudentObject);
            
            return Ok(studentList);
        }

        [HttpPost("GetStudentById")]
        public IActionResult GetStudentById([FromBody] StudentInfoModel model)
        {
            var student = _student.GetStudentById(model.Id);

            if (student == null) return BadRequest();

            return Ok(MapStudentObject(student));
        }

        public ReturnStudentModel MapStudentObject(User user)
        {
            return Ok(_student.GetStudentById(id));
        }
    }
}