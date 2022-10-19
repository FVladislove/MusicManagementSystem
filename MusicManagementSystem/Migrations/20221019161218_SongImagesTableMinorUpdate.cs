using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicManagementSystem.Migrations
{
    public partial class SongImagesTableMinorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongsImagesPaths_ImagesPaths_ImagesPathsId",
                table: "SongsImagesPaths");

            migrationBuilder.DropIndex(
                name: "IX_SongsImagesPaths_ImagesPathsId",
                table: "SongsImagesPaths");

            migrationBuilder.DropColumn(
                name: "ImagesPathsId",
                table: "SongsImagesPaths");

            migrationBuilder.CreateIndex(
                name: "IX_SongsImagesPaths_ImagePathId",
                table: "SongsImagesPaths",
                column: "ImagePathId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongsImagesPaths_ImagesPaths_ImagePathId",
                table: "SongsImagesPaths",
                column: "ImagePathId",
                principalTable: "ImagesPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongsImagesPaths_ImagesPaths_ImagePathId",
                table: "SongsImagesPaths");

            migrationBuilder.DropIndex(
                name: "IX_SongsImagesPaths_ImagePathId",
                table: "SongsImagesPaths");

            migrationBuilder.AddColumn<int>(
                name: "ImagesPathsId",
                table: "SongsImagesPaths",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SongsImagesPaths_ImagesPathsId",
                table: "SongsImagesPaths",
                column: "ImagesPathsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SongsImagesPaths_ImagesPaths_ImagesPathsId",
                table: "SongsImagesPaths",
                column: "ImagesPathsId",
                principalTable: "ImagesPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
