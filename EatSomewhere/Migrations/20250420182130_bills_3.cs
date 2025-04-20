using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class bills_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_FoodEntries_FoodEntryId",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "FoodEntryId",
                table: "Bills",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_FoodEntries_FoodEntryId",
                table: "Bills",
                column: "FoodEntryId",
                principalTable: "FoodEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_FoodEntries_FoodEntryId",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "FoodEntryId",
                table: "Bills",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_FoodEntries_FoodEntryId",
                table: "Bills",
                column: "FoodEntryId",
                principalTable: "FoodEntries",
                principalColumn: "Id");
        }
    }
}
