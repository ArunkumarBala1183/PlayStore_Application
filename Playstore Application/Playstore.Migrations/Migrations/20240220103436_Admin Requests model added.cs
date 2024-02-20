using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Playstore.Migrations.Migrations
{
    public partial class AdminRequestsmodeladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RequestId",
                table: "AppInfo",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AppInfo",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdminRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminRequests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppInfo_RequestId",
                table: "AppInfo",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdminRequests_UserId",
                table: "AdminRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfo_AdminRequests_RequestId",
                table: "AppInfo",
                column: "RequestId",
                principalTable: "AdminRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInfo_AdminRequests_RequestId",
                table: "AppInfo");

            migrationBuilder.DropTable(
                name: "AdminRequests");

            migrationBuilder.DropIndex(
                name: "IX_AppInfo_RequestId",
                table: "AppInfo");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "AppInfo");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AppInfo");
        }
    }
}
