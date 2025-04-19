using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class jgyekf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodIngredientEntry_Foods_FoodId",
                table: "FoodIngredientEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientEntries_Ingredients_IngredientId",
                table: "IngredientEntries");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "FoodIngredientEntry",
                newName: "FoodsId");

            migrationBuilder.AlterColumn<string>(
                name: "IngredientId",
                table: "IngredientEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIngredientEntry_Foods_FoodsId",
                table: "FoodIngredientEntry",
                column: "FoodsId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientEntries_Ingredients_IngredientId",
                table: "IngredientEntries",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodIngredientEntry_Foods_FoodsId",
                table: "FoodIngredientEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientEntries_Ingredients_IngredientId",
                table: "IngredientEntries");

            migrationBuilder.RenameColumn(
                name: "FoodsId",
                table: "FoodIngredientEntry",
                newName: "FoodId");

            migrationBuilder.AlterColumn<string>(
                name: "IngredientId",
                table: "IngredientEntries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIngredientEntry_Foods_FoodId",
                table: "FoodIngredientEntry",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientEntries_Ingredients_IngredientId",
                table: "IngredientEntries",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id");
        }
    }
}
