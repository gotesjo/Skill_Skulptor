using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class CreateExperienceModel
    {
        public string Position { get; set; }
        public string? ExDescription { get; set; }
        public string? Employer { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExEndDate { get; set; }
    }
}
