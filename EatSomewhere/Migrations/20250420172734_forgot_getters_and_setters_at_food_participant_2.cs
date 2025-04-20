using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class forgot_getters_and_setters_at_food_participant_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssemblyId",
                table: "FoodParticipant",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FoodId",
                table: "FoodParticipant",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FoodParticipant_AssemblyId",
                table: "FoodParticipant",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodParticipant_FoodId",
                table: "FoodParticipant",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodParticipant_Assemblies_AssemblyId",
                table: "FoodParticipant",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodParticipant_Foods_FoodId",
                table: "FoodParticipant",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodParticipant_Assemblies_AssemblyId",
                table: "FoodParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodParticipant_Foods_FoodId",
                table: "FoodParticipant");

            migrationBuilder.DropIndex(
                name: "IX_FoodParticipant_AssemblyId",
                table: "FoodParticipant");

            migrationBuilder.DropIndex(
                name: "IX_FoodParticipant_FoodId",
                table: "FoodParticipant");

            migrationBuilder.DropColumn(
                name: "AssemblyId",
                table: "FoodParticipant");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "FoodParticipant");
        }
    }
}
