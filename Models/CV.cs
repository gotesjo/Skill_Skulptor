using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class CV
    {
        public int CVID { get; set; }
        public int Clicks { get; set; }
        public String Summary { get; set; }
        public int BelongsTo { get; set; }

        [ForeignKey(nameof(BelongsTo))]
        public AppUser fkUser { get; set; }

        public virtual IEnumerable<Experience> Experiences { get; set; } = new List<Experience>();
    }
}
