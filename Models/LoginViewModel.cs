using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Extensions.Logging;
using SkillSkulptor.Models;
using static System.Collections.Specialized.BitVector32;

namespace SkillSkulptor.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vänligen skriv in ditt Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vänligen skriv in ditt lösenord")]
        public string Password { get; set; }
        public bool RemeberMe { get; set; }

    }
}


