using Microsoft.EntityFrameworkCore.Migrations;

namespace IJobs.Migrations
{
    public partial class ondeletesetnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Subdomains_SubdomainId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tutorials_Subdomains_SubdomainId",
                table: "Tutorials");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobTitle",
                table: "Jobs");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Subdomains_SubdomainId",
                table: "Jobs",
                column: "SubdomainId",
                principalTable: "Subdomains",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tutorials_Subdomains_SubdomainId",
                table: "Tutorials",
                column: "SubdomainId",
                principalTable: "Subdomains",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Subdomains_SubdomainId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tutorials_Subdomains_SubdomainId",
                table: "Tutorials");

            migrationBuilder.AlterColumn<string>(
                name: "JobTitle",
                table: "Jobs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobTitle",
                table: "Jobs",
                column: "JobTitle",
                unique: true,
                filter: "[JobTitle] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Subdomains_SubdomainId",
                table: "Jobs",
                column: "SubdomainId",
                principalTable: "Subdomains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tutorials_Subdomains_SubdomainId",
                table: "Tutorials",
                column: "SubdomainId",
                principalTable: "Subdomains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
