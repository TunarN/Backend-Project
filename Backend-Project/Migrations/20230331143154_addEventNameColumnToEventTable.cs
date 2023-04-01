using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Project.Migrations
{
    public partial class addEventNameColumnToEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventName",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventName",
                table: "Events");
        }
    }
}
