using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSkulptor.Migrations
{
    /// <inheritdoc />
    public partial class addEducation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "Educations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "Course", "EndDate", "StartDate" },
                values: new object[] { "Systemutveckling", new DateTime(2022, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9154), new DateTime(2019, 12, 18, 14, 40, 59, 445, DateTimeKind.Local).AddTicks(377) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "Course", "EndDate", "StartDate" },
                values: new object[] { "Systemutveckling", new DateTime(2021, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9212), new DateTime(2020, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9201) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "Course", "EndDate", "StartDate" },
                values: new object[] { "Systemutveckling", new DateTime(2022, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9234), new DateTime(2021, 12, 18, 14, 40, 59, 446, DateTimeKind.Local).AddTicks(9225) });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Course",
                table: "Educations");

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8597), new DateTime(2019, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8506) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2021, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8617), new DateTime(2020, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8608) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8631), new DateTime(2021, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8625) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8714), new DateTime(2020, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8707) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8729), new DateTime(2021, 12, 15, 17, 30, 50, 919, DateTimeKind.Local).AddTicks(8722) });
        }
    }
}
