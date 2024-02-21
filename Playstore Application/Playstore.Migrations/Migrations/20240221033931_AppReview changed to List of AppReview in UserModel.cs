using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Playstore.Migrations.Migrations
{
    public partial class AppReviewchangedtoListofAppReviewinUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppReviews_UserId",
                table: "AppReviews");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "AppDatas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppReviews_UserId",
                table: "AppReviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppReviews_UserId",
                table: "AppReviews");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "AppDatas");

            migrationBuilder.CreateIndex(
                name: "IX_AppReviews_UserId",
                table: "AppReviews",
                column: "UserId",
                unique: true);
        }
    }
}
