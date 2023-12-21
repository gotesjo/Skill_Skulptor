using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage ="Du måste skriva in ett användarnamn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Du måste skriva in en Emailadress")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Du måste skriva ett lösenord")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassord")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Du måste bekräfta lösenordet")]
        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta Lösenordet")]
        public string ConfirmPassord { get; set; }

        [Required(ErrorMessage = "Du måste skriva ett Förnamn")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Du måste skriva in ett Efternamn")]
        public string Lastname { get; set; }

        
    }
}
