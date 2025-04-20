using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class afsßemh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodParticipant_Assemblies_AssemblyId",
                table: "FoodParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodParticipant_Foods_FoodId",
                table: "FoodParticipant");

            migrationBuilder.DropTable(
                name: "FoodEntryFoodParticipant");

            migrationBuilder.DropIndex(
                name: "IX_FoodParticipant_AssemblyId",
                table: "FoodParticipant");

            migrationBuilder.DropColumn(
                name: "AssemblyId",
                table: "FoodParticipant");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "FoodParticipant",
                newName: "FoodEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodParticipant_FoodId",
                table: "FoodParticipant",
                newName: "IX_FoodParticipant_FoodEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodParticipant_FoodEntries_FoodEntryId",
                table: "FoodParticipant",
                column: "FoodEntryId",
                principalTable: "FoodEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodParticipant_FoodEntries_FoodEntryId",
                table: "FoodParticipant");

            migrationBuilder.RenameColumn(
                name: "FoodEntryId",
                table: "FoodParticipant",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_FoodParticipant_FoodEntryId",
                table: "FoodParticipant",
                newName: "IX_FoodParticipant_FoodId");

            migrationBuilder.AddColumn<string>(
                name: "AssemblyId",
                table: "FoodParticipant",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FoodEntryFoodParticipant",
                columns: table => new
                {
                    FoodEntryId = table.Column<string>(type: "TEXT", nullable: false),
                    ParticipantsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodEntryFoodParticipant", x => new { x.FoodEntryId, x.ParticipantsId });
                    table.ForeignKey(
                        name: "FK_FoodEntryFoodParticipant_FoodEntries_FoodEntryId",
                        column: x => x.FoodEntryId,
                        principalTable: "FoodEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodEntryFoodParticipant_FoodParticipant_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "FoodParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodParticipant_AssemblyId",
                table: "FoodParticipant",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntryFoodParticipant_ParticipantsId",
                table: "FoodEntryFoodParticipant",
                column: "ParticipantsId");

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
    }
}
