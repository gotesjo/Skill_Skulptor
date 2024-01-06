using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class ProfileViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Du måste ha ett förnamn")]
        [StringLength(20, ErrorMessage = "Postnummer får inte vara längre än 20 bokstäver")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Du måste ha ett efternamn")]
        [StringLength(20, ErrorMessage = "Postnummer får inte vara längre än 20 bokstäver")]
        public string Lastname { get; set; }

        [Phone(ErrorMessage = "Du måste ange telefonnumret korrekt")]
        public string? Phonenr { get; set; }
        [Required(ErrorMessage ="Du måste ha ett email")]
        [EmailAddress(ErrorMessage ="Du måste ange en giltig email")]
        public string Email { get; set; }

        public Boolean ProfileAccess { get; set; }

        public Boolean Active { get; set; }

        [Display(Name = "Gatuadress")]
        [StringLength(30, ErrorMessage = "Postnummer får inte vara längre än 30 bokstäver")]
        public string? Street { get; set; }

        [Display(Name = "Stad")]
        [StringLength(20, ErrorMessage = "Postnummer får inte vara längre än 20 bokstäver")]
        public string? City { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Du får bara använda siffror")]
        [StringLength(5, ErrorMessage = "Postnummer får inte vara längre än 5 siffror")]
        public string? ZipCode { get; set; }

        [Display(Name = "Land")]
        [StringLength(15, ErrorMessage = "Postnummer får inte vara längre än 15 bokstäver")]
        public string? Country { get; set; }

    }
}
