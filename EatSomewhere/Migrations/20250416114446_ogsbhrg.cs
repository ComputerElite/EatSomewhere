using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class ogsbhrg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodParticipant_FoodEntry_FoodEntryId",
                table: "FoodParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientEntry_Food_FoodId",
                table: "IngredientEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Food_FoodId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Assemblies_AssemblyId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Assemblies_AssemblyId1",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Assemblies_AssemblyId2",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AssemblyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AssemblyId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AssemblyId2",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tag_FoodId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_IngredientEntry_FoodId",
                table: "IngredientEntry");

            migrationBuilder.DropIndex(
                name: "IX_FoodParticipant_FoodEntryId",
                table: "FoodParticipant");

            migrationBuilder.DropColumn(
                name: "AssemblyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AssemblyId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AssemblyId2",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "IngredientEntry");

            migrationBuilder.DropColumn(
                name: "FoodEntryId",
                table: "FoodParticipant");

            migrationBuilder.CreateTable(
                name: "AssemblyUser",
                columns: table => new
                {
                    AssemblyId = table.Column<string>(type: "TEXT", nullable: false),
                    UsersId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyUser", x => new { x.AssemblyId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AssemblyUser_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblyUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyUser1",
                columns: table => new
                {
                    AdminsId = table.Column<string>(type: "TEXT", nullable: false),
                    Assembly1Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyUser1", x => new { x.AdminsId, x.Assembly1Id });
                    table.ForeignKey(
                        name: "FK_AssemblyUser1_Assemblies_Assembly1Id",
                        column: x => x.Assembly1Id,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblyUser1_Users_AdminsId",
                        column: x => x.AdminsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyUser2",
                columns: table => new
                {
                    Assembly2Id = table.Column<string>(type: "TEXT", nullable: false),
                    PendingId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssemblyUser2", x => new { x.Assembly2Id, x.PendingId });
                    table.ForeignKey(
                        name: "FK_AssemblyUser2_Assemblies_Assembly2Id",
                        column: x => x.Assembly2Id,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblyUser2_Users_PendingId",
                        column: x => x.PendingId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        name: "FK_FoodEntryFoodParticipant_FoodEntry_FoodEntryId",
                        column: x => x.FoodEntryId,
                        principalTable: "FoodEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodEntryFoodParticipant_FoodParticipant_ParticipantsId",
                        column: x => x.ParticipantsId,
                        principalTable: "FoodParticipant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredientEntry",
                columns: table => new
                {
                    FoodId = table.Column<string>(type: "TEXT", nullable: false),
                    IngredientsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredientEntry", x => new { x.FoodId, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_FoodIngredientEntry_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodIngredientEntry_IngredientEntry_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "IngredientEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodTag",
                columns: table => new
                {
                    FoodId = table.Column<string>(type: "TEXT", nullable: false),
                    TagsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTag", x => new { x.FoodId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_FoodTag_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyUser_UsersId",
                table: "AssemblyUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyUser1_Assembly1Id",
                table: "AssemblyUser1",
                column: "Assembly1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyUser2_PendingId",
                table: "AssemblyUser2",
                column: "PendingId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntryFoodParticipant_ParticipantsId",
                table: "FoodEntryFoodParticipant",
                column: "ParticipantsId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredientEntry_IngredientsId",
                table: "FoodIngredientEntry",
                column: "IngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTag_TagsId",
                table: "FoodTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssemblyUser");

            migrationBuilder.DropTable(
                name: "AssemblyUser1");

            migrationBuilder.DropTable(
                name: "AssemblyUser2");

            migrationBuilder.DropTable(
                name: "FoodEntryFoodParticipant");

            migrationBuilder.DropTable(
                name: "FoodIngredientEntry");

            migrationBuilder.DropTable(
                name: "FoodTag");

            migrationBuilder.AddColumn<string>(
                name: "AssemblyId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssemblyId1",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssemblyId2",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FoodId",
                table: "Tag",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FoodId",
                table: "IngredientEntry",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FoodEntryId",
                table: "FoodParticipant",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AssemblyId",
                table: "Users",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AssemblyId1",
                table: "Users",
                column: "AssemblyId1");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AssemblyId2",
                table: "Users",
                column: "AssemblyId2");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_FoodId",
                table: "Tag",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientEntry_FoodId",
                table: "IngredientEntry",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodParticipant_FoodEntryId",
                table: "FoodParticipant",
                column: "FoodEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodParticipant_FoodEntry_FoodEntryId",
                table: "FoodParticipant",
                column: "FoodEntryId",
                principalTable: "FoodEntry",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientEntry_Food_FoodId",
                table: "IngredientEntry",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Food_FoodId",
                table: "Tag",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Assemblies_AssemblyId",
                table: "Users",
                column: "AssemblyId",
                principalTable: "Assemblies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Assemblies_AssemblyId1",
                table: "Users",
                column: "AssemblyId1",
                principalTable: "Assemblies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Assemblies_AssemblyId2",
                table: "Users",
                column: "AssemblyId2",
                principalTable: "Assemblies",
                principalColumn: "Id");
        }
    }
}
