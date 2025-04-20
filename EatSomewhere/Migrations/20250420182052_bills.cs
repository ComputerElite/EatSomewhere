using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class bills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    RecipientId = table.Column<string>(type: "TEXT", nullable: false),
                    FoodEntryId = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_FoodEntries_FoodEntryId",
                        column: x => x.FoodEntryId,
                        principalTable: "FoodEntries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_FoodEntryId",
                table: "Bills",
                column: "FoodEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_RecipientId",
                table: "Bills",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");
        }
    }
}
