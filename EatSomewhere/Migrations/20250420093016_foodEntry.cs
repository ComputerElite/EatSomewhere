using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class foodEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssemblyId",
                table: "FoodEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "FoodEntries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntries_AssemblyId",
                table: "FoodEntries",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntries_CreatedById",
                table: "FoodEntries",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntries_Assemblies_AssemblyId",
                table: "FoodEntries",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntries_Users_CreatedById",
                table: "FoodEntries",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntries_Assemblies_AssemblyId",
                table: "FoodEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntries_Users_CreatedById",
                table: "FoodEntries");

            migrationBuilder.DropIndex(
                name: "IX_FoodEntries_AssemblyId",
                table: "FoodEntries");

            migrationBuilder.DropIndex(
                name: "IX_FoodEntries_CreatedById",
                table: "FoodEntries");

            migrationBuilder.DropColumn(
                name: "AssemblyId",
                table: "FoodEntries");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "FoodEntries");
        }
    }
}
