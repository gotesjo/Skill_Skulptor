using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class Profilepicture
    {
        [Key]
        public int PicrtureID { get; set; }
        public string Filename {  get; set; }
    }
}
