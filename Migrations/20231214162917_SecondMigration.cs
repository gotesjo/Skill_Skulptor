using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkillSkulptor.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Adresses_Address",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_ProfilePictures_Picture",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_Address",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_Picture",
                table: "AppUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Employer",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Picture",
                table: "AppUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Phonenr",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Address",
                table: "AppUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Address",
                table: "AppUsers",
                column: "Address",
                unique: true,
                filter: "[Address] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Picture",
                table: "AppUsers",
                column: "Picture",
                unique: true,
                filter: "[Picture] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Adresses_Address",
                table: "AppUsers",
                column: "Address",
                principalTable: "Adresses",
                principalColumn: "AdressID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_ProfilePictures_Picture",
                table: "AppUsers",
                column: "Picture",
                principalTable: "ProfilePictures",
                principalColumn: "PicrtureID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Adresses_Address",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_ProfilePictures_Picture",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_Address",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_Picture",
                table: "AppUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Employer",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Picture",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phonenr",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Address",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Adresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Address",
                table: "AppUsers",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_Picture",
                table: "AppUsers",
                column: "Picture",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Adresses_Address",
                table: "AppUsers",
                column: "Address",
                principalTable: "Adresses",
                principalColumn: "AdressID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_ProfilePictures_Picture",
                table: "AppUsers",
                column: "Picture",
                principalTable: "ProfilePictures",
                principalColumn: "PicrtureID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
