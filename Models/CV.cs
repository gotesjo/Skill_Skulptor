using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class CV
    {
        [Key]
        public int CVID { get; set; }
        public int Clicks { get; set; }
        public string? Summary { get; set; }
        public string? PersonalLetter {  get; set; }
        public string BelongsTo { get; set; }

        [ForeignKey(nameof(BelongsTo))]
        public virtual AppUser fkUser { get; set; }

        public virtual List<Experience> Experiences { get; set; } = new List<Experience>();
        public virtual List<Education> Educations { get; set; } = new List<Education>();
        public virtual List<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
}
