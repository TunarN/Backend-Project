using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Project.Migrations
{
    public partial class mistakeCorrectly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Teachers_TeacherId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Teachers_TeacherId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_TeacherId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_TeacherId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Contacts");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SkillsId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ContactId",
                table: "Teachers",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SkillsId",
                table: "Teachers",
                column: "SkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Contacts_ContactId",
                table: "Teachers",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Skills_SkillsId",
                table: "Teachers",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Contacts_ContactId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Skills_SkillsId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_ContactId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_SkillsId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "SkillsId",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_TeacherId",
                table: "Skills",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TeacherId",
                table: "Contacts",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Teachers_TeacherId",
                table: "Contacts",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Teachers_TeacherId",
                table: "Skills",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
