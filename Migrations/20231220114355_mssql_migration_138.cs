using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSkulptor.Migrations
{
    /// <inheritdoc />
    public partial class mssql_migration_138 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AppUsers_CreatedBy",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3019), new DateTime(2019, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(2505) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2021, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3076), new DateTime(2020, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3031) });

            migrationBuilder.UpdateData(
                table: "Educations",
                keyColumn: "EdID",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3120), new DateTime(2021, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3096) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3226), new DateTime(2020, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3222) });

            migrationBuilder.UpdateData(
                table: "Experiences",
                keyColumn: "ExId",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2022, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3235), new DateTime(2021, 12, 20, 12, 43, 53, 434, DateTimeKind.Local).AddTicks(3232) });

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AppUsers_CreatedBy",
                table: "Projects",
                column: "CreatedBy",
                principalTable: "AppUsers",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AppUsers_CreatedBy",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AppUsers_CreatedBy",
                table: "Projects",
                column: "CreatedBy",
                principalTable: "AppUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
