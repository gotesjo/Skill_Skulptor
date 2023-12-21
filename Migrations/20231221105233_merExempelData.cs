using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillSkulptor.Migrations
{
    /// <inheritdoc />
    public partial class merExempelData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "AdressID",
                keyValue: 1,
                columns: new[] { "City", "Country", "Street", "ZipCode" },
                values: new object[] { "Örebro", "Sweden", "Dragonvägen 15", "70254" });

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "AdressID",
                keyValue: 2,
                columns: new[] { "City", "Country", "Street", "ZipCode" },
                values: new object[] { "Stockholm", "Sweden", "Pastellvägen 12", "12136" });

            migrationBuilder.InsertData(
                table: "Adresses",
                columns: new[] { "AdressID", "City", "Country", "Street", "ZipCode" },
                values: new object[] { 3, "Örebro", "Sweden", "Granrisvägen 23", "70234" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "Lastname", "Phonenr" },
                values: new object[] { "john.larsson@orebrokommun.com", "Larsson", "0732341481" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Email", "Firstname", "Lastname", "Phonenr" },
                values: new object[] { "jessica.nyman@gmail.com", "Jessica", "Nyman", "0703414521" });

            migrationBuilder.UpdateData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 1,
                column: "Summary",
                value: "Jag är John Larsson, en entusiastisk webbutvecklare från Örebro. Jag studerar för tillfället systemutveckling på Örebros Universitet där jag är inne på min 4e termin. Utomhusaktiviteter är en viktig del av mitt liv, där jag under vintern älskar skidåkning och strävar efter att bli bättre varje säsong. På sommaren omfamnar jag äventyr genom mountainbikecykling, friklättring och kajakpaddling. Trots min passion för teknik och webbutveckling tar jag även tid att utforska kreativa intressen som att bli en skicklig kock och njuta av sci-fi- och fantasy-genrefilmer. Min starka drivkraft och mångsidighet gör mig till en dedikerad professionell med ögon för innovation och nytänkande.");

            migrationBuilder.UpdateData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 2,
                columns: new[] { "PersonalLetter", "Summary" },
                values: new object[] { "Med en Masterexamen i biomedicin från Karolinska institutet och en stark passion för vetenskaplig forskning, är jag ivrig att bidra till ert team med mina kunskaper och erfarenheter.Under min akademiska karriär har jag utvecklat en djup förståelse för biomedicinska processer och dess tillämpningar. Jag har arbetat med flera forskningsprojekt där jag inte bara bidragit med min vetenskapliga expertis, men också visat förmåga att arbeta effektivt i team, lösa komplexa problem och hantera tidspressade situationer.\r\nBortom min professionella sfär, ägnar jag mig åt idrott och fysisk välbefinnande. Att idrotta, särskilt löpning och simning, är inte bara min hobby, utan också ett sätt för mig att upprätthålla balans och fokus i mitt liv. Jag tror starkt på att ett hälsosamt sinne och kropp bidrar till professionell effektivitet och kreativitet.\r\nMatlagning är en annan passion jag har, där jag utforskar nya recept och smaker. Detta har lärt mig vikten av precision, tålamod och kreativitet - egenskaper som jag även tillämpar i mitt yrkesliv.\r\nMed mina vetenskapliga kompetenser och personliga intressen är jag övertygad om att jag kan tillföra värdefulla insikter.", "Jag är Jessica Nyman, en driven biomedicinsk forskare från Stockholm med en masterexamen i biomedicin från Karolinska institutet. Min karriär har varit präglad av ett starkt engagemang för vetenskaplig forskning och innovation, med fokus på att utforska nya gränser inom mitt fält. Utöver min akademiska strävan, är jag också passionerad för idrott och fysisk aktivitet. Jag tillbringar mycket av min fritid med olika sporter som löpning, simning och gruppträning, vilket inte bara håller mig fysiskt aktiv utan även mentalt fokuserad. Dessutom har jag ett stort intresse för matlagning, där jag älskar att experimentera med nya recept och smakkombinationer. Denna blandning av professionell drivkraft och personliga intressen skapar en dynamisk balans i mitt liv, vilket jag tror bidrar till min effektivitet och kreativitet både på och utanför arbetet." });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "EndDate", "Institution", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(907), "Handelshögskolan vid Örebro Universitet", new DateTime(2019, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(762) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "Course", "EndDate", "Institution", "StartDate" },
                values: new object[] { "Masterprogrammet i biomedicin", new DateTime(2021, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(927), "Karolinska institutet", new DateTime(2020, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(921) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "Course", "Degree", "EndDate", "Institution", "StartDate" },
                values: new object[] { "Psykologi", "Bachelor's", new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(936), "Uppsala Universitet", new DateTime(2021, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(932) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1,
                columns: new[] { "Description", "Employer", "EndDate", "Position", "StartDate" },
                values: new object[] { "Arbetade med utveckling av programvara och löste tekniska problem", "Nexer Group", new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1046), "Systemutvecklare", new DateTime(2020, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1041) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2,
                columns: new[] { "Description", "Employer", "EndDate", "Position", "StartDate" },
                values: new object[] { "Bidrog till banbrytande forskningsprojekt", "Biomedical Innovations Ltd.", new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1057), "Research Assistant", new DateTime(2021, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1052) });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExId", "Cv", "Description", "Employer", "EndDate", "Position", "StartDate" },
                values: new object[] { 4, 1, "Designade och implementerade mjukvarulösningar för kundprojekt", "DigitalWave Innovations", new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1076), "Applikationsutvecklare", new DateTime(2020, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1072) });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "PicrtureID", "Filename", "ImageData" },
                values: new object[] { 3, "profile3.jpg", null });

            migrationBuilder.UpdateData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 1,
                columns: new[] { "Description", "QName" },
                values: new object[] { "Validerar kunskapen i att utveckla och implementera Azure-lösningar.", "Microsoft Certified: Azure Developer Associate" });

            migrationBuilder.UpdateData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 2,
                columns: new[] { "CvId", "Description", "QName" },
                values: new object[] { 1, "Bekräftar kompetensen i Java-programmering och utveckling.", "Oracle Certified Professional, Java SE Programmer" });

            migrationBuilder.InsertData(
                table: "Qualifications",
                columns: new[] { "QID", "CvId", "Description", "QName" },
                values: new object[,]
                {
                    { 3, 1, "Ger förståelse för Scrum-principer och effektivt teamarbete.", "Certified ScrumMaster (CSM)" },
                    { 4, 1, "Bestyrker förmågan att utveckla applikationer på AWS-plattformen.", "AWS Certified Developer - Associate" },
                    { 5, 1, "Bekräftar grundläggande kunskaper inom IT-säkerhet.", "CompTIA Security+" },
                    { 11, 2, "Godkänner efterlevnad av etiska och vetenskapliga standarder inom klinisk forskning.", "Good Clinical Practice (GCP) Certification" },
                    { 12, 2, "Bekräftar kompetens inom klinisk forskning och regler.", "Certified Clinical Research Professional (CCRP)" },
                    { 13, 2, "Validerar kunskaper och färdigheter inom molekylärbiologiska metoder.", "Molecular Biology Techniques Certification" },
                    { 14, 2, "Befäster förmågan att säkert arbeta i biologiska laboratoriemiljöer.", "Biosafety Level 2 (BSL-2) Laboratory Certification" },
                    { 15, 2, "Godkänner kompetens inom kvalitetssäkring av kliniska laboratorietester.", "Clinical Laboratory Improvement Amendments (CLIA) Certification" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "UserId", "Active", "Address", "Email", "Firstname", "Lastname", "Password", "Phonenr", "Picture", "ProfileAccess" },
                values: new object[] { 3, true, 3, "david.persson@gmail.com", "David", "Persson", "password3", "0707893321", 3, true });

            migrationBuilder.InsertData(
                table: "CVs",
                columns: new[] { "CVID", "BelongsTo", "Clicks", "PersonalLetter", "Summary" },
                values: new object[] { 3, 3, 25, "Utöver min akademiska bakgrund och yrkeserfarenhet omfattar mitt liv en varierad mix av intressen och passioner. Jag har en grundutbildning inom psykologi, där jag har fördjupat mig i att förstå människors tankar och beteenden. Min tid på universitetet har gett mig insikter som jag använder för att skapa meningsfulla och användarcentrerade digitala lösningar.\r\nNär jag inte dyker in i världen av psykologi och användbarhet, finner jag glädje i att utforska konst och kreativitet. Jag har en konstnärlig sida som jag utvecklar genom att måla och delta i konstutställningar. Att skapa och uppleva konst ger mig en nödvändig kontrast till den tekniska världen.\r\nUtöver detta är jag en hängiven friluftsentusiast. Jag njuter av vandring i naturen, camping och fotografering av landskap.\r\nMin utbildning och intressen kompletterar varandra på ett sätt som berikar mitt perspektiv, vilket i sin tur påverkar hur jag närmar mig utmaningar och kreativa projekt.", "Jag är David Persson, en nyutexaminerad kandidat som är bosatt i Örebro, Sverige, med en stark akademisk bakgrund inom psykologi. Min utbildning har gett mig en djup förståelse för mänskliga tankar och beteenden, vilket jag sömlöst integrerar i skapandet av meningsfulla och användarcentrerade digitala lösningar. Utöver den tekniska världen är jag en passionerad konstnär och deltar aktivt i måleri och konstutställningar, vilket visar på min kreativa talang.\r\n\r\nFör att skapa en uppfriskande kontrast till den tekniska världen är jag också en entusiastisk naturälskare och finner glädje i vandring, camping och fotografering av vackra landskap. Denna mångsidighet präglar min karaktär och gör att jag kan närma mig utmaningar och kreativa projekt med en mångfacetterad och berikande synvinkel.\r\n\r\nInte bara är min profil tillgänglig, utan den är också aktivt engagerad, vilket återspeglar mitt starka intresse för professionell utveckling. I det dynamiska samspel mellan psykologi, teknologi och kreativitet framträder jag som en mångsidig och kompetent individ." });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExId", "Cv", "Description", "Employer", "EndDate", "Position", "StartDate" },
                values: new object[,]
                {
                    { 3, 3, "Utförde psykologiska utvärderingar och stödde klienter", "Mindscape Solutions AB", new DateTime(2023, 6, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1065), "Psykologisk Konsult", new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1062) },
                    { 5, 3, "Genomförde individuella rådgivningssessioner och utvecklade psykologiska interventionsstrategier", "MindWellness Consulting", new DateTime(2023, 4, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1087), "Psykologisk Rådgivare", new DateTime(2021, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1083) }
                });

            migrationBuilder.InsertData(
                table: "Qualifications",
                columns: new[] { "QID", "CvId", "Description", "QName" },
                values: new object[,]
                {
                    { 6, 3, "Validerar kompetens inom personalutveckling och talanghantering.", "Certified Professional in Talent Development (CPTD)" },
                    { 7, 3, "Certifierar förståelsen och tillämpningen av emotionell intelligens.", "Emotional Intelligence Appraisal Certification" },
                    { 8, 3, "Befäster kunskaper om första hjälpen vid mentala hälsoproblem.", "Certified Mental Health First Aid Responder" },
                    { 9, 3, "Bekräftar färdigheter inom beteendeanalys och konsultation.", "Certified Behavioral Consultant (CBC)" },
                    { 10, 3, "Validerar användningen av personlighetstestning för arbetsrelaterade bedömningar.", "Occupational Personality Questionnaire (OPQ) Certification" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "AdressID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProfilePictures",
                keyColumn: "PicrtureID",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "AdressID",
                keyValue: 1,
                columns: new[] { "City", "Country", "Street", "ZipCode" },
                values: new object[] { "City1", "Country1", "123 Main St", "12345" });

            migrationBuilder.UpdateData(
                table: "Adresses",
                keyColumn: "AdressID",
                keyValue: 2,
                columns: new[] { "City", "Country", "Street", "ZipCode" },
                values: new object[] { "City2", "Country2", "456 Oak St", "67890" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "Lastname", "Phonenr" },
                values: new object[] { "john.doe@example.com", "Doe", "123456789" });

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Email", "Firstname", "Lastname", "Phonenr" },
                values: new object[] { "jane.smith@example.com", "Jane", "Smith", "987654321" });

            migrationBuilder.UpdateData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 1,
                column: "Summary",
                value: "Experienced professional");

            migrationBuilder.UpdateData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 2,
                columns: new[] { "PersonalLetter", "Summary" },
                values: new object[] { "Utöver min akademiska bakgrund och yrkeserfarenhet omfattar mitt liv en varierad mix av intressen och passioner. Jag har en grundutbildning inom psykologi, där jag har fördjupat mig i att förstå människors tankar och beteenden. Min tid på universitetet har gett mig insikter som jag använder för att skapa meningsfulla och användarcentrerade digitala lösningar.\r\nNär jag inte dyker in i världen av psykologi och användbarhet, finner jag glädje i att utforska konst och kreativitet. Jag har en konstnärlig sida som jag utvecklar genom att måla och delta i konstutställningar. Att skapa och uppleva konst ger mig en nödvändig kontrast till den tekniska världen.\r\nUtöver detta är jag en hängiven friluftsentusiast. Jag njuter av vandring i naturen, camping och fotografering av landskap.\r\nMin utbildning och intressen kompletterar varandra på ett sätt som berikar mitt perspektiv, vilket i sin tur påverkar hur jag närmar mig utmaningar och kreativa projekt.", "Entry-level candidate" });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "EndDate", "Institution", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3019), "University1", new DateTime(2019, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(2505) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "Course", "EndDate", "Institution", "StartDate" },
                values: new object[] { "Systemutveckling", new DateTime(2021, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3076), "University2", new DateTime(2020, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3031) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "Course", "Degree", "EndDate", "Institution", "StartDate" },
                values: new object[] { "Systemutveckling", "Associate", new DateTime(2022, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3120), "College1", new DateTime(2021, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3096) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1,
                columns: new[] { "Description", "Employer", "EndDate", "Position", "StartDate" },
                values: new object[] { "Worked on various projects", "Company1", new DateTime(2022, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3226), "Developer", new DateTime(2020, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3222) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2,
                columns: new[] { "Description", "Employer", "EndDate", "Position", "StartDate" },
                values: new object[] { "Assisted with tasks", "Company2", new DateTime(2022, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3235), "Intern", new DateTime(2021, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3232) });

            migrationBuilder.UpdateData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 1,
                columns: new[] { "Description", "QName" },
                values: new object[] { "Certification description 1", "Certification1" });

            migrationBuilder.UpdateData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 2,
                columns: new[] { "CvId", "Description", "QName" },
                values: new object[] { 2, "Certification description 2", "Certification2" });
        }
    }
}
