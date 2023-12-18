using Microsoft.EntityFrameworkCore;

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

            modelBuilder.Entity<ProjectMembers>()
                .HasOne(pj => pj.Project)
                .WithMany(p => p.listProjectmembers)
                .HasForeignKey(pj => pj.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProjectMembers>()
                .HasOne(pj => pj.User)
                .WithMany(u => u.listProjectmembers)
                .HasForeignKey(pj => pj.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Startup data
            modelBuilder.Entity<Adress>().HasData(
                new Adress { AdressID = 1, Street = "123 Main St", City = "City1", ZipCode = "12345", Country = "Country1" },
                new Adress { AdressID = 2, Street = "456 Oak St", City = "City2", ZipCode = "67890", Country = "Country2" }
            );

            modelBuilder.Entity<Profilepicture>().HasData(
                new Profilepicture { PicrtureID = 1, Filename = "profile1.jpg" },
                new Profilepicture { PicrtureID = 2, Filename = "profile2.jpg" }
            );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser { UserId = 1, Firstname = "John", Lastname = "Doe", Email = "john.doe@example.com", Password = "password1", Phonenr = "123456789", ProfileAccess = true, Active = true, Address = 1, Picture = 1 },
                new AppUser { UserId = 2, Firstname = "Jane", Lastname = "Smith", Email = "jane.smith@example.com", Password = "password2", Phonenr = "987654321", ProfileAccess = true, Active = true, Address = 2, Picture = 2 }
            );

            modelBuilder.Entity<CV>().HasData(
                new CV { CVID = 3, Clicks = 10, Summary = "Experienced professional", BelongsTo = 1 },
                new CV { CVID = 4, Clicks = 5, Summary = "Entry-level candidate", BelongsTo = 2 }
            );

            modelBuilder.Entity<CV>().HasData(
                new CV { CVID = 1, Clicks = 10, Summary = "Experienced professional", BelongsTo = 1 },
                new CV { CVID = 2, Clicks = 5, Summary = "Entry-level candidate", BelongsTo = 2 }
             );

            modelBuilder.Entity<Education>().HasData(
                new Education { EdID = 1, Institution = "University1", Degree = "Bachelor's", StartDate = DateTime.Now.AddYears(-4), EndDate = DateTime.Now.AddYears(-1), CvId = 1 },
                new Education { EdID = 2, Institution = "University2", Degree = "Master's", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-2), CvId = 1 },
                new Education { EdID = 3, Institution = "College1", Degree = "Associate", StartDate = DateTime.Now.AddYears(-2), EndDate = DateTime.Now.AddYears(-1), CvId = 2 }
            );

            modelBuilder.Entity<Experience>().HasData(
                new Experience { ExId = 1, Position = "Developer", Description = "Worked on various projects", Employer = "Company1", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-1), Cv = 1 },
                new Experience { ExId = 2, Position = "Intern", Description = "Assisted with tasks", Employer = "Company2", StartDate = DateTime.Now.AddYears(-2), EndDate = DateTime.Now.AddYears(-1), Cv = 2 }
            );

            modelBuilder.Entity<Qualification>().HasData(
                new Qualification { QID = 1, QName = "Certification1", Description = "Certification description 1", CvId = 1 },
                new Qualification { QID = 2, QName = "Certification2", Description = "Certification description 2", CvId = 2 }
            );
        }



    }
}
