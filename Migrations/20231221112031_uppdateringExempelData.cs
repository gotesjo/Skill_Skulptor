using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSkulptor.Migrations
{
    /// <inheritdoc />
    public partial class uppdateringExempelData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1694), new DateTime(2019, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1466) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "CvId", "EndDate", "StartDate" },
                values: new object[] { 2, new DateTime(2021, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1715), new DateTime(2020, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1708) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "CvId", "EndDate", "StartDate" },
                values: new object[] { 3, new DateTime(2022, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1726), new DateTime(2021, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1720) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1822), new DateTime(2020, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1818) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1837), new DateTime(2021, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1833) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 6, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1846), new DateTime(2022, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1842) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1862), new DateTime(2020, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1858) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 4, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1871), new DateTime(2021, 12, 21, 12, 20, 30, 416, DateTimeKind.Local).AddTicks(1868) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(907), new DateTime(2019, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(762) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "CvId", "EndDate", "StartDate" },
                values: new object[] { 1, new DateTime(2021, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(927), new DateTime(2020, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(921) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "CvId", "EndDate", "StartDate" },
                values: new object[] { 2, new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(936), new DateTime(2021, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(932) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1046), new DateTime(2020, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1041) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1057), new DateTime(2021, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1052) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 6, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1065), new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1062) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1076), new DateTime(2020, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1072) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 5,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 4, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1087), new DateTime(2021, 12, 21, 11, 52, 31, 857, DateTimeKind.Local).AddTicks(1083) });
        }
    }
}
