using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSkulptor.Migrations
{
    /// <inheritdoc />
    public partial class addImageData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Startdate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Enddate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "ProfilePictures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "ProfilePictures",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5175), new DateTime(2019, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5065) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2021, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5183), new DateTime(2020, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5190), new DateTime(2021, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5187) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5239), new DateTime(2020, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5235) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5245), new DateTime(2021, 12, 19, 12, 21, 36, 656, DateTimeKind.Local).AddTicks(5243) });

            migrationBuilder.UpdateData(
                table: "ProfilePictures",
                keyColumn: "PicrtureID",
                keyValue: 1,
                column: "ImageData",
                value: null);

            migrationBuilder.UpdateData(
                table: "ProfilePictures",
                keyColumn: "PicrtureID",
                keyValue: 2,
                column: "ImageData",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "ProfilePictures");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Startdate",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Enddate",
                table: "Projects",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Filename",
                table: "ProfilePictures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
