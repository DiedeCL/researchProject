namespace research_project_backend.Models
{
    public class AddAssignmentModel
    {
        public int CompanyId { get; set; }
        public string Specialization { get; set; }
        public string Description { get; set; }
        public string Enviroments { get; set; } //ENviroment naam met komma tussen
        public string ExtraDescr { get; set; }
        public string Conditions { get; set; }
        public string ResearchTheme { get; set; }
        public string Street { get; set; }
        public string Township { get; set; }
        public string Postalnumber { get; set; }
        public int Number { get; set; }
        public int AmountOfSupportingEmployees { get; set; }
        public string IntroductionCondition { get; set; }// komma seperation 
        public int AmountOfInterns { get; set; }
        public string SpecificStudentFirstAndLastName { get; set; }
        public string OtherComments { get; set; }
        public string InternshipPeriod { get; set; }

    }
}