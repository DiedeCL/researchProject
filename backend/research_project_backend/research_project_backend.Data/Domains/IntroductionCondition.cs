namespace research_project_backend.Data.Domains
{
    public class IntroductionCondition
    {
        public int Id { get; set; }
        public string Condition { get; set; }
        public InternshipAssignment InternshipAssignment { get; set; }
        public int InternshipAssignmentId { get; set; }
    }
}