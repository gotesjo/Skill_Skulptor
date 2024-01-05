using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage = "Du måste skriva ett lösenord")]
        [DataType(DataType.Password)]
        public String OldPassword { get; set; }

        [Required(ErrorMessage = "Du måste skriva ett lösenord")]
        [MinLength(8, ErrorMessage = "Lösenordet är för kort")]
        [MaxLength(20, ErrorMessage = "Lösenordet är för långt")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage ="Lösenorden matchar inte varandra")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Du måste bekräfta lösenordet")]
        [MinLength(8, ErrorMessage = "Lösenordet är för kort")]
        [MaxLength(20, ErrorMessage = "Lösenordet är för långt")]
        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta Lösenordet")]
        public string ConfirmPassword { get; set; }
    }
}
