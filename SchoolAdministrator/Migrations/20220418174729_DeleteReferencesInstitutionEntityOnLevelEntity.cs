using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAdministrator.Migrations
{
    public partial class DeleteReferencesInstitutionEntityOnLevelEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Institutions_InstitutionId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_InstitutionId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Levels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstitutionId",
                table: "Levels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Levels_InstitutionId",
                table: "Levels",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Institutions_InstitutionId",
                table: "Levels",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
