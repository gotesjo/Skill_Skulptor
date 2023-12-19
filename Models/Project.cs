using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Namn är obligatoriskt.")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Beskrivning är obligatoriskt.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "StartDatum är obligatoriskt.")]
        public DateTime? Startdate { get; set; }
        [Required(ErrorMessage = "SlutDatum är obligatoriskt.")]
        public DateTime? Enddate { get; set; }
        public int CreatedBy { get; set; } = 1;

        [ForeignKey(nameof(CreatedBy))]
        public virtual AppUser CreatedByUser { get; set; }

        public virtual IEnumerable<ProjectMembers> listProjectmembers { get; set; } = new List<ProjectMembers>();

    }
}
