using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class CreateQualificationModel
    {
        [MaxLength(100, ErrorMessage = "Skriv en färdighet")]
        public string QName { get; set; }

        [MaxLength(200, ErrorMessage = "Du får bara skicka max 200 tecken")]
        public string QDescription { get; set; }
    }
}
