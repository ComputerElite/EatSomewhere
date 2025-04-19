using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class Ownership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Users_UserId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "IngredientEntries");

            migrationBuilder.DropColumn(
                name: "CostPerPerson",
                table: "FoodEntries");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Ingredients",
                newName: "CreatedById");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_UserId",
                table: "Ingredients",
                newName: "IX_Ingredients_CreatedById");

            migrationBuilder.AlterColumn<int>(
                name: "Cost",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Foods",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Foods",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_CreatedById",
                table: "Foods",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Users_CreatedById",
                table: "Foods",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Users_CreatedById",
                table: "Ingredients",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Users_CreatedById",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Users_CreatedById",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Foods_CreatedById",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Foods");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Ingredients",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_CreatedById",
                table: "Ingredients",
                newName: "IX_Ingredients_UserId");

            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "Ingredients",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Unit",
                table: "IngredientEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CostPerPerson",
                table: "FoodEntries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Users_UserId",
                table: "Ingredients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
