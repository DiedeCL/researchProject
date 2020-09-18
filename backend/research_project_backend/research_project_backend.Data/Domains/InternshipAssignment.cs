using System;
using System.Collections.Generic;
using research_project_backend.Data.Domains.Enums;

namespace research_project_backend.Data.Domains
{
    public class InternshipAssignment
    {
        public InternshipAssignment() { 
            
            Status = Status.Behandeling;
            DateCreated = DateTime.Now;
            TeacherStatus = TeacherStatus.Behandeling;
        }
        public int Id { get; set; }
        public Specialization Specialization { get; set; }
        public string Description { get; set; }
        public List<Environment> Environments { get; set; } // EF core kan geen primitive datatypes opslaan --> moet hiervoor een nieuwe table aan maken
        public string ExtraDescriptionEnvironments { get; set; }
        public string Conditions { get; set; }
        public string ResearchTheme { get; set; }
        public Address Location { get; set; }
        public int AmountOfSupportingEmployees { get; set; }
        public List<IntroductionCondition> IntroductionConditions { get; set; }
        public int AmountOfInterns { get; set; }
        public string SpecificStudentFirstAndLastName { get; set; }
        public string OtherComments { get; set; }
        public InternshipPeriod InternshipPeriod { get; set; }
        public Status Status { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public DateTime DateCreated { get; set; }
        public TeacherStatus TeacherStatus { get; set; }


    }
}