using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Playstore.Migrations.Migrations
{
    public partial class UpdateListofAppInfoandListofAppDownloadsinUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppInfo_UserId",
                table: "AppInfo");

            migrationBuilder.DropIndex(
                name: "IX_AppDownloads_UserId",
                table: "AppDownloads");

            migrationBuilder.CreateIndex(
                name: "IX_AppInfo_UserId",
                table: "AppInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDownloads_UserId",
                table: "AppDownloads",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppInfo_UserId",
                table: "AppInfo");

            migrationBuilder.DropIndex(
                name: "IX_AppDownloads_UserId",
                table: "AppDownloads");

            migrationBuilder.CreateIndex(
                name: "IX_AppInfo_UserId",
                table: "AppInfo",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppDownloads_UserId",
                table: "AppDownloads",
                column: "UserId",
                unique: true);
        }
    }
}
