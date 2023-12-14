using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class Qualification
    {
        [Key]
        public int QID { get; set; }
        public string QName { get; set; }
        public string Description { get; set; }
        public int CvId { get; set; }

        [ForeignKey(nameof(CvId))]
        public virtual CV fkCv { get; set; }
    }
}
