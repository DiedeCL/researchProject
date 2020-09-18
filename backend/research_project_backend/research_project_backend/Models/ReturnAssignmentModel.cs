using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace research_project_backend.Models
{
    public class ReturnAssignmentModel
    {
        public string companyName { get; set; }
        public int companyId { get; set; }
        public int amountOfInterns { get; set; }
        public int Id { get; set; }
        public int AmountOfSupportingEmployees { get; set; }
        public string Conditions { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Enviroments { get; set; }
        public string ExtraDescriptionEnvironments { get; set; }
        public string InternshipPeriod { get; set; }
        public string IntroductionConditions { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalNumber { get; set; }
        public string Township { get; set; }
        public string OtherComments { get; set; }
        public string ResearchTheme { get; set; }
        public string Specialization { get; set; }
        public string SpecificStudentFirstAndLastName { get; set; }
        public string TeacherStatus { get; set; }
        public string Status { get; set; }
    }
}
