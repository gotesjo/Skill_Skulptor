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
                new Adress { AdressID = 1, Street = "Dragonvägen 15", City = "Örebro", ZipCode = "70254", Country = "Sweden" },
                new Adress { AdressID = 2, Street = "Pastellvägen 12", City = "Stockholm", ZipCode = "12136", Country = "Sweden" },
                new Adress { AdressID = 3, Street = "Granrisvägen 23", City = "Örebro", ZipCode = "70234", Country = "Sweden" }

            );

            modelBuilder.Entity<Profilepicture>().HasData(
                new Profilepicture { PicrtureID = 1, Filename = "profile1.jpg" },
                new Profilepicture { PicrtureID = 2, Filename = "profile2.jpg" },
                new Profilepicture { PicrtureID = 3, Filename = "profile3.jpg" }

            );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser { UserId = 1, Firstname = "John", Lastname = "Larsson", Email = "john.larsson@orebrokommun.com", Password = "password1", Phonenr = "0732341481", ProfileAccess = true, Active = true, Address = 1, Picture = 1 },
                new AppUser { UserId = 2, Firstname = "Jessica", Lastname = "Nyman", Email = "jessica.nyman@gmail.com", Password = "password2", Phonenr = "0703414521", ProfileAccess = true, Active = true, Address = 2, Picture = 2 },
                new AppUser { UserId = 3, Firstname = "David", Lastname = "Persson", Email = "david.persson@gmail.com", Password = "password3", Phonenr = "0707893321", ProfileAccess = true, Active = true, Address = 3, Picture = 3 }

            );

            modelBuilder.Entity<CV>().HasData(
                new CV { CVID = 1, Clicks = 10, 
                        Summary = "Jag är John Larsson, en entusiastisk webbutvecklare från Örebro. Jag studerar för tillfället systemutveckling på Örebros Universitet där jag är inne på min 4e termin. Utomhusaktiviteter är en viktig del av mitt liv, där jag under vintern älskar skidåkning och strävar efter att bli bättre varje säsong. På sommaren omfamnar jag äventyr genom mountainbikecykling, friklättring och kajakpaddling. Trots min passion för teknik och webbutveckling tar jag även tid att utforska kreativa intressen som att bli en skicklig kock och njuta av sci-fi- och fantasy-genrefilmer. Min starka drivkraft och mångsidighet gör mig till en dedikerad professionell med ögon för innovation och nytänkande.",
                        PersonalLetter= "Förutom att vara webbutvecklare tycker jag mest om att vara utomhus. På vintern är jag en ivrig skidåkare och nybörjare. På sommaren tycker jag om att cykla mountainbike, friklättra och paddla kajak.\r\nNär jag tvingas inomhus följer jag ett antal sci-fi- och fantasy-genrefilmer och tv-program, jag är en blivande kock, och jag spenderar en stor del av min fritid på att utforska de senaste tekniska framstegen i front-end webbutvecklingsvärlden.", BelongsTo = 1 },
                new CV { CVID = 2, Clicks = 5, 
                        Summary = "Jag är Jessica Nyman, en driven biomedicinsk forskare från Stockholm med en masterexamen i biomedicin från Karolinska institutet. Min karriär har varit präglad av ett starkt engagemang för vetenskaplig forskning och innovation, med fokus på att utforska nya gränser inom mitt fält. Utöver min akademiska strävan, är jag också passionerad för idrott och fysisk aktivitet. Jag tillbringar mycket av min fritid med olika sporter som löpning, simning och gruppträning, vilket inte bara håller mig fysiskt aktiv utan även mentalt fokuserad. Dessutom har jag ett stort intresse för matlagning, där jag älskar att experimentera med nya recept och smakkombinationer. Denna blandning av professionell drivkraft och personliga intressen skapar en dynamisk balans i mitt liv, vilket jag tror bidrar till min effektivitet och kreativitet både på och utanför arbetet.",
                        PersonalLetter= "Med en Masterexamen i biomedicin från Karolinska institutet och en stark passion för vetenskaplig forskning, är jag ivrig att bidra till ert team med mina kunskaper och erfarenheter.Under min akademiska karriär har jag utvecklat en djup förståelse för biomedicinska processer och dess tillämpningar. Jag har arbetat med flera forskningsprojekt där jag inte bara bidragit med min vetenskapliga expertis, men också visat förmåga att arbeta effektivt i team, lösa komplexa problem och hantera tidspressade situationer.\r\nBortom min professionella sfär, ägnar jag mig åt idrott och fysisk välbefinnande. Att idrotta, särskilt löpning och simning, är inte bara min hobby, utan också ett sätt för mig att upprätthålla balans och fokus i mitt liv. Jag tror starkt på att ett hälsosamt sinne och kropp bidrar till professionell effektivitet och kreativitet.\r\nMatlagning är en annan passion jag har, där jag utforskar nya recept och smaker. Detta har lärt mig vikten av precision, tålamod och kreativitet - egenskaper som jag även tillämpar i mitt yrkesliv.\r\nMed mina vetenskapliga kompetenser och personliga intressen är jag övertygad om att jag kan tillföra värdefulla insikter."
                        ,  BelongsTo = 2 
                   },
                  new CV
                  {
                      CVID = 3,
                      Clicks = 25,
                      Summary = "Jag är David Persson, en nyutexaminerad kandidat som är bosatt i Örebro, Sverige, med en stark akademisk bakgrund inom psykologi. Min utbildning har gett mig en djup förståelse för mänskliga tankar och beteenden, vilket jag sömlöst integrerar i skapandet av meningsfulla och användarcentrerade digitala lösningar. Utöver den tekniska världen är jag en passionerad konstnär och deltar aktivt i måleri och konstutställningar, vilket visar på min kreativa talang.\r\n\r\nFör att skapa en uppfriskande kontrast till den tekniska världen är jag också en entusiastisk naturälskare och finner glädje i vandring, camping och fotografering av vackra landskap. Denna mångsidighet präglar min karaktär och gör att jag kan närma mig utmaningar och kreativa projekt med en mångfacetterad och berikande synvinkel.\r\n\r\nInte bara är min profil tillgänglig, utan den är också aktivt engagerad, vilket återspeglar mitt starka intresse för professionell utveckling. I det dynamiska samspel mellan psykologi, teknologi och kreativitet framträder jag som en mångsidig och kompetent individ.",
                      PersonalLetter = "Utöver min akademiska bakgrund och yrkeserfarenhet omfattar mitt liv en varierad mix av intressen och passioner. Jag har en grundutbildning inom psykologi, där jag har fördjupat mig i att förstå människors tankar och beteenden. Min tid på universitetet har gett mig insikter som jag använder för att skapa meningsfulla och användarcentrerade digitala lösningar.\r\nNär jag inte dyker in i världen av psykologi och användbarhet, finner jag glädje i att utforska konst och kreativitet. Jag har en konstnärlig sida som jag utvecklar genom att måla och delta i konstutställningar. Att skapa och uppleva konst ger mig en nödvändig kontrast till den tekniska världen.\r\nUtöver detta är jag en hängiven friluftsentusiast. Jag njuter av vandring i naturen, camping och fotografering av landskap.\r\nMin utbildning och intressen kompletterar varandra på ett sätt som berikar mitt perspektiv, vilket i sin tur påverkar hur jag närmar mig utmaningar och kreativa projekt."
                        ,
                      BelongsTo = 3
                  }
            );

     

            modelBuilder.Entity<Education>().HasData(
                new Education { EdID = 1, Institution = "Handelshögskolan vid Örebro Universitet",Course ="Systemutveckling", Degree = "Bachelor's", StartDate = DateTime.Now.AddYears(-4), EndDate = DateTime.Now.AddYears(-1), CvId = 1 },
                new Education { EdID = 2, Institution = "Karolinska institutet", Course = "Masterprogrammet i biomedicin", Degree = "Master's", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-2), CvId = 2 },
                new Education { EdID = 3, Institution = "Uppsala Universitet", Course = "Psykologi", Degree = "Bachelor's", StartDate = DateTime.Now.AddYears(-2), EndDate = DateTime.Now.AddYears(-1), CvId = 3 }
            );

            modelBuilder.Entity<Experience>().HasData(
                new Experience { ExId = 1, Position = "Systemutvecklare", Description = "Arbetade med utveckling av programvara och löste tekniska problem", Employer = "Nexer Group", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddYears(-1), Cv = 1 },
                new Experience { ExId = 2, Position = "Research Assistant", Description = "Bidrog till banbrytande forskningsprojekt", Employer = "Biomedical Innovations Ltd.", StartDate = DateTime.Now.AddYears(-2), EndDate = DateTime.Now.AddYears(-1), Cv = 2 },
                new Experience { ExId = 3, Position = "Psykologisk Konsult", Description = "Utförde psykologiska utvärderingar och stödde klienter", Employer = "Mindscape Solutions AB", StartDate = DateTime.Now.AddYears(-1), EndDate = DateTime.Now.AddMonths(-6), Cv = 3 },
                new Experience { ExId = 4, Position = "Applikationsutvecklare", Description = "Designade och implementerade mjukvarulösningar för kundprojekt", Employer = "DigitalWave Innovations", StartDate = DateTime.Now.AddYears(-3), EndDate = DateTime.Now.AddMonths(-12), Cv = 1 },
                new Experience { ExId = 5, Position = "Psykologisk Rådgivare", Description = "Genomförde individuella rådgivningssessioner och utvecklade psykologiska interventionsstrategier", Employer = "MindWellness Consulting", StartDate = DateTime.Now.AddYears(-2), EndDate = DateTime.Now.AddMonths(-8), Cv = 3 }


                );

            modelBuilder.Entity<Qualification>().HasData(
                new Qualification { QID = 1, QName = "Microsoft Certified: Azure Developer Associate", Description = "Validerar kunskapen i att utveckla och implementera Azure-lösningar.", CvId = 1 },
                new Qualification { QID = 2, QName = "Oracle Certified Professional, Java SE Programmer", Description = "Bekräftar kompetensen i Java-programmering och utveckling.", CvId = 1 },
                new Qualification { QID = 3, QName = "Certified ScrumMaster (CSM)", Description = "Ger förståelse för Scrum-principer och effektivt teamarbete.", CvId = 1 },
                new Qualification { QID = 4, QName = "AWS Certified Developer - Associate", Description = "Bestyrker förmågan att utveckla applikationer på AWS-plattformen.", CvId = 1 },
                new Qualification { QID = 5, QName = "CompTIA Security+", Description = "Bekräftar grundläggande kunskaper inom IT-säkerhet.", CvId = 1 },
                new Qualification { QID = 6, QName = "Certified Professional in Talent Development (CPTD)", Description = "Validerar kompetens inom personalutveckling och talanghantering.", CvId = 3 },
                new Qualification { QID = 7, QName = "Emotional Intelligence Appraisal Certification", Description = "Certifierar förståelsen och tillämpningen av emotionell intelligens.", CvId = 3 },
                new Qualification { QID = 8, QName = "Certified Mental Health First Aid Responder", Description = "Befäster kunskaper om första hjälpen vid mentala hälsoproblem.", CvId = 3 },
                new Qualification { QID = 9, QName = "Certified Behavioral Consultant (CBC)", Description = "Bekräftar färdigheter inom beteendeanalys och konsultation.", CvId = 3 },
                new Qualification { QID = 10, QName = "Occupational Personality Questionnaire (OPQ) Certification", Description = "Validerar användningen av personlighetstestning för arbetsrelaterade bedömningar.", CvId = 3 },
                new Qualification { QID = 11, QName = "Good Clinical Practice (GCP) Certification", Description = "Godkänner efterlevnad av etiska och vetenskapliga standarder inom klinisk forskning.", CvId = 2 },
                new Qualification { QID = 12, QName = "Certified Clinical Research Professional (CCRP)", Description = "Bekräftar kompetens inom klinisk forskning och regler.", CvId = 2 },
                new Qualification { QID = 13, QName = "Molecular Biology Techniques Certification", Description = "Validerar kunskaper och färdigheter inom molekylärbiologiska metoder.", CvId = 2 },
                new Qualification { QID = 14, QName = "Biosafety Level 2 (BSL-2) Laboratory Certification", Description = "Befäster förmågan att säkert arbeta i biologiska laboratoriemiljöer.", CvId = 2 },
                new Qualification { QID = 15, QName = "Clinical Laboratory Improvement Amendments (CLIA) Certification", Description = "Godkänner kompetens inom kvalitetssäkring av kliniska laboratorietester.", CvId = 2 }



            );
        }



    }
}
