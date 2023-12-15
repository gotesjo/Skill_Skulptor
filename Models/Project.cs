using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public int CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual AppUser CreatedByUser { get; set; }

        public virtual IEnumerable<ProjectMembers> listProjectmembers { get; set; } = new List<ProjectMembers>();

    }
}
