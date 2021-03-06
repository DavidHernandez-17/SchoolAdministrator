using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAdministrator.Migrations
{
    public partial class AddRelationLevelInstitution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Levels_Name_Type",
                table: "Levels");

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

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name_InstitutionId",
                table: "Levels",
                columns: new[] { "Name", "InstitutionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Institutions_InstitutionId",
                table: "Levels",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Institutions_InstitutionId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_InstitutionId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_Name_InstitutionId",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "InstitutionId",
                table: "Levels");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name_Type",
                table: "Levels",
                columns: new[] { "Name", "Type" },
                unique: true);
        }
    }
}
