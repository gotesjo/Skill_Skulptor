using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class CreateExperienceModel
    {
        [Required(ErrorMessage ="Du måste skriva in vilken position du hade")]
        public string Position { get; set; }
        public string? ExDescription { get; set; }
        [Required(ErrorMessage = "Du måste skriva in vart du jobbade")]
        public string? Employer { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExEndDate { get; set; }
    }
}
