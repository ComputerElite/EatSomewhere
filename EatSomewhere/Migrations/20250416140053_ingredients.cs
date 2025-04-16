using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class ingredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Assemblies_AssemblyId",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntry_Food_FoodId",
                table: "FoodEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntry_Users_PayedById",
                table: "FoodEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntryFoodParticipant_FoodEntry_FoodEntryId",
                table: "FoodEntryFoodParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodIngredientEntry_Food_FoodId",
                table: "FoodIngredientEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodIngredientEntry_IngredientEntry_IngredientsId",
                table: "FoodIngredientEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodTag_Food_FoodId",
                table: "FoodTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Assemblies_AssemblyId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Users_UserId",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientEntry_Ingredient_IngredientId",
                table: "IngredientEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientEntry",
                table: "IngredientEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodEntry",
                table: "FoodEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Food",
                table: "Food");

            migrationBuilder.RenameTable(
                name: "IngredientEntry",
                newName: "IngredientEntries");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameTable(
                name: "FoodEntry",
                newName: "FoodEntries");

            migrationBuilder.RenameTable(
                name: "Food",
                newName: "Foods");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientEntry_IngredientId",
                table: "IngredientEntries",
                newName: "IX_IngredientEntries_IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_UserId",
                table: "Ingredients",
                newName: "IX_Ingredients_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_AssemblyId",
                table: "Ingredients",
                newName: "IX_Ingredients_AssemblyId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodEntry_PayedById",
                table: "FoodEntries",
                newName: "IX_FoodEntries_PayedById");

            migrationBuilder.RenameIndex(
                name: "IX_FoodEntry_FoodId",
                table: "FoodEntries",
                newName: "IX_FoodEntries_FoodId");

            migrationBuilder.RenameColumn(
                name: "Rezept",
                table: "Foods",
                newName: "Recipe");

            migrationBuilder.RenameIndex(
                name: "IX_Food_AssemblyId",
                table: "Foods",
                newName: "IX_Foods_AssemblyId");

            migrationBuilder.AlterColumn<string>(
                name: "IngredientId",
                table: "IngredientEntries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "PayedById",
                table: "FoodEntries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "FoodId",
                table: "FoodEntries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientEntries",
                table: "IngredientEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodEntries",
                table: "FoodEntries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Foods",
                table: "Foods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntries_Foods_FoodId",
                table: "FoodEntries",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntries_Users_PayedById",
                table: "FoodEntries",
                column: "PayedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntryFoodParticipant_FoodEntries_FoodEntryId",
                table: "FoodEntryFoodParticipant",
                column: "FoodEntryId",
                principalTable: "FoodEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIngredientEntry_Foods_FoodId",
                table: "FoodIngredientEntry",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIngredientEntry_IngredientEntries_IngredientsId",
                table: "FoodIngredientEntry",
                column: "IngredientsId",
                principalTable: "IngredientEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Assemblies_AssemblyId",
                table: "Foods",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodTag_Foods_FoodId",
                table: "FoodTag",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Assemblies_AssemblyId",
                table: "Ingredients",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Users_UserId",
                table: "Ingredients",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntries_Foods_FoodId",
                table: "FoodEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntries_Users_PayedById",
                table: "FoodEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntryFoodParticipant_FoodEntries_FoodEntryId",
                table: "FoodEntryFoodParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodIngredientEntry_Foods_FoodId",
                table: "FoodIngredientEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodIngredientEntry_IngredientEntries_IngredientsId",
                table: "FoodIngredientEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Assemblies_AssemblyId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodTag_Foods_FoodId",
                table: "FoodTag");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientEntries_Ingredients_IngredientId",
                table: "IngredientEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Assemblies_AssemblyId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Users_UserId",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientEntries",
                table: "IngredientEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Foods",
                table: "Foods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodEntries",
                table: "FoodEntries");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameTable(
                name: "IngredientEntries",
                newName: "IngredientEntry");

            migrationBuilder.RenameTable(
                name: "Foods",
                newName: "Food");

            migrationBuilder.RenameTable(
                name: "FoodEntries",
                newName: "FoodEntry");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_UserId",
                table: "Ingredient",
                newName: "IX_Ingredient_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_AssemblyId",
                table: "Ingredient",
                newName: "IX_Ingredient_AssemblyId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientEntries_IngredientId",
                table: "IngredientEntry",
                newName: "IX_IngredientEntry_IngredientId");

            migrationBuilder.RenameColumn(
                name: "Recipe",
                table: "Food",
                newName: "Rezept");

            migrationBuilder.RenameIndex(
                name: "IX_Foods_AssemblyId",
                table: "Food",
                newName: "IX_Food_AssemblyId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodEntries_PayedById",
                table: "FoodEntry",
                newName: "IX_FoodEntry_PayedById");

            migrationBuilder.RenameIndex(
                name: "IX_FoodEntries_FoodId",
                table: "FoodEntry",
                newName: "IX_FoodEntry_FoodId");

            migrationBuilder.AlterColumn<string>(
                name: "IngredientId",
                table: "IngredientEntry",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PayedById",
                table: "FoodEntry",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FoodId",
                table: "FoodEntry",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientEntry",
                table: "IngredientEntry",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Food",
                table: "Food",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodEntry",
                table: "FoodEntry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Assemblies_AssemblyId",
                table: "Food",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntry_Food_FoodId",
                table: "FoodEntry",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntry_Users_PayedById",
                table: "FoodEntry",
                column: "PayedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntryFoodParticipant_FoodEntry_FoodEntryId",
                table: "FoodEntryFoodParticipant",
                column: "FoodEntryId",
                principalTable: "FoodEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIngredientEntry_Food_FoodId",
                table: "FoodIngredientEntry",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodIngredientEntry_IngredientEntry_IngredientsId",
                table: "FoodIngredientEntry",
                column: "IngredientsId",
                principalTable: "IngredientEntry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodTag_Food_FoodId",
                table: "FoodTag",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Assemblies_AssemblyId",
                table: "Ingredient",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Users_UserId",
                table: "Ingredient",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientEntry_Ingredient_IngredientId",
                table: "IngredientEntry",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
