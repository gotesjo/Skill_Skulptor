using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class CreateResumeModel
    {

        [Required]
        [MaxLength(200, ErrorMessage = "Du får bara skicka max 200 tecken")]
        public string? Summary { get; set; }

        [MaxLength(500, ErrorMessage = "Du får bara skicka max 500 tecken")]
        public string? PersonalLetter { get; set; }

        //Education
        public CreateEducationModel Education { get; set; }

        //Experience
        public CreateExperienceModel Experience { get; set; }

        //Qualification
        public CreateQualificationModel Qualification { get; set; }
    }
}
