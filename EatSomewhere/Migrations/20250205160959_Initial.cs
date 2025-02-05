using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assemblies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assemblies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Origin = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidUnti = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AssemblyId = table.Column<string>(type: "TEXT", nullable: false),
                    PersonCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Rezept = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Salt = table.Column<string>(type: "TEXT", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AssemblyId = table.Column<string>(type: "TEXT", nullable: true),
                    AssemblyId1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Assemblies_AssemblyId1",
                        column: x => x.AssemblyId1,
                        principalTable: "Assemblies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AssemblyId = table.Column<string>(type: "TEXT", nullable: false),
                    FoodId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tag_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodEntry",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    FoodId = table.Column<string>(type: "TEXT", nullable: false),
                    Cost = table.Column<int>(type: "INTEGER", nullable: false),
                    CostPerPerson = table.Column<int>(type: "INTEGER", nullable: false),
                    PayedById = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodEntry_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodEntry_Users_PayedById",
                        column: x => x.PayedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AssemblyId = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredient_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodParticipant",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FoodEntryId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodParticipant_FoodEntry_FoodEntryId",
                        column: x => x.FoodEntryId,
                        principalTable: "FoodEntry",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IngredientEntry",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    IngredientId = table.Column<string>(type: "TEXT", nullable: false),
                    Amount = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientEntry_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IngredientEntry_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_AssemblyId",
                table: "Food",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntry_FoodId",
                table: "FoodEntry",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntry_PayedById",
                table: "FoodEntry",
                column: "PayedById");

            migrationBuilder.CreateIndex(
                name: "IX_FoodParticipant_FoodEntryId",
                table: "FoodParticipant",
                column: "FoodEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_AssemblyId",
                table: "Ingredient",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_UserId",
                table: "Ingredient",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientEntry_FoodId",
                table: "IngredientEntry",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientEntry_IngredientId",
                table: "IngredientEntry",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_AssemblyId",
                table: "Tag",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_FoodId",
                table: "Tag",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AssemblyId",
                table: "Users",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AssemblyId1",
                table: "Users",
                column: "AssemblyId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodParticipant");

            migrationBuilder.DropTable(
                name: "IngredientEntry");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "FoodEntry");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Assemblies");
        }
    }
}
