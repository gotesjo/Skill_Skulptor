using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class CreateQualificationModel
    {
        public int? QID { get; set; }

        [Required(ErrorMessage ="Du måste ha en färdighet")]
        [MaxLength(100, ErrorMessage = "Får max vara 100 tecken")]
        public string? QName { get; set; }

        [MaxLength(200, ErrorMessage = "Du får bara skicka max 200 tecken")]
        public string? QDescription { get; set; }
    }
}
