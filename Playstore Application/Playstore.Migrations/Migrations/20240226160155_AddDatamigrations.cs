using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Playstore.Migrations.Migrations
{
    public partial class AddDatamigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppImages_AppInfo_AppInfoAppId",
                table: "AppImages");

            migrationBuilder.DropIndex(
                name: "IX_AppImages_AppInfoAppId",
                table: "AppImages");

            migrationBuilder.DropColumn(
                name: "AppInfoAppId",
                table: "AppImages");

            migrationBuilder.CreateIndex(
                name: "IX_AppImages_AppId",
                table: "AppImages",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImages_AppInfo_AppId",
                table: "AppImages",
                column: "AppId",
                principalTable: "AppInfo",
                principalColumn: "AppId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppImages_AppInfo_AppId",
                table: "AppImages");

            migrationBuilder.DropIndex(
                name: "IX_AppImages_AppId",
                table: "AppImages");

            migrationBuilder.AddColumn<Guid>(
                name: "AppInfoAppId",
                table: "AppImages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppImages_AppInfoAppId",
                table: "AppImages",
                column: "AppInfoAppId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppImages_AppInfo_AppInfoAppId",
                table: "AppImages",
                column: "AppInfoAppId",
                principalTable: "AppInfo",
                principalColumn: "AppId");
        }
    }
}
