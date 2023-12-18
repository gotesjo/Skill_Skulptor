using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class Education
    {
        [Key]
        public int EdID { get; set; }
        public string Institution { get; set; }
        public string Course { get; set; }
        public string Degree { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int CvId { get; set; }

        [ForeignKey(nameof(CvId))]
        public virtual CV cv { get; set; }


    }
}
