using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolAdministrator.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Institutions_InstitutionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Institutions_InstitutionId",
                table: "Levels");

            migrationBuilder.DropIndex(
                name: "IX_Levels_Name_InstitutionId",
                table: "Levels");

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "Levels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    SubjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCategories_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name_InstitutionId",
                table: "Levels",
                columns: new[] { "Name", "InstitutionId" },
                unique: true,
                filter: "[InstitutionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_ProductId_SubjectId",
                table: "ProductCategories",
                columns: new[] { "ProductId", "SubjectId" },
                unique: true,
                filter: "[ProductId] IS NOT NULL AND [SubjectId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_SubjectId",
                table: "ProductCategories",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Institutions_InstitutionId",
                table: "AspNetUsers",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Institutions_InstitutionId",
                table: "Levels",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Institutions_InstitutionId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Institutions_InstitutionId",
                table: "Levels");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Levels_Name_InstitutionId",
                table: "Levels");

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "Levels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InstitutionId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Levels_Name_InstitutionId",
                table: "Levels",
                columns: new[] { "Name", "InstitutionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Institutions_InstitutionId",
                table: "AspNetUsers",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
