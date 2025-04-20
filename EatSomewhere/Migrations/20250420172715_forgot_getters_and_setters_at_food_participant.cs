using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class forgot_getters_and_setters_at_food_participant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdditionalPersons",
                table: "FoodParticipant",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FoodParticipant",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FoodParticipant_UserId",
                table: "FoodParticipant",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodParticipant_Users_UserId",
                table: "FoodParticipant",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodParticipant_Users_UserId",
                table: "FoodParticipant");

            migrationBuilder.DropIndex(
                name: "IX_FoodParticipant_UserId",
                table: "FoodParticipant");

            migrationBuilder.DropColumn(
                name: "AdditionalPersons",
                table: "FoodParticipant");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FoodParticipant");
        }
    }
}
