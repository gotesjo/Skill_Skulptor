using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class ProjectMembers
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }
    }
}
