using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using research_project_backend.Controllers;
using research_project_backend.Data.Domains;
using research_project_backend.Models;
using research_project_backend.Services;
using Moq;
using research_project_backend.Data.Domains.Enums;
using Environment = research_project_backend.Data.Domains.Environment;
using research_project_backend.Services.Interfaces;
using System.Threading.Tasks;

namespace research_project_backend.Test.Controllers
{
    [TestFixture]
    class AssignmentsControllerTest
    {
        private readonly Mock<IInternshipAssignments> _internshipAssignmentMock;
        private readonly Mock<IEnviromentsData> _environmentsDataMock;
        private readonly Mock<IIntroductionConditionData> _introductionConditionDataMock;
        private readonly Mock<ICompanyData> _companyDataMock;
        private readonly Mock<IAddressData> _addressDataMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private User user;
        private bool hasAssignments;
        private readonly AssignmentsController _assignmentsController;
        private InternshipAssignment assignment;
        private AssignedTeachers assigned;
        private User teacher;
        private User unassignedTeacher;
        private Address address;

        public AssignmentsControllerTest()
        {
            var store = new Mock<IUserStore<User>>();
            _internshipAssignmentMock = new Mock<IInternshipAssignments>();
            _addressDataMock = new Mock<IAddressData>();
            _companyDataMock = new Mock<ICompanyData>();
            _introductionConditionDataMock = new Mock<IIntroductionConditionData>();
            _environmentsDataMock = new Mock<IEnviromentsData>();
            _userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            _assignmentsController = new AssignmentsController(_internshipAssignmentMock.Object, _environmentsDataMock.Object, _introductionConditionDataMock.Object, _companyDataMock.Object, _userManagerMock.Object, _addressDataMock.Object);
        }

        [SetUp]
        public void Init()
        {
            user = new User
            {
                Email = "test@pxl.be",
                UserName = "test@pxl.be",
                PasswordHash = "Test1234"
            };
            teacher = new User
            {
                Id = 0,
                Email = "lector@pxl.be",
                UserName = "lector@pxl.be",
                PasswordHash = "Test1234"
            };
            unassignedTeacher = new User
            {
                Email = "unassigned@pxl.be"
            };

            assigned = new AssignedTeachers
            {
                ReviewMessage = "iets",
                Status = TeacherStatus.Behandeling,
                Teacher = teacher
            };

            assignment = new InternshipAssignment
            {
                Id = 0,
                Status = Status.Verzonden,
                TeacherStatus = TeacherStatus.GeenLector
            };
        }

        [Test]
        public void GetAllAssignmentsShouldReturnOkObjectResult()
        {
            _internshipAssignmentMock.Setup(_ia => _ia.GetAllInternshipAssignments())
                .Returns(GetAllAssignmentList);

            var assignments = _assignmentsController.GetAllInternshipAssignments();
            Assert.NotNull(assignments);
            Assert.AreEqual(assignments.GetType(), typeof(OkObjectResult));
        }

        [Test]
        public void GetAllAssignmentsShouldReturnAListOfAssignmentReturnModels()
        {
            _internshipAssignmentMock.Setup(_ia => _ia.GetAllInternshipAssignments())
                .Returns(GetAllAssignmentList);
            var assignments = _assignmentsController.GetAllInternshipAssignments();
            Assert.NotNull(assignments);
            var result = assignments as OkObjectResult;
            Assert.IsInstanceOf<IEnumerable<ReturnAssignmentModel>>(result.Value);
        }

        [Test]
        [TestCase(1)]
        public void GetAllAssignmentsByCompanyIdShouldReturnOkObjectResult(int companyId)
        {
            _internshipAssignmentMock.Setup(_ia => _ia.GetAllInternshipAssignmentsByCompanyId(It.IsAny<int>()))
                .Returns(GetAllAssignmentListByCompanyId);

            var assignmentModel = new AssignmentsModel
            {
                CompanyId = companyId,
                Id = 1
            };

            var assignments = _assignmentsController.GetAllInternshipAssignmentsByCompanyId(assignmentModel);
            Assert.NotNull(assignments);
            Assert.AreEqual(assignments.GetType(), typeof(OkObjectResult));
        }

        [Test]
        [TestCase(1)]
        public void GetAllAssignmentsByCompanyIdShouldReturnAListOfAssignmentReturnModels(int companyId)
        {
            _internshipAssignmentMock.Setup(_ia => _ia.GetAllInternshipAssignmentsByCompanyId(It.IsAny<int>()))
                .Returns(GetAllAssignmentListByCompanyId);

            var assignmentModel = new AssignmentsModel
            {
                CompanyId = companyId,
                Id = 1
            };

            var assignments = _assignmentsController.GetAllInternshipAssignmentsByCompanyId(assignmentModel);
            Assert.NotNull(assignments);
            var result = assignments as OkObjectResult;
            Assert.IsInstanceOf<IEnumerable<ReturnAssignmentModel>>(result.Value);
        }

        [Test]
        [TestCase("ApplicationDevelopment")]
        public void GetAllAssignmentsBySpecializationShouldReturnOkObject(string specialization)
        {
            _internshipAssignmentMock.Setup(_ia => _ia.GetApprovedInternshipAssingmentBySpecialization(It.IsAny<Specialization>()))
                .Returns(GetAllAssignmentListBySpecialization);

            var specializationModel = new SpecializationModel
            {
                Specialization = specialization
            };

            var assignments = _assignmentsController.GetApprovedInternshipAssingmentBySpecialization(specializationModel);
            Assert.NotNull(assignments);
            Assert.AreEqual(assignments.GetType(), typeof(OkObjectResult));
        }

        [Test]
        [TestCase("ApplicationDevelopment")]
        public void GetAllAssignmentsBySpecializationShouldReturnAListOfReturnAssignmentModels(string specialization)
        {
            _internshipAssignmentMock.Setup(_ia => _ia.GetApprovedInternshipAssingmentBySpecialization(It.IsAny<Specialization>()))
                .Returns(GetAllAssignmentListBySpecialization);

            var specializationModel = new SpecializationModel
            {
                Specialization = specialization
            };

            var assignments = _assignmentsController.GetApprovedInternshipAssingmentBySpecialization(specializationModel);
            Assert.NotNull(assignments);
            var result = assignments as OkObjectResult;
            Assert.IsInstanceOf<IEnumerable<ReturnAssignmentModel>>(result.Value);
        }

        [Test]
        [TestCase(8)]
        public void GetAssignmentByIdShouldReturnOkResultWhenAssignmentExists(int id)
        {
            var result = GetAssignmentById(id);

            Assert.AreEqual(result.GetType(), typeof(OkObjectResult));
        }

        [Test]
        public async Task GetAllAssignedAssingmentsReturns200WhenEmailExists()
        {
            this.hasAssignments = true;
            SetUpMocksForGetAllAssignedAssingments();

            var result = await _assignmentsController.GetAllAssignedAssingments(GetAssignedTeacherModel(this.user.Email)) as OkObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetAllAssignedAssingmentsReturnsAssignmentListWhenEmailExists()
        {
            this.hasAssignments = true;
            SetUpMocksForGetAllAssignedAssingments();

            var result = await _assignmentsController.GetAllAssignedAssingments(GetAssignedTeacherModel(this.user.Email)) as OkObjectResult;

            Assert.NotNull(result.Value);
        }

        [Test]
        public async Task GetAllAssignedAssingmentsReturns401WhenDoesNotEmailExist()
        {
            this.hasAssignments = true;
            SetUpMocksForGetAllAssignedAssingments();
            var result = await _assignmentsController.GetAllAssignedAssingments(GetAssignedTeacherModel("wrong@email.com")) as UnauthorizedResult;

            Assert.That(result.StatusCode, Is.EqualTo(401));
        }
        [Test]
        public async Task GetAllAssignedAssingmentsReturnsEmptyListWhenNoAssignmentsAssigned()
        {
            this.hasAssignments = false;
            SetUpMocksForGetAllAssignedAssingments();

            var result = await _assignmentsController.GetAllAssignedAssingments(GetAssignedTeacherModel(this.user.Email)) as OkObjectResult;

            Assert.That(result.Value, Is.Empty);
        }


        [Test]
        [TestCase("Afgekeurd", Status.Afgekeurd, TeacherStatus.Afgekeurd)]
        [TestCase("Goedgekeurd", Status.Goedgekeurd, TeacherStatus.Goedgekeurd)]
        public void UpdateStatusChangesStatusWhenEverythingIsCorrect(string status, Status expected, TeacherStatus expectedTeacher)
        {
            SetUpMockForUpdateStatusSystem();
            string message = "bla";
            var model = GetUpdateStatusModel(0, status, message);
            _assignmentsController.UpdateStatus(model);

            Assert.That(assignment.Status, Is.EqualTo(expected));
            Assert.That(assignment.TeacherStatus, Is.EqualTo(expectedTeacher));
            Assert.That(assignment.ReviewMessage, Is.EqualTo(message));
            Assert.That(assigned.Teacher, Is.Null);
        }

        [Test]
        public void UpdateStatusReturns200WhenEverythingIsCorrect()
        {
            SetUpMockForUpdateStatusSystem();
            string message = "bla";
            var model = GetUpdateStatusModel(0, "Goedgekeurd", message);
            var result = _assignmentsController.UpdateStatus(model) as OkResult;

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void UpdateStatusReturns400WhenStatusIsNotFound()
        {
            SetUpMockForUpdateStatusSystem();
            string message = "bla";
            var model = GetUpdateStatusModel(0, "een random string", message);
            var result = _assignmentsController.UpdateStatus(model) as BadRequestResult;

            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task UpdateTeacherStatusReturns200WhenEverythingIsCorrect()
        {
            SetUpMockForUpdateStatusSystem();
            var model = GetUpdateTeacherStatusModel(teacher.Email, 0, "Goedgekeurd", "message");

            var result = await _assignmentsController.UpdateTeacherStatus(model) as OkResult;

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateTeacherStatusUpdatesMainStatusToTeacherStatusWhenOnly1TeacherAssigned()
        {
            SetUpMockForUpdateStatusSystem();
            var model = GetUpdateTeacherStatusModel(teacher.Email, 0, "Goedgekeurd", "message");

            await _assignmentsController.UpdateTeacherStatus(model);

            Assert.That(assigned.Status, Is.EqualTo(TeacherStatus.Goedgekeurd));
            Assert.That(assignment.TeacherStatus, Is.EqualTo(TeacherStatus.Goedgekeurd));
        }

        [Test]
        public async Task UpdateTeacherStatusUpdatesMainStatusToCorrectValueWhenMultipleTeachersAssigned()
        {
            SetUpMockForUpdateStatusSystem();
            var model = GetUpdateTeacherStatusModel(teacher.Email, 1, "Goedgekeurd", "message");

            await _assignmentsController.UpdateTeacherStatus(model);

            Assert.That(assigned.Status, Is.EqualTo(TeacherStatus.Goedgekeurd));
            Assert.That(assignment.TeacherStatus, Is.EqualTo(TeacherStatus.Behandeling));
        }

        [Test]
        public void UpdateInternshipAssignmentShouldCorrectlyUpdateAssignment()
        {

            SetUpMockForUpdateInternshipAssignment();
            var model = GetUpdateAssignmentModel();

            _assignmentsController.UpdateInternshipAssignment(model);

            Assert.That(assignment.Description, Is.EqualTo(model.Description));
            Assert.That(assignment.InternshipPeriod, Is.EqualTo(InternshipPeriod.Semester1));
            Assert.That(assignment.Conditions, Is.EqualTo(model.Conditions));

            var model2 = model;
            model2.InternshipPeriod = "semester2";
            model2.Description = "Modified!";
            model2.Title = "TITEL MODIFIED!";

            _assignmentsController.UpdateInternshipAssignment(model2);

            Assert.That(assignment.Description, Is.EqualTo(model2.Description));
            Assert.That(assignment.Title, Is.EqualTo(model2.Title));
            Assert.That(assignment.InternshipPeriod, Is.EqualTo(InternshipPeriod.Semester2));
        }

        [Test]
        public void UpdateInternshipAssignmentShouldReturn200IfSuccessful()
        {
            SetUpMockForUpdateInternshipAssignment();
            var model = GetUpdateAssignmentModel();

            var result = _assignmentsController.UpdateInternshipAssignment(model) as OkResult;

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UpdateTeacherStatusReturns400WhenTeacherIsNotAssigned()
        {
            SetUpMockForUpdateStatusSystem();
            var model = GetUpdateTeacherStatusModel(unassignedTeacher.Email, 0, "Goedgekeurd", "message");

            var result = await _assignmentsController.UpdateTeacherStatus(model) as BadRequestResult;

            Assert.That(result.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public async Task UpdateTeacherStatusReturns401WhenTeacherIsNotFound()
        {
            SetUpMockForUpdateStatusSystem();
            var model = GetUpdateTeacherStatusModel("dontexsit@mail.com", 0, "Goedgekeurd", "message");

            var result = await _assignmentsController.UpdateTeacherStatus(model) as UnauthorizedResult;

            Assert.That(result.StatusCode, Is.EqualTo(401));
        }

        [Test]
        public void GetAssignedTeacherReviewsReturns200WhenEverythingIsCorrect()
        {
            SetUpMockForUpdateStatusSystem();
            GetAssignedTeachersModel model = new GetAssignedTeachersModel();
            model.AssignmentId = 0;
            var result = _assignmentsController.GetAssignedTeachersReview(model) as OkObjectResult;


            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void GetAssignedTeacherReviewsReturnsListReturnModelWhenEverythingIsCorrect()
        {

            SetUpMockForUpdateStatusSystem();
            GetAssignedTeachersModel model = new GetAssignedTeachersModel();
            model.AssignmentId = 0;
            var result = _assignmentsController.GetAssignedTeachersReview(model) as OkObjectResult;

            var resultList = result.Value as List<GetAssignedTeachersReturnModel>;

            Assert.That(resultList.Count > 0);
        }

        [Test]
        public void GetAssignedTeacherReviewsReturnsEmptyListWhenNoReviewsFound()
        {
            SetUpMockForUpdateStatusSystem();
            GetAssignedTeachersModel model = new GetAssignedTeachersModel();
            model.AssignmentId = 2;
            var result = _assignmentsController.GetAssignedTeachersReview(model) as OkObjectResult;

            var resultList = result.Value as List<GetAssignedTeachersReturnModel>;

            Assert.That(resultList.Count, Is.EqualTo(0));
        }

        [Test]
        [TestCase("Semester1", InternshipPeriod.Semester1)]
        [TestCase("Semester2", InternshipPeriod.Semester2)]
        public void GetInternshipPeriodShouldReturnTheCorrectPeriod(string period, InternshipPeriod expectedPeriod)
        {
            var model = GetAddAssignmentModel();
            model.InternshipPeriod = period;
            var result = _assignmentsController.GetInterschipPeriode(model);

            Assert.That(result, Is.EqualTo(expectedPeriod));
        }

        [Test]
        [TestCase(Specialization.ApplicationDevelopment, "Application Development")]
        [TestCase(Specialization.SystemAndNetwork, "System And Network")]
        [TestCase(Specialization.SoftwareManagement, "Software Management")]
        [TestCase(Specialization.AiAndRobotics, "Ai And Robotics")]

        public void ConvertToStringShouldReturnTheCorrectString(Specialization specialization, string expected)
        {
            var result = _assignmentsController.ConvertToString(specialization);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ConvertListOfEnviromentsToStringShouldReturnCorrectString()
        {
            SetupMockForGetEnviromentsByAssignmentId();
            var result = _assignmentsController.ConvertListOfEnviromentsToString(1);

            Assert.That(result, Is.EqualTo("C#,java"));
        }

        [Test]
        public void ConvertToCommaSeperatedStringOfConditionsShouldReturnCorrectString()
        {
            SetupMockForGetIntroductionConditionById();
            var result = _assignmentsController.ConvertToCommaSeperatedStringOfConditions(1);

            Assert.That(result, Is.EqualTo("CV,Vergoeding"));
        }

        [Test]
        [TestCase("0", Specialization.ApplicationDevelopment)]
        [TestCase("applicationdevelopment", Specialization.ApplicationDevelopment)]
        [TestCase("1", Specialization.SystemAndNetwork)]
        [TestCase("systemandnetwork", Specialization.SystemAndNetwork)]
        [TestCase("2", Specialization.SoftwareManagement)]
        [TestCase("softwaremanagement", Specialization.SoftwareManagement)]
        [TestCase("3", Specialization.AiAndRobotics)]
        public void GetSpecilizationShouldReturnTheCorrectSpecialization(string environmentModel, Specialization expected)
        {
            var result = _assignmentsController.GetSpecilization(environmentModel);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void MapAssignmentModelShouldReturnTheAReturnAssignmentModel()
        {
            SetupMockForMapAssignmentModel();
            SetupMockForGetEnviromentsByAssignmentId();
            SetupMockForGetIntroductionConditionById();

            var assignment = GetInternshipAssignment();

            var result = _assignmentsController.mapAssignmentModel(assignment);

            Assert.AreEqual(result.GetType(), typeof(ReturnAssignmentModel));
        }

        [Test]
        public void MapAssignmentModelShouldReturnACorrectReturnAssignmentModel()
        {
            SetupMockForMapAssignmentModel();
            SetupMockForGetEnviromentsByAssignmentId();
            SetupMockForGetIntroductionConditionById();

            var assignment = GetInternshipAssignment();

            var result = _assignmentsController.mapAssignmentModel(assignment);

            Assert.That(result.companyName, Is.EqualTo("PXL"));
            Assert.That(result.Enviroments, Is.EqualTo(_assignmentsController.ConvertListOfEnviromentsToString(assignment.Id)));
            Assert.That(result.Title, Is.EqualTo(assignment.Title));
        }

        [Test]
        public void ConvertTeacherIdStringListToIntShouldReturnACorrectList()
        {
            string teacherIDs = "1,5,9";
            var expected = new List<int>
            {
                1, 5 ,9
            };
            var result = _assignmentsController.ConvertTeacherIdStringListToInt(teacherIDs);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("Afgekeurd", TeacherStatus.Afgekeurd)]
        [TestCase("Goedgekeurd", TeacherStatus.Goedgekeurd)]
        [TestCase("Behandeling", TeacherStatus.Behandeling)]
        [TestCase("Lector toegewezen", TeacherStatus.LectorToegewezen)]
        [TestCase("Geen lector toegevoegd", TeacherStatus.GeenLector)]
        public void GetTeacherStatusShouldReturnTheCorrectStatus(string status, TeacherStatus expected)
        {
            var result = _assignmentsController.GetTeacherStatus(status);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("A")]
        public void GetTeacherStatusShouldThrowArgumentExceptionIfInputIsInvalid(string status)
        {

            Assert.That(() => _assignmentsController.GetTeacherStatus(status), Throws.ArgumentException);
        }

        [Test]
        [TestCase("0")]
        [TestCase("1,5,6")]
        public async Task AddTeachersToAssignmentShouldReturnOkIfSuccessful(string ids)
        {
            SetUpMockForUpdateStatusSystem();


            AddTeacherModel addTeacherModel = new AddTeacherModel
            {
                AssignmentId = 1,
                TeacherIds = ids
            };

            _internshipAssignmentMock.Setup(_internshipAssignmentMock => _internshipAssignmentMock.GetInternshipAssignmentById(It.IsAny<int>()))
                .Returns(GetInternshipAssignment());

            var action = await _assignmentsController.AddTeachersToAssignment(addTeacherModel);

            var result = (OkResult)action;
            Assert.That(result.StatusCode, Is.EqualTo(200));
        }

        [Test]
        [TestCase("Afgekeurd", Status.Afgekeurd)]
        [TestCase("Goedgekeurd", Status.Goedgekeurd)]
        [TestCase("Behandeling", Status.Behandeling)]
        [TestCase("Opdracht verzonden", Status.Verzonden)]
        public void GetStatusShouldChangeStatusToCorrectStatus(string status, Status expected)
        {
            var result = _assignmentsController.GetStatus(status);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("AA", Status.Afgekeurd)]
        public void GetStatusShouldThrowArgumentExceptionIfStatusDoesNotExist(string status, Status expected)
        {
            Assert.That(() => _assignmentsController.GetStatus(status), Throws.ArgumentException);
        }

        [Test]
        [TestCase(TeacherStatus.Afgekeurd, "Afgekeurd")]
        [TestCase(TeacherStatus.Behandeling, "Behandeling")]
        [TestCase(TeacherStatus.Goedgekeurd, "Goedgekeurd")]
        [TestCase(TeacherStatus.LectorToegewezen, "Lector toegewezen")]
        [TestCase(TeacherStatus.GeenLector, "Geen lector toegevoegd")]
        public void DeterminTeacherStatusShouldReturnStatusInStringCorrectly(TeacherStatus teacherStatus, string expected)
        {
            var result = _assignmentsController.DeterminTeacherStatus(teacherStatus);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(Status.Goedgekeurd, "Goedgekeurd")]
        [TestCase(Status.Afgekeurd, "Afgekeurd")]
        [TestCase(Status.Behandeling, "Behandeling")]
        [TestCase(Status.Verzonden, "Opdracht verzonden")]
        public void DeterminStatusShouldReturnCorrectString(Status status, string expected)
        {
            var result = _assignmentsController.DeterminStatus(status);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(TeacherStatus.Afgekeurd, "Afgekeurd (0/2)")]
        [TestCase(TeacherStatus.Goedgekeurd, "Goedgekeurd (0/2)")]
        [TestCase(TeacherStatus.Behandeling, "Behandeling (0/2)")]
        [TestCase(TeacherStatus.GeenLector, "Geen lector toegevoegd")]
        [TestCase(TeacherStatus.LectorToegewezen, "Lector toegewezen (0/2)")]
        public void DeterminTeacherStatusShouldReturnCorrectStringWithId(TeacherStatus status, string expected)
        {
            SetUpMockForUpdateStatusSystem();
            var result = _assignmentsController.DeterminTeacherStatus(status, 1);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(InternshipPeriod.Semester1, "Semester1")]
        [TestCase(InternshipPeriod.Semester2, "Semester2")]
        public void ConvertInternshipPeriodToStringShouldReturnCorrectString(InternshipPeriod period, string expected)
        {
            var result = _assignmentsController.ConvertInternshipPeriodToString(period);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void AddInternshipAssignmentShouldReturn200IfSuccessful()
        {
            var assignment = GetAddAssignmentModel();
            var result = _assignmentsController.AddInternshipAssignments(assignment) as OkObjectResult;

            Assert.That(result.StatusCode, Is.EqualTo(200));
        }


        private IActionResult GetAssignmentById(int id)
        {
            var assignmentModel = SetupAssignmentsModel(id);
            var environments = new List<Environment>();

            var environment = new Environment()
            {

                EnvironmentName = "Java",
                Id = 1,
                InternshipAssignment = new InternshipAssignment(),
                InternshipAssignmentId = id
            };


            environments.Add(environment);

            _internshipAssignmentMock.Setup(_internshipAssignmentMock =>
                _internshipAssignmentMock.GetInternshipAssignmentById(It.IsAny<int>()))
            .Returns(new InternshipAssignment()
            {
                AmountOfInterns = 2,
                AmountOfSupportingEmployees = 50,
                Company = new Company(),
                CompanyId = 1,
                Conditions = "condition",
                DateCreated = DateTime.Now,
                Description = "description",
                Environments = new List<Environment>(environments),
                ExtraDescriptionEnvironments = "extra description",
                Id = id,
                Specialization = Specialization.AiAndRobotics,
                SpecificStudentFirstAndLastName = "Last first",
                Status = Status.Behandeling,
                TeacherStatus = TeacherStatus.Behandeling,
                ResearchTheme = "test",
                Location = new Address(),
                OtherComments = "",
                InternshipPeriod = InternshipPeriod.Semester1,
                IntroductionConditions = new List<IntroductionCondition>()


            });

            return _assignmentsController.GetInternshipAssignmentById(assignmentModel);
        }

        private AssignmentsModel SetupAssignmentsModel(int id)
        {
            return new AssignmentsModel
            {
                Id = id
            };
        }

        private void SetUpMocksForGetAllAssignedAssingments()
        {
            _userManagerMock.Setup(_usermanager => _usermanager.FindByEmailAsync(It.IsAny<string>()))
                .Returns<string>(email => GetUserByMail(email));
            _internshipAssignmentMock.Setup(_ia => _ia.GetAssignmentsByTeacher(It.IsAny<User>()))
                .Returns(GetInternshipAssignmentsTeacher);
            _environmentsDataMock.Setup(_environmentsDataMock => _environmentsDataMock.GetEnviromentsByAssignmentId(It.IsAny<int>()))
                .Returns(GetEnvironment);
            _introductionConditionDataMock.Setup(_introductionConditionDataMock => _introductionConditionDataMock.GetIntroductionConditionById(It.IsAny<int>()))
                .Returns(new List<IntroductionCondition>());
        }

        private void SetUpMockForUpdateStatusSystem()
        {
            _internshipAssignmentMock.Setup(_internshipAssignmentMock => _internshipAssignmentMock.UpdateStatus(It.IsAny<int>(), It.IsAny<Status>(), It.IsAny<string>()))
                .Callback<int, Status, string>((id, status, message) => UpdateStatus(status, message));
            _internshipAssignmentMock.Setup(_internshipAssignmentMock => _internshipAssignmentMock.GetInternshipAssignmentById(It.IsAny<int>()))
                .Returns(assignment);
            _internshipAssignmentMock.Setup(_internshipAssignmentMock => _internshipAssignmentMock.RemoveAllTeachersFromAssignment(It.IsAny<InternshipAssignment>()))
                .Callback(() => assigned.Teacher = null);
            _internshipAssignmentMock.Setup(_internshipAssignmentMock => _internshipAssignmentMock.UpdateTeacherStatusAssignment(It.IsAny<int>(), It.IsAny<TeacherStatus>()))
                .Callback<int, TeacherStatus>((id, status) => UpdateTeacherStatus(status));
            _internshipAssignmentMock.Setup(_internshipAssignmentMock => _internshipAssignmentMock.GetAssignedTeacherObjectsByAssignmentId(It.IsAny<int>()))
                .Returns<int>(id => GetAssignedTeachers(id));
            _internshipAssignmentMock.Setup(_internshipAssignmentMock => _internshipAssignmentMock.GetAllAssignedTeachersByAssigmentId(It.IsAny<int>()))
                .Returns<int>(id => GetTeachers(id));
            _internshipAssignmentMock.Setup(_internshipAssignmentMock => _internshipAssignmentMock.UpdateTeacherStatusAssignmentTeacher(It.IsAny<User>(), It.IsAny<int>(), It.IsAny<TeacherStatus>(), It.IsAny<string>()))
                .Callback<User, int, TeacherStatus, string>((teacher, id, status, message) => UpdateAssigned(status, message));
            _userManagerMock.Setup(_usermanager => _usermanager.FindByEmailAsync(It.IsAny<string>()))
                .Returns<string>(email => GetUserByMail(email));

        }

        private void SetUpMockForUpdateInternshipAssignment()
        {
            _internshipAssignmentMock.Setup(_internshipAssignmentMock => _internshipAssignmentMock.UpdateInternshipAssignment(It.IsAny<InternshipAssignment>()))
                .Callback<InternshipAssignment>((assignment) => UpdateAssignment(assignment));

        }

        private void SetupMockForGetEnviromentsByAssignmentId()
        {
            var environmentList = new List<Environment>();
            var environment = new Environment
            {
                Id = 1,
                EnvironmentName = "C#",
                InternshipAssignmentId = 1
            };

            var environment2 = new Environment
            {
                Id = 2,
                EnvironmentName = "java",
                InternshipAssignmentId = 1
            };

            environmentList.Add(environment);
            environmentList.Add(environment2);

            _environmentsDataMock.Setup(_environmentsDataMock => _environmentsDataMock.GetEnviromentsByAssignmentId(It.IsAny<int>()))
                .Returns(environmentList);
        }

        private void SetupMockForGetIntroductionConditionById()
        {
            var conditionList = new List<IntroductionCondition>();
            var introCondition = new IntroductionCondition
            {
                Id = 1,
                Condition = "CV",
                InternshipAssignmentId = 1
            };
            var introCondition2 = new IntroductionCondition
            {
                Id = 1,
                Condition = "Vergoeding",
                InternshipAssignmentId = 1
            };

            conditionList.Add(introCondition);
            conditionList.Add(introCondition2);

            _introductionConditionDataMock.Setup(_introductionConditionDataMock => _introductionConditionDataMock.GetIntroductionConditionById(It.IsAny<int>()))
                .Returns(conditionList);
        }

        private void SetupMockForMapAssignmentModel()
        {
            var address = new Address
            {
                AddressId = 1,
                Street = "Elfde-liniestraat",
                Number = "50",
                PostalNumber = "3500",
                Township = "Hasselt",
                CompanyId = 1
            };

            _companyDataMock.Setup(_companyDataMock => _companyDataMock.GetCompanyById(It.IsAny<int>()))
                .Returns(new Company
                {
                    NameCompany = "PXL",
                    AddressCompany = address,
                    AmountOfITPersonnel = 50,
                    AmountOfPersonnel = 100,
                    CompanyId = 1
                });
        }


        private void UpdateStatus(Status status, string message)
        {
            assignment.Status = status;
            assignment.ReviewMessage = message;
        }

        private void UpdateAssignment(InternshipAssignment assignment)
        {
            this.assignment = assignment;
        }

        private void UpdateTeacherStatus(TeacherStatus status)
        {
            assignment.TeacherStatus = status;
        }

        private void UpdateAssigned(TeacherStatus status, string message)
        {
            assigned.Status = status;
            assigned.ReviewMessage = message;
        }

        private IEnumerable<AssignedTeachers> GetAssignedTeachers(int id)
        {
            List<AssignedTeachers> assignedTeachers = new List<AssignedTeachers>();
            if (id == assignment.Id)
            {
                assignedTeachers.Add(assigned);
            }
            else if (id == 1)
            {
                assignedTeachers.Add(assigned);
                assignedTeachers.Add(new AssignedTeachers
                {
                    Status = TeacherStatus.Behandeling
                });
            }
            return assignedTeachers;
        }

        private List<User> GetTeachers(int id)
        {
            List<User> teachers = new List<User>();
            teachers.Add(teacher);
            if (id == 1)
            {
                teachers.Add(unassignedTeacher);
            }

            return teachers;
        }

        private async Task<User> GetUserByMail(string email)
        {
            if (email == user.Email)
                return user;
            else if (email == teacher.Email)
                return teacher;
            else if (email == unassignedTeacher.Email)
                return unassignedTeacher;
            return null;
        }

        private IEnumerable<InternshipAssignment> GetInternshipAssignmentsTeacher()
        {
            List<InternshipAssignment> assignments = new List<InternshipAssignment>();
            if (this.hasAssignments)
            {
                int id = 1;
                var environments = new List<Environment>();

                var environment = new Environment()
                {

                    EnvironmentName = "Java",
                    Id = 1,
                    InternshipAssignment = new InternshipAssignment(),
                    InternshipAssignmentId = id
                };

                environments.Add(environment);

                assignments.Add(new InternshipAssignment()
                {
                    AmountOfInterns = 2,
                    AmountOfSupportingEmployees = 50,
                    Company = new Company(),
                    CompanyId = 1,
                    Conditions = "condition",
                    DateCreated = DateTime.Now,
                    Description = "description",
                    Environments = new List<Environment>(environments),
                    ExtraDescriptionEnvironments = "extra description",
                    Id = id,
                    Specialization = Specialization.AiAndRobotics,
                    SpecificStudentFirstAndLastName = "Last first",
                    Status = Status.Behandeling,
                    TeacherStatus = TeacherStatus.Behandeling,
                    ResearchTheme = "test",
                    Location = new Address(),
                    OtherComments = "",
                    InternshipPeriod = InternshipPeriod.Semester1,
                    IntroductionConditions = new List<IntroductionCondition>()


                });

            }
            return assignments;

        }

        private IEnumerable<InternshipAssignment> GetAllAssignmentList()
        {

            return new List<InternshipAssignment>
                {
                    new InternshipAssignment {
                        AmountOfInterns = 2,
                    AmountOfSupportingEmployees = 50,
                    Company = new Company(),
                    CompanyId = 1,
                    Conditions = "condition",
                    DateCreated = DateTime.Now,
                    Description = "description",
                    Environments = new List<Environment>(),
                    ExtraDescriptionEnvironments = "extra description",
                    Id = 1,
                    Specialization = Specialization.AiAndRobotics,
                    SpecificStudentFirstAndLastName = "Last first",
                    Status = Status.Behandeling,
                    TeacherStatus = TeacherStatus.Behandeling,
                    ResearchTheme = "test",
                    Location = new Address(),
                    OtherComments = "",
                    InternshipPeriod = InternshipPeriod.Semester1,
                    IntroductionConditions = new List<IntroductionCondition>()
                    },
                    new InternshipAssignment {
                        AmountOfInterns = 2,
                        AmountOfSupportingEmployees = 50,
                        Company = new Company(),
                        CompanyId = 2,
                        Conditions = "condition",
                        DateCreated = DateTime.Now,
                        Description = "description",
                        Environments = new List<Environment>(),
                        ExtraDescriptionEnvironments = "extra description",
                        Id = 2,
                        Specialization = Specialization.AiAndRobotics,
                        SpecificStudentFirstAndLastName = "Last first",
                        Status = Status.Behandeling,
                        TeacherStatus = TeacherStatus.Behandeling,
                        ResearchTheme = "test",
                        Location = new Address(),
                        OtherComments = "",
                        InternshipPeriod = InternshipPeriod.Semester1,
                        IntroductionConditions = new List<IntroductionCondition>()
                    },
                    new InternshipAssignment {
                        AmountOfInterns = 2,
                        AmountOfSupportingEmployees = 50,
                        Company = new Company(),
                        CompanyId = 1,
                        Conditions = "condition",
                        DateCreated = DateTime.Now,
                        Description = "description",
                        Environments = new List<Environment>(),
                        ExtraDescriptionEnvironments = "extra description",
                        Id = 3,
                        Specialization = Specialization.AiAndRobotics,
                        SpecificStudentFirstAndLastName = "Last first",
                        Status = Status.Behandeling,
                        TeacherStatus = TeacherStatus.Behandeling,
                        ResearchTheme = "test",
                        Location = new Address(),
                        OtherComments = "",
                        InternshipPeriod = InternshipPeriod.Semester1,
                        IntroductionConditions = new List<IntroductionCondition>()
                    }
                };
        }

        private IEnumerable<InternshipAssignment> GetAllAssignmentListByCompanyId()
        {

            return new List<InternshipAssignment>
                {
                    new InternshipAssignment {
                        AmountOfInterns = 2,
                    AmountOfSupportingEmployees = 50,
                    Company = new Company(),
                    CompanyId = 1,
                    Conditions = "condition",
                    DateCreated = DateTime.Now,
                    Description = "description",
                    Environments = new List<Environment>(),
                    ExtraDescriptionEnvironments = "extra description",
                    Id = 1,
                    Specialization = Specialization.AiAndRobotics,
                    SpecificStudentFirstAndLastName = "Last first",
                    Status = Status.Behandeling,
                    TeacherStatus = TeacherStatus.Behandeling,
                    ResearchTheme = "test",
                    Location = new Address(),
                    OtherComments = "",
                    InternshipPeriod = InternshipPeriod.Semester1,
                    IntroductionConditions = new List<IntroductionCondition>()
                    },
                    new InternshipAssignment {
                        AmountOfInterns = 2,
                        AmountOfSupportingEmployees = 50,
                        Company = new Company(),
                        CompanyId = 1,
                        Conditions = "condition",
                        DateCreated = DateTime.Now,
                        Description = "description",
                        Environments = new List<Environment>(),
                        ExtraDescriptionEnvironments = "extra description",
                        Id = 2,
                        Specialization = Specialization.AiAndRobotics,
                        SpecificStudentFirstAndLastName = "Last first",
                        Status = Status.Behandeling,
                        TeacherStatus = TeacherStatus.Behandeling,
                        ResearchTheme = "test",
                        Location = new Address(),
                        OtherComments = "",
                        InternshipPeriod = InternshipPeriod.Semester1,
                        IntroductionConditions = new List<IntroductionCondition>()
                    },
                    new InternshipAssignment {
                        AmountOfInterns = 2,
                        AmountOfSupportingEmployees = 50,
                        Company = new Company(),
                        CompanyId = 1,
                        Conditions = "condition",
                        DateCreated = DateTime.Now,
                        Description = "description",
                        Environments = new List<Environment>(),
                        ExtraDescriptionEnvironments = "extra description",
                        Id = 3,
                        Specialization = Specialization.AiAndRobotics,
                        SpecificStudentFirstAndLastName = "Last first",
                        Status = Status.Behandeling,
                        TeacherStatus = TeacherStatus.Behandeling,
                        ResearchTheme = "test",
                        Location = new Address(),
                        OtherComments = "",
                        InternshipPeriod = InternshipPeriod.Semester1,
                        IntroductionConditions = new List<IntroductionCondition>()
                    }
                };
        }
        private IEnumerable<InternshipAssignment> GetAllAssignmentListBySpecialization()
        {

            return new List<InternshipAssignment>
                {
                    new InternshipAssignment {
                        AmountOfInterns = 2,
                    AmountOfSupportingEmployees = 50,
                    Company = new Company(),
                    CompanyId = 1,
                    Conditions = "condition",
                    DateCreated = DateTime.Now,
                    Description = "description",
                    Environments = new List<Environment>(),
                    ExtraDescriptionEnvironments = "extra description",
                    Id = 1,
                    Specialization = Specialization.ApplicationDevelopment,
                    SpecificStudentFirstAndLastName = "Last first",
                    Status = Status.Behandeling,
                    TeacherStatus = TeacherStatus.Behandeling,
                    ResearchTheme = "test",
                    Location = new Address(),
                    OtherComments = "",
                    InternshipPeriod = InternshipPeriod.Semester1,
                    IntroductionConditions = new List<IntroductionCondition>()
                    },
                    new InternshipAssignment {
                        AmountOfInterns = 2,
                        AmountOfSupportingEmployees = 50,
                        Company = new Company(),
                        CompanyId = 1,
                        Conditions = "condition",
                        DateCreated = DateTime.Now,
                        Description = "description",
                        Environments = new List<Environment>(),
                        ExtraDescriptionEnvironments = "extra description",
                        Id = 2,
                        Specialization = Specialization.ApplicationDevelopment,
                        SpecificStudentFirstAndLastName = "Last first",
                        Status = Status.Behandeling,
                        TeacherStatus = TeacherStatus.Behandeling,
                        ResearchTheme = "test",
                        Location = new Address(),
                        OtherComments = "",
                        InternshipPeriod = InternshipPeriod.Semester1,
                        IntroductionConditions = new List<IntroductionCondition>()
                    },
                    new InternshipAssignment {
                        AmountOfInterns = 2,
                        AmountOfSupportingEmployees = 50,
                        Company = new Company(),
                        CompanyId = 1,
                        Conditions = "condition",
                        DateCreated = DateTime.Now,
                        Description = "description",
                        Environments = new List<Environment>(),
                        ExtraDescriptionEnvironments = "extra description",
                        Id = 3,
                        Specialization = Specialization.ApplicationDevelopment,
                        SpecificStudentFirstAndLastName = "Last first",
                        Status = Status.Behandeling,
                        TeacherStatus = TeacherStatus.Behandeling,
                        ResearchTheme = "test",
                        Location = new Address(),
                        OtherComments = "",
                        InternshipPeriod = InternshipPeriod.Semester1,
                        IntroductionConditions = new List<IntroductionCondition>()
                    }
                };
        }

        private InternshipAssignment GetInternshipAssignment()
        {
            return new InternshipAssignment()
            {
                AmountOfInterns = 2,
                AmountOfSupportingEmployees = 50,
                Company = new Company(),
                CompanyId = 1,
                Conditions = "condition",
                DateCreated = DateTime.Now,
                Description = "description",
                Environments = new List<Environment>(),
                ExtraDescriptionEnvironments = "extra description",
                Id = 1,
                Specialization = Specialization.AiAndRobotics,
                SpecificStudentFirstAndLastName = "Last first",
                Status = Status.Behandeling,
                TeacherStatus = TeacherStatus.Behandeling,
                ResearchTheme = "test",
                Location = new Address(),
                OtherComments = "",
                InternshipPeriod = InternshipPeriod.Semester1,
                IntroductionConditions = new List<IntroductionCondition>()


            };
        }

        private List<Environment> GetEnvironment()
        {
            var environments = new List<Environment>();
            int id = 1;
            var environment = new Environment()
            {

                EnvironmentName = "Java",
                Id = 1,
                InternshipAssignment = new InternshipAssignment(),
                InternshipAssignmentId = id
            };

            environments.Add(environment);
            return environments;
        }

        private AssignedTeacherModel GetAssignedTeacherModel(string email)
        {
            return new AssignedTeacherModel
            {
                Email = email
            };
        }

        private UpdateStatusModel GetUpdateStatusModel(int assignmentId, string status, string message)
        {
            return new UpdateStatusModel
            {
                AssignmentId = assignmentId,
                Status = status,
                Message = message
            };
        }

        private UpdateTeacherStatusModel GetUpdateTeacherStatusModel(string email, int id, string status, string message)
        {
            return new UpdateTeacherStatusModel
            {
                LectorEmail = email,
                AssignmentId = id,
                Status = status,
                Message = message
            };
        }

        private UpdateAssignmentModel GetUpdateAssignmentModel()
        {
            return new UpdateAssignmentModel
            {
                Id = 0,
                AmountOfInterns = 1,
                AmountOfSupportingEmployees = 50,
                LocationAddressId = 1,
                SpecificStudentFirstAndLastName = "",
                CompanyId = 1,
                Conditions = "",
                Description = "Description",
                Environments = "C#, java",
                Specialization = "aiandrobotics",
                ExtraDescriptionEnvironments = "",
                InternshipPeriod = "0",
                IntroductionConditions = "CV",
                Number = 50,
                Street = "Elfde-liniestraat",
                Postalnumber = "3500",
                Title = "title",
                Township = "Hasselt",
                OtherComments = "",
                ResearchTheme = "Researchtheme"
            };
        }

        private AddAssignmentModel GetAddAssignmentModel()
        {
            return new AddAssignmentModel
            {
                AmountOfInterns = 1,
                AmountOfSupportingEmployees = 50,
                SpecificStudentFirstAndLastName = "",
                CompanyId = 1,
                Conditions = "",
                Description = "Description",
                Enviroments = "C#, java",
                Specialization = "aiandrobotics",
                ExtraDescr = "",
                InternshipPeriod = "Semester1",
                IntroductionCondition = "CV",
                Number = "50",
                Street = "Elfde-liniestraat",
                Postalnumber = "3500",
                Title = "title",
                Township = "Hasselt",
                OtherComments = "",
                ResearchTheme = "Researchtheme"
            };
        }


    }
}
