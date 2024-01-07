﻿using SkillSkulptor.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SkillSkulptor.Models
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Namn är obligatoriskt.")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Du måste ha en beskrivning")]
        [MaxLength(500, ErrorMessage = "Beskrivning får vara högst 500 tecken.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "StartDatum är obligatoriskt.")]
        [DataType(DataType.Date)]
        public DateTime Startdate { get; set; }

        [Required(ErrorMessage = "SlutDatum är obligatoriskt.")]
        [DataType(DataType.Date)]
        public DateTime Enddate { get; set; }

        [Required(ErrorMessage ="Antal är obligatoriskt")]
        public int MaxPeople { get; set; }

        public int PersonCount { get; set; } = 1;

        public string? CreatedBy { get; set; }

        public AppUser? CreatedByUser { get; set; }

        public List<ProjectMembers>? ProjectMembers { get; set; }

    }
}
