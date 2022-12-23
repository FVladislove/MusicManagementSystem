using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicManagementSystem.Migrations
{
    public partial class SongFileInModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SongFilePath",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongFilePath",
                table: "Songs");
        }
    }
}
