﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSkulptor.Models
{
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Phonenr { get; set; }
        public Boolean ProfileAccess { get; set; }
        public Boolean Active { get; set; }

        public int? Address { get; set; }
        public int? Picture {  get; set; }

        [ForeignKey(nameof(Address))]
        public virtual Adress fkAddress { get; set; }

        [ForeignKey(nameof(Picture))]
        public virtual Profilepicture? fkPicture { get; set; }

        public virtual IEnumerable<ProjectMembers> listProjectmembers { get; set; } = new List<ProjectMembers>();

        public virtual CV? userCV { get; set; }
        public virtual IEnumerable<Project> OwnProjects { get; set; } = new List<Project>();

		public virtual IEnumerable<Message> SentMessages { get; set; } = new List<Message>();
        public virtual IEnumerable<Message> ReceivedMessages { get; set; } = new List<Message>();
    }
}
