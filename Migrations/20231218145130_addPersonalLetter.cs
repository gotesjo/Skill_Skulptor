using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillSkulptor.Migrations
{
    /// <inheritdoc />
    public partial class addPersonalLetter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "CVs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PersonalLetter",
                table: "CVs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 1,
                column: "PersonalLetter",
                value: "Förutom att vara webbutvecklare tycker jag mest om att vara utomhus. På vintern är jag en ivrig skidåkare och nybörjare. På sommaren tycker jag om att cykla mountainbike, friklättra och paddla kajak.\r\nNär jag tvingas inomhus följer jag ett antal sci-fi- och fantasy-genrefilmer och tv-program, jag är en blivande kock, och jag spenderar en stor del av min fritid på att utforska de senaste tekniska framstegen i front-end webbutvecklingsvärlden.");

            migrationBuilder.UpdateData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 2,
                column: "PersonalLetter",
                value: "Utöver min akademiska bakgrund och yrkeserfarenhet omfattar mitt liv en varierad mix av intressen och passioner. Jag har en grundutbildning inom psykologi, där jag har fördjupat mig i att förstå människors tankar och beteenden. Min tid på universitetet har gett mig insikter som jag använder för att skapa meningsfulla och användarcentrerade digitala lösningar.\r\nNär jag inte dyker in i världen av psykologi och användbarhet, finner jag glädje i att utforska konst och kreativitet. Jag har en konstnärlig sida som jag utvecklar genom att måla och delta i konstutställningar. Att skapa och uppleva konst ger mig en nödvändig kontrast till den tekniska världen.\r\nUtöver detta är jag en hängiven friluftsentusiast. Jag njuter av vandring i naturen, camping och fotografering av landskap.\r\nMin utbildning och intressen kompletterar varandra på ett sätt som berikar mitt perspektiv, vilket i sin tur påverkar hur jag närmar mig utmaningar och kreativa projekt.");

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5561), new DateTime(2019, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5306) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2021, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5579), new DateTime(2020, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5572) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5593), new DateTime(2021, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5587) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5685), new DateTime(2020, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5679) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5699), new DateTime(2021, 12, 18, 15, 51, 29, 208, DateTimeKind.Local).AddTicks(5693) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonalLetter",
                table: "CVs");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "CVs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "CVs",
                columns: new[] { "CVID", "BelongsTo", "Clicks", "Summary" },
                values: new object[,]
                {
                    { 3, 1, 10, "Experienced professional" },
                    { 4, 2, 5, "Entry-level candidate" }
                });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9154), new DateTime(2019, 12, 18, 14, 40, 59, 445, DateTimeKind.Local).AddTicks(377) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2021, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9212), new DateTime(2020, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9201) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9234), new DateTime(2021, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9225) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9598), new DateTime(2020, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9566) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 18, 14, 40, 59, 447, DateTimeKind.Local).AddTicks(1628), new DateTime(2021, 12, 18, 14, 40, 59, 447, DateTimeKind.Local).AddTicks(1592) });
        }
    }
}
