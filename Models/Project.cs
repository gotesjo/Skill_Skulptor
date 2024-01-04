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
        [Required(ErrorMessage ="Du måste ha en beskrivning")]
        [MaxLength(500, ErrorMessage = "Beskrivning får vara högst 500 tecken.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "StartDatum är obligatoriskt.")]
        [DataType(DataType.Date)]
        public DateTime? Startdate { get; set; }
        [Required(ErrorMessage = "SlutDatum är obligatoriskt.")]
        [DataType(DataType.Date)]
        public DateTime? Enddate { get; set; }

        public int MaxPeople { get; set; }
        public string? CreatedBy { get; set; } 

        [ForeignKey(nameof(CreatedBy))]
        public virtual AppUser? CreatedByUser { get; set; }

        public virtual IEnumerable<ProjectMembers> listProjectmembers { get; set; } = new List<ProjectMembers>();

    }
}
