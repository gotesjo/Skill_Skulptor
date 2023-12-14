using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class Experience
    {
        public int ExId { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
        public string Employer { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Cv { get; set; }

        [ForeignKey(nameof(Cv))]
        public virtual CV fkCv { get; set; }
    }
}
