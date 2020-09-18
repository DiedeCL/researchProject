using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using research_project_backend.Controllers;
using research_project_backend.Data.Domains;
using research_project_backend.Models;
using research_project_backend.Services;
using System.Collections.Generic;

namespace research_project_backend.Test.Controllers
{
    [TestFixture]
    public class StudentControllerTest
    {
        private readonly Mock<IStudentData> _studentDataMock;
        private readonly StudentController _studentController;

        public StudentControllerTest()
        {
            _studentDataMock = new Mock<IStudentData>();
            _studentController = new StudentController(_studentDataMock.Object);
        }

        [Test]
        public void GetAllStudentsShouldReturnAOkObjectResult()
        {
            var result = _studentController.GetAllStudents();
            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
        }

        [Test]
        public void GetAllStudents_ShouldReturnAllRegisteredStudents_WhenStudentsExist()
        {
            _studentDataMock.Setup(x => x.GetAllStudents())
                .Returns(GetTestStudents());

            var result = _studentController.GetAllStudents() as OkObjectResult;

            Assert.IsInstanceOf<IEnumerable<ReturnStudentModel>>(result.Value);
        }

        [Test]
        public void GetAllStudents_ShouldReturnABadRequest_WhenStudentListIsNull()
        {
            _studentDataMock.Setup(x => x.GetAllStudents())
                .Returns((List<User>)null);

            var result = _studentController.GetAllStudents() as BadRequestResult;

            Assert.AreEqual(result.GetType(), typeof(BadRequestResult));
        }

        [Test]
        public void GetStudentById_ShouldReturnAOkObjectResult_WhenStudentExists()
        {
            var result = GetTestStudentById(1);

            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
        }

        [Test]
        public void GetStudentById_ShouldReturnAStudent_WhenStudentExists()
        {
            var result = GetTestStudentById(1) as OkObjectResult;

            Assert.IsInstanceOf<ReturnStudentModel>(result.Value);
        }

        [Test]
        public void GetStudentById_ShouldReturnABadRequestResult_WhenNoStudentIdIsGiven()
        {
            var studentInfoModel = new StudentInfoModel
            {
                Id = 1
            };

            _studentDataMock.Setup(x => x.GetStudentById(It.IsAny<int>()))
                .Returns((User)null);

            var result = _studentController.GetStudentById(studentInfoModel);

            Assert.AreEqual(result.GetType(), typeof(BadRequestResult));
        }

        [Test]
        public void MapStudentObject_ShouldMapAStudentObjectToAReturnStudentModelObjectCorrectly()
        {
            var student = new User
            {
                AccessFailedCount = 0,
                CompanyId = 0,
                ConcurrencyStamp = "",
                Email = "11700104@student.pxl.be",
                EmailConfirmed = false,
                Id = 1,
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = "11700104@STUDENT.PXL.BE",
                NormalizedUserName = "",
            };

            var returnStudentModel = _studentController.MapStudentObject(student);

            Assert.IsInstanceOf<ReturnStudentModel>(returnStudentModel);
        }

        [Test]
        public void MapStudentObject_ShouldMapAStudentObjectToAReturnStudentModelObjectWithCorrectData()
        {
            var student = new User
            {
                AccessFailedCount = 0,
                CompanyId = 0,
                ConcurrencyStamp = "",
                Email = "11700104@student.pxl.be",
                EmailConfirmed = false,
                Id = 1,
                LockoutEnabled = false,
                LockoutEnd = null,
                NormalizedEmail = "11700104@STUDENT.PXL.BE",
                NormalizedUserName = "",
            };

            var returnStudentModel = _studentController.MapStudentObject(student);

            Assert.AreEqual(returnStudentModel.Id, student.Id);
            Assert.AreEqual(returnStudentModel.Email, student.Email);
            Assert.AreEqual(returnStudentModel.FirstName, student.FirstName);
            Assert.AreEqual(returnStudentModel.Name, student.Name);
            Assert.AreEqual(returnStudentModel.Specialization, student.Specialization);
            Assert.AreEqual(returnStudentModel.StudentNumber, student.StudentNumber);
        }

        private static IEnumerable<User> GetTestStudents()
        {
            return new List<User>
            {
                new User
                {
                    AccessFailedCount = 0,
                    CompanyId = 0,
                    ConcurrencyStamp = "",
                    Email = "11700104@student.pxl.be",
                    EmailConfirmed = false,
                    Id = 1,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    NormalizedEmail = "11700104@STUDENT.PXL.BE",
                    NormalizedUserName = "",
                },
                new User
                {
                    AccessFailedCount = 0,
                    CompanyId = 0,
                    ConcurrencyStamp = "",
                    Email = "11700104@student.pxl.be",
                    EmailConfirmed = false,
                    Id = 2,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    NormalizedEmail = "11700104@STUDENT.PXL.BE",
                    NormalizedUserName = "",
                },
                new User
                {
                    AccessFailedCount = 0,
                    CompanyId = 0,
                    ConcurrencyStamp = "",
                    Email = "11700104@student.pxl.be",
                    EmailConfirmed = false,
                    Id = 3,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    NormalizedEmail = "11700104@STUDENT.PXL.BE",
                    NormalizedUserName = "",
                }
            };
        }

        private IActionResult GetTestStudentById(int studentId)
        {
            var studentInfoModel = new StudentInfoModel
            {
                Id = studentId
            };

            _studentDataMock.Setup(x => x.GetStudentById(It.IsAny<int>()))
                .Returns(new User());

            return _studentController.GetStudentById(studentInfoModel);
        }
    }
}
