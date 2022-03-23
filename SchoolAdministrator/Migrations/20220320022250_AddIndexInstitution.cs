using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAdministrator.Migrations
{
    public partial class AddIndexInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Institutions_Name",
                table: "Institutions",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Institutions_Name",
                table: "Institutions");
        }
    }
}
