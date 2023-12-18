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
                new CV { CVID = 1, Clicks = 10, 
                        Summary = "Experienced professional",
                        PersonalLetter= "Förutom att vara webbutvecklare tycker jag mest om att vara utomhus. På vintern är jag en ivrig skidåkare och nybörjare. På sommaren tycker jag om att cykla mountainbike, friklättra och paddla kajak.\r\nNär jag tvingas inomhus följer jag ett antal sci-fi- och fantasy-genrefilmer och tv-program, jag är en blivande kock, och jag spenderar en stor del av min fritid på att utforska de senaste tekniska framstegen i front-end webbutvecklingsvärlden.", BelongsTo = 1 },
                new CV { CVID = 2, Clicks = 5, 
                        Summary = "Entry-level candidate",
                        PersonalLetter= "Utöver min akademiska bakgrund och yrkeserfarenhet omfattar mitt liv en varierad mix av intressen och passioner. Jag har en grundutbildning inom psykologi, där jag har fördjupat mig i att förstå människors tankar och beteenden. Min tid på universitetet har gett mig insikter som jag använder för att skapa meningsfulla och användarcentrerade digitala lösningar.\r\nNär jag inte dyker in i världen av psykologi och användbarhet, finner jag glädje i att utforska konst och kreativitet. Jag har en konstnärlig sida som jag utvecklar genom att måla och delta i konstutställningar. Att skapa och uppleva konst ger mig en nödvändig kontrast till den tekniska världen.\r\nUtöver detta är jag en hängiven friluftsentusiast. Jag njuter av vandring i naturen, camping och fotografering av landskap.\r\nMin utbildning och intressen kompletterar varandra på ett sätt som berikar mitt perspektiv, vilket i sin tur påverkar hur jag närmar mig utmaningar och kreativa projekt."
                        ,  BelongsTo = 2 
                   }
            );

     

            modelBuilder.Entity<Education>().HasData(
                new Education { EdID = 1, Institution = "University1",Course ="Systemutveckling", Degree = "Bachelor's", StartDate = DateTime.Now.AddYears(-4), EndDate = DateTime.Now.AddYears(-1), CvId = 1 },
                new Education { EdID = 2, Institution = "University2", Course = "Systemutveckling", Degree = "Master's", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-2), CvId = 1 },
                new Education { EdID = 3, Institution = "College1", Course = "Systemutveckling", Degree = "Associate", StartDate = DateTime.Now.AddYears(-2), EndDate = DateTime.Now.AddYears(-1), CvId = 2 }
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
