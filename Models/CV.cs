using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class CV
    {
        [Key]
        public int CVID { get; set; }
        public int Clicks { get; set; }
        public String Summary { get; set; }
        public int BelongsTo { get; set; }

        [ForeignKey(nameof(BelongsTo))]
        public virtual AppUser fkUser { get; set; }

        public virtual IEnumerable<Experience> Experiences { get; set; } = new List<Experience>();
        public virtual IEnumerable<Education> Educations { get; set; } = new List<Education>();
        public virtual IEnumerable<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
}
