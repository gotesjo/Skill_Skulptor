using Microsoft.Extensions.Configuration.UserSecrets;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class ProjectMembers
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser fkUser { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public Project fkProject { get; set; }
    }
}
