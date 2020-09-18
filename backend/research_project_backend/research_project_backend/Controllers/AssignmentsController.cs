using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using research_project_backend.Data;
using research_project_backend.Data.Domains;
using research_project_backend.Data.Domains.Enums;
using research_project_backend.Services;
using research_project_backend.Models;

namespace research_project_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AssignmentsController : ControllerBase
    {
        private readonly IInternshipAssignments _internshipAssignments;
        private readonly IEnviromentsData _enviromentsData;
        private readonly IIntroductionConditionData _introductionConditionData;
        private readonly ICompany _company;

        public AssignmentsController(IInternshipAssignments internshipAssignments, IEnviromentsData enviromentsData, IIntroductionConditionData introductionConditionData
        , ICompany company)
        {
            _internshipAssignments = internshipAssignments;
            _enviromentsData = enviromentsData;
            _introductionConditionData = introductionConditionData;
            _company = company;
        }

        [Route("InternshipAssignments")]
        [HttpGet]
        public IActionResult GetAllInternshipAssignments()
        {
            var assignments = _internshipAssignments.GetAllInternshipAssignments()
                .Select(mapAssignmentModel);

            return Ok(assignments);
        }
        [Route("InternshipAssignmentsByCompanyId")]
        [HttpPost]
        public IActionResult GetAllInternshipAssignmentsByCompanyId([FromBody] AssignmentsModel model)
        {
            var assignments = _internshipAssignments.GetAllInternshipAssignmentsByCompanyId(model.CompanyId)
                .Select(mapAssignmentModel);
            return Ok(assignments);
        }

        [Route("AddInternshipAssignments")]
        [HttpPost]
        public IActionResult AddInternshipAssignments([FromBody] AddAssignmentModel model)
        {
            var assignment = new InternshipAssignment
            {
                CompanyId = model.CompanyId,
                Specialization = GetSpecilization(model.Specialization),
                Description = model.Description,
                Environments = EnviromentsToList(model.Enviroments),
                ExtraDescriptionEnvironments = model.ExtraDescr,
                Conditions = model.Conditions,
                ResearchTheme = model.ResearchTheme,
                Location = new Address { Street = model.Street, Number = model.Number, Township = model.Township, PostalNumber = model.Postalnumber},
                AmountOfSupportingEmployees = model.AmountOfSupportingEmployees,
                IntroductionConditions = IndroductionConditionsParseToList(model.IntroductionCondition),
                AmountOfInterns = model.AmountOfInterns,
                SpecificStudentFirstAndLastName = model.SpecificStudentFirstAndLastName,
                OtherComments = model.OtherComments,
                InternshipPeriod = GetInterschipPeriode(model)


            };

            _internshipAssignments.UpdateInternshipAssignment(assignment);
            return Ok();

    }

        private static InternshipPeriod UpdateInterschipPeriode(UpdateAssignmentModel model)
        {
            switch (model.InternshipPeriod)
            {
                case "0":
                    return InternshipPeriod.Semester1;
                default:
                    return InternshipPeriod.Semester2;
            }
        }

        [Route("GetApprovedInternshipAssingmentBySpecialization")]
        [HttpPost]
        public IActionResult GetApprovedInternshipAssingmentBySpecialization([FromBody] SpecializationModel model)
        {
            var assignments = _internshipAssignments
                .GetApprovedInternshipAssingmentBySpecialization(GetSpecilization(model.Specialization))
                .Select(mapAssignmentModel);
            return Ok(assignments);
        }

        [Route("AddTeachersToAssignment")]
        [HttpPost]
        public async Task<IActionResult> AddTeachersToAssignment([FromBody] AddTeacherModel model)
        {
            var assignment = _internshipAssignments.GetInternshipAssignmentById(model.AssignmentId);

            if (model.TeacherIds == "0")
            {
                _internshipAssignments.RemoveAllTeachersFromAssignment(assignment);

                return Ok();
            }

            List<int> teacherids = ConvertTeacherIdStringListToInt(model.TeacherIds);
            List<User> teachers = new List<User>();
            foreach (int id in teacherids)
            {

                var teacher = await _userManager.FindByIdAsync(id.ToString());
                teachers.Add(teacher);
            }
            _internshipAssignments.AddTeacherToAssignment(teachers, assignment);
            _internshipAssignments.UpdateStatus(model.AssignmentId, Status.Behandeling, "");
            _internshipAssignments.UpdateTeacherStatusAssignment(model.AssignmentId, TeacherStatus.LectorToegewezen);

            return Ok();
        }

        [Route("GetAllAssignedAssingments")]
        [HttpPost]
        public async Task<IActionResult> GetAllAssignedAssingments([FromBody] AssignedTeacherModel model)
        {
            var teacher = await _userManager.FindByEmailAsync(model.Email);
            if (teacher == null)
                return Unauthorized();

            var assignedAssignments = _internshipAssignments.GetAssignmentsByTeacher(teacher)
                .Select(mapAssignmentModel);

            return Ok(assignedAssignments);
        }

        [Route("updatestatus")]
        [HttpPost]
        public IActionResult UpdateStatus([FromBody] UpdateStatusModel model)
        {
            
            Status status;
            try
            {
                status = GetStatus(model.Status);
                TeacherStatus teacherstatus = GetTeacherStatus(model.Status);
                _internshipAssignments.UpdateStatus(model.AssignmentId, status, model.Message);
                var assignment = _internshipAssignments.GetInternshipAssignmentById(model.AssignmentId);
                _internshipAssignments.RemoveAllTeachersFromAssignment(assignment);
                _internshipAssignments.UpdateTeacherStatusAssignment(model.AssignmentId, teacherstatus);


                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest();
            }
            
            
        }

        [Route("updateteacherstatus")]
        [HttpPost]
        public async Task<IActionResult> UpdateTeacherStatus([FromBody] UpdateTeacherStatusModel model)
        {
            
            var teacher = await _userManager.FindByEmailAsync(model.LectorEmail);
            if (teacher == null)
                return Unauthorized();
            
            var assignedTeachers = _internshipAssignments.GetAllAssignedTeachersByAssigmentId(model.AssignmentId);
            if (assignedTeachers.Count == 0)
                return BadRequest();

            if(assignedTeachers.Count > 1)
            {
                bool teacherFound = false;
                foreach(User assignedTeacher in assignedTeachers)
                {
                    if (assignedTeacher == teacher)
                        teacherFound = true;
                }

                if (!teacherFound)
                    return BadRequest();
                try
                {
                    TeacherStatus status;
                    status = GetTeacherStatus(model.Status);
                    _internshipAssignments.UpdateTeacherStatusAssignmentTeacher(teacher, model.AssignmentId, status, model.Message);

                    var assignedTeacherObjects = _internshipAssignments.GetAssignedTeacherObjectsByAssignmentId(model.AssignmentId);
                    bool allTeachersAgree = true;
                    foreach(AssignedTeachers assigned in assignedTeacherObjects)
                    {
                        if (assigned.Status != status)
                            allTeachersAgree = false;
                    }
                    if(allTeachersAgree)
                        _internshipAssignments.UpdateTeacherStatusAssignment(model.AssignmentId, status);
                    else
                        _internshipAssignments.UpdateTeacherStatusAssignment(model.AssignmentId, TeacherStatus.Behandeling);
                    return Ok();
                }
                catch (ArgumentException e)
                {
                    return BadRequest();
                }

            }
            else
            {
                if (assignedTeachers[0] != teacher)
                    return BadRequest();
                try
                {
                    TeacherStatus status;
                    status = GetTeacherStatus(model.Status);
                    _internshipAssignments.UpdateTeacherStatusAssignmentTeacher(teacher, model.AssignmentId, status, model.Message);
                    _internshipAssignments.UpdateTeacherStatusAssignment(model.AssignmentId, status);
                    return Ok();
                }
                catch (ArgumentException e)
                {
                    return BadRequest();
                }
            }
            
        }

        [Route("getassignedteachersreview")]
        [HttpPost]
        public IActionResult GetAssignedTeachersReview(GetAssignedTeachersModel model)
        {
            var assignedTeacher = _internshipAssignments.GetAssignedTeacherObjectsByAssignmentId(model.AssignmentId);
            var result = new List<GetAssignedTeachersReturnModel>();

            foreach(AssignedTeachers assigned in assignedTeacher)
            {
                result.Add(new GetAssignedTeachersReturnModel 
                { 
                    Message = assigned.ReviewMessage,
                    Status = DeterminTeacherStatus(assigned.Status),
                    TeacherEmail = assigned.Teacher.Email,
                    TeacherId = assigned.Teacher.Id
                });
            }

            return Ok(result);

        }

        public Status GetStatus(string status)
        {
            switch (status)
            {
                case "Afgekeurd":
                    return Status.Afgekeurd;
                case "Goedgekeurd":
                    return Status.Goedgekeurd;
                case "Behandeling":
                    return Status.Behandeling;
                case "Opdracht verzonden":
                    return Status.Verzonden;
                default:
                    throw new ArgumentException();
            }
        }

        public TeacherStatus GetTeacherStatus(string teacherstatus)
        {
            switch (teacherstatus)
            {
                case "Afgekeurd":
                    return TeacherStatus.Afgekeurd;
                case "Goedgekeurd":
                    return TeacherStatus.Goedgekeurd;
                case "Behandeling":
                    return TeacherStatus.Behandeling;
                case "Lector toegewezen":
                    return TeacherStatus.LectorToegewezen;
                case "Geen lector toegevoegd":
                    return TeacherStatus.GeenLector;
                default:
                    throw new ArgumentException();
            }
        }

        public InternshipPeriod GetInterschipPeriode(AddAssignmentModel model)
        {
            switch (model.InternshipPeriod)
            {
                case "Semester1":
                    return InternshipPeriod.Semester1;
                default:
                    return InternshipPeriod.Semester2;
            }
        }

        public List<int> ConvertTeacherIdStringListToInt(string teacherIds)
        {
            List<string> teacherIdStringList = teacherIds.Trim(',').Split(',').ToList();
            List<int> teacherIdIntList = new List<int>();
            foreach (string id in teacherIdStringList)

            {
                teacherIdIntList.Add(Convert.ToInt32(id));
            }

            
        }

        private List<IntroductionCondition> IndroductionConditionsParseToList(string conditions)
        {
            return conditions.Split(',').ToList()
                .Select(s => new IntroductionCondition {Condition = s}).ToList();

        }

        private static List<Environment> EnviromentsToList(string enviroments)
        {
            return enviroments.Split(',').ToList().Select(s => new Environment {EnvironmentName = s}).ToList();
        }

        public Specialization GetSpecilization(string modelSpecialization)
        {
            switch (modelSpecialization.ToLower())
            {
                case "applicationdevelopment":
                    return Specialization.ApplicationDevelopment;
                case "systemandnetwork":
                    return Specialization.SystemAndNetwork;
                case "softwaremanagement":
                    return Specialization.SoftwareManagement;
               default:
                    return Specialization.AiAndRobotics;
            }
        }
        public string DeterminStatus(Status status)
        {
            switch (status)
            {
                case Status.Afgekeurd:
                    return "Afgekeurd";
                case Status.Goedgekeurd:
                    return "Goedgekeurd";
                default:
                    return "Behandeling";

        public string DeterminTeacherStatus(TeacherStatus status)
        {
            switch (status)
            {
                case TeacherStatus.Afgekeurd:
                    return "Afgekeurd" ;
                case TeacherStatus.Goedgekeurd:
                    return "Goedgekeurd" ;
                case TeacherStatus.Behandeling:
                    return "Behandeling" ;
                case TeacherStatus.LectorToegewezen:
                    return "Lector toegewezen" ;
                default:
                    return "Geen lector toegevoegd";
            }
        }

        public string DeterminTeacherStatus(TeacherStatus status, int id)
        {
            switch (status)
            {
                case TeacherStatus.Afgekeurd:
                    return "Afgekeurd " + GetAssignedTeacherString(id, status);
                case TeacherStatus.Goedgekeurd:
                    return "Goedgekeurd " + GetAssignedTeacherString(id, status);
                case TeacherStatus.Behandeling:
                    return GetAssignedTeacherString(id, status);
                case TeacherStatus.LectorToegewezen:
                    return "Lector toegewezen " + GetAssignedTeacherString(id, status);
                default:
                    return "Geen lector toegevoegd";
            }
        }

        private string GetAssignedTeacherString(int id, TeacherStatus status)
        {
            var assignedTeacher = _internshipAssignments.GetAssignedTeacherObjectsByAssignmentId(id);
            int amountAssigned = assignedTeacher.Count();
            int amountReviewed = 0;

            }
            StringBuilder builder = new StringBuilder();
            if (status == TeacherStatus.Behandeling)
            {
                if(amountReviewed == amountAssigned)
                {
                    builder.Append("Verschillende reviews (").Append(amountReviewed).Append("/").Append(amountAssigned).Append(")");
                }
                else
                {
                    builder.Append("Behandeling (").Append(amountReviewed).Append("/").Append(amountAssigned).Append(")");
                }
            }
            else
            {
                builder.Append("(").Append(amountReviewed).Append("/").Append(amountAssigned).Append(")");
            }
            
            

            return builder.ToString();
        }

        public string ConvertToString(Specialization specialization)
        {
            switch (specialization)
            {
                case Specialization.ApplicationDevelopment:
                    return "Application Development";
                case Specialization.SystemAndNetwork:
                    return "System And Network";
                case Specialization.SoftwareManagement:
                    return "Software Management";
                default:
                    return "Ai And Robotics";
            }

            ;
        }

        public string ConvertToCommaSeperatedStringOfConditions(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            _introductionConditionData.GetIntroductionConditionById(id).ForEach(i => stringBuilder.Append(i.Condition + ","));
            stringBuilder.Length--;
            return stringBuilder.ToString();
        }

        public string ConvertInternshipPeriodToString(InternshipPeriod internshipPeriod)
        {
            switch (internshipPeriod)
            {
                case InternshipPeriod.Semester1:
                    return "Semester1";
                default:
                    return "Semester2";

            }
        }

        public string ConvertListOfEnviromentsToString(int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            _enviromentsData.GetEnviromentsByAssignmentId(id).ForEach(e => stringBuilder.Append(e.EnvironmentName + ","));
            return stringBuilder.ToString();
        }

        public ReturnAssignmentModel mapAssignmentModel(InternshipAssignment assignment)
        {
            return new ReturnAssignmentModel
            {
                companyName = _company.GetCompanyById(assignment.CompanyId).NameCompany,
                companyId = assignment.CompanyId,
                amountOfInterns = assignment.AmountOfInterns,
                Id = assignment.Id,
                AmountOfSupportingEmployees = assignment.AmountOfSupportingEmployees,
                Conditions = assignment.Conditions,
                Title = assignment.Title,
                Description = assignment.Description,
                DateCreated = assignment.DateCreated,
                Enviroments = ConvertListOfEnviromentsToString(assignment.Id),
                ExtraDescriptionEnvironments = assignment.ExtraDescriptionEnvironments,
                InternshipPeriod = ConvertInternshipPeriodToString(assignment.InternshipPeriod),
                IntroductionConditions = ConvertToCommaSeperatedStringOfConditions(assignment.Id),
                Street = assignment.Location.Street,
                Number = assignment.Location.Number,
                PostalNumber = assignment.Location.PostalNumber,
                Township = assignment.Location.Township,
                OtherComments = assignment.OtherComments,
                ResearchTheme = assignment.ResearchTheme,
                Specialization = ConvertToString(assignment.Specialization),
                SpecificStudentFirstAndLastName = assignment.SpecificStudentFirstAndLastName,
                TeacherStatus = DeterminTeacherStatus(assignment.TeacherStatus, assignment.Id),
                Status = DeterminStatus(assignment.Status)
            };
        }
    }
}