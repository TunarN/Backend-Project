using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Project.Migrations
{
    public partial class addNameNameDescColumnToCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameDesc",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NameDesc",
                table: "Courses");
        }
    }
}
