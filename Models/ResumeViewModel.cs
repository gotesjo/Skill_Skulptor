namespace SkillSkulptor.Models
{
    public class ResumeViewModel
    {
        public AppUser User { get; set; }
        public CV UserCV { get; set; }

        public List<Experience> Experiences { get; set; } = new List<Experience>();
        public List<Education> Educations { get; set; } = new List<Education>();
        public List<Qualification> Qualifications { get; set; } = new List<Qualification>();
        public List<Project> Projects { get; set; }
    }
}
