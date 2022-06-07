using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IJobs.Migrations
{
    public partial class changeKeyApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Applications_ApplicationUserId_ApplicationJobId",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_ApplicationUserId_ApplicationJobId",
                table: "Interviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationJobId",
                table: "Interviews");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Interviews");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_ApplicationId",
                table: "Interviews",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId_JobId",
                table: "Applications",
                columns: new[] { "UserId", "JobId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Applications_ApplicationId",
                table: "Interviews",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_Applications_ApplicationId",
                table: "Interviews");

            migrationBuilder.DropIndex(
                name: "IX_Interviews_ApplicationId",
                table: "Interviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_UserId_JobId",
                table: "Applications");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationJobId",
                table: "Interviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Interviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Applications",
                columns: new[] { "UserId", "JobId" });

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_ApplicationUserId_ApplicationJobId",
                table: "Interviews",
                columns: new[] { "ApplicationUserId", "ApplicationJobId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_Applications_ApplicationUserId_ApplicationJobId",
                table: "Interviews",
                columns: new[] { "ApplicationUserId", "ApplicationJobId" },
                principalTable: "Applications",
                principalColumns: new[] { "UserId", "JobId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
