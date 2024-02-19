using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Playstore.Migrations.Migrations
{
    public partial class Updated_List_of_AppInfo_to_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppInfo_UserId",
                table: "AppInfo");

            migrationBuilder.CreateIndex(
                name: "IX_AppInfo_UserId",
                table: "AppInfo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppInfo_UserId",
                table: "AppInfo");

            migrationBuilder.CreateIndex(
                name: "IX_AppInfo_UserId",
                table: "AppInfo",
                column: "UserId",
                unique: true);
        }
    }
}
