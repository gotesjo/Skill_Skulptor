using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillSkulptor.Migrations
{
    /// <inheritdoc />
    public partial class exempeldata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Adresses",
                columns: new[] { "AdressID", "City", "Country", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, "City1", "Country1", "123 Main St", "12345" },
                    { 2, "City2", "Country2", "456 Oak St", "67890" }
                });

            migrationBuilder.InsertData(
                table: "ProfilePictures",
                columns: new[] { "PicrtureID", "Filename" },
                values: new object[,]
                {
                    { 1, "profile1.jpg" },
                    { 2, "profile2.jpg" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "UserId", "Active", "Address", "Email", "Firstname", "Lastname", "Password", "Phonenr", "Picture", "ProfileAccess" },
                values: new object[,]
                {
                    { 1, true, 1, "john.doe@example.com", "John", "Doe", "password1", "123456789", 1, true },
                    { 2, true, 2, "jane.smith@example.com", "Jane", "Smith", "password2", "987654321", 2, true }
                });

            migrationBuilder.InsertData(
                table: "CVs",
                columns: new[] { "CVID", "BelongsTo", "Clicks", "Summary" },
                values: new object[,]
                {
                    { 1, 1, 10, "Experienced professional" },
                    { 2, 2, 5, "Entry-level candidate" },
                });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "EdID", "CvId", "Degree", "EndDate", "Institution", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, "Bachelor's", new DateTime(2022, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8597), "University1", new DateTime(2019, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8506) },
                    { 2, 1, "Master's", new DateTime(2021, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8617), "University2", new DateTime(2020, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8608) },
                    { 3, 2, "Associate", new DateTime(2022, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8631), "College1", new DateTime(2021, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8625) }
                });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "ExId", "Cv", "Description", "Employer", "EndDate", "Position", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, "Worked on various projects", "Oracle", new DateTime(2022, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8714), "Developer", new DateTime(2020, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8707) },
                    { 2, 2, "Assisted with tasks", "Netbeans", new DateTime(2022, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8729), "Intern", new DateTime(2021, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8722) }
                });

            migrationBuilder.InsertData(
                table: "Qualifications",
                columns: new[] { "QID", "CvId", "Description", "QName" },
                values: new object[,]
                {
                    { 1, 1, "Certification description 1", "Certification1" },
                    { 2, 2, "Certification description 2", "Certification2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Qualifications",
                keyColumn: "QID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CVs",
                keyColumn: "CVID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "AdressID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Adresses",
                keyColumn: "AdressID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProfilePictures",
                keyColumn: "PicrtureID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProfilePictures",
                keyColumn: "PicrtureID",
                keyValue: 2);
        }
    }
}
