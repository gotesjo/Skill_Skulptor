using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class CreateEducationModel
    {
        [MaxLength(100, ErrorMessage = "Du får bara skicka max 200 tecken")]
        public string Institution { get; set; }

        [MaxLength(100, ErrorMessage = "Du får bara skicka max 200 tecken")]
        public string Course { get; set; }

        [MaxLength(100, ErrorMessage = "Du får bara skicka max 200 tecken")]
        public string Degree { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EdStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EdEndDate { get; set; }
    }
}
