﻿using Microsoft.EntityFrameworkCore;

namespace SkillSkulptor.Models
{
    public class SsDbContext : DbContext
    {
        public SsDbContext(DbContextOptions<SsDbContext> options) : base(options) { 
        }

        public DbSet<Adress> Adresses { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<CV> CVs { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Profilepicture> ProfilePictures { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMembers> ProjectMembers { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Message>()
            .HasOne(m => m.fkFromUser)
            .WithMany(u => u.SentMessages) 
            .HasForeignKey(m => m.FromUserID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
            .HasOne(m => m.fkToUser)
            .WithMany(u => u.ReceivedMessages) 
            .HasForeignKey(m => m.ToUserID)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProjectMembers>()
                .HasKey(pj => new { pj.UserId, pj.ProjectId });

            // Eventuellt annan konfiguration för andra relationer här...
        }



    }
}
