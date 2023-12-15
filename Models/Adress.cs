using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class Adress
    {
        [Key]
        public int AdressID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string? Country { get; set; }
        public virtual AppUser resident { get; set; }

    }
}
