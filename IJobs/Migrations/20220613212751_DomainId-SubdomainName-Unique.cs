using Microsoft.EntityFrameworkCore.Migrations;

namespace IJobs.Migrations
{
    public partial class DomainIdSubdomainNameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subdomains_DomainId",
                table: "Subdomains");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subdomains",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subdomains_DomainId_Name",
                table: "Subdomains",
                columns: new[] { "DomainId", "Name" },
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subdomains_DomainId_Name",
                table: "Subdomains");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subdomains",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subdomains_DomainId",
                table: "Subdomains",
                column: "DomainId");
        }
    }
}
