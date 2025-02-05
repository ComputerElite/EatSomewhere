using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class stuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssemblyId2",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Ingredient",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Ingredient",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "Ingredient",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AssemblyId2",
                table: "Users",
                column: "AssemblyId2");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Assemblies_AssemblyId2",
                table: "Users",
                column: "AssemblyId2",
                principalTable: "Assemblies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Assemblies_AssemblyId2",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AssemblyId2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AssemblyId2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Ingredient");
        }
    }
}
