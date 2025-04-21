using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EatSomewhere.Migrations
{
    /// <inheritdoc />
    public partial class first_migration_tm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assemblies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assemblies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Origin = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidUnti = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AssemblyId = table.Column<string>(type: "text", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AssemblyUser",
                columns: table => new
                {
                    AssemblyId = table.Column<string>(type: "text", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
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
                    AdminsId = table.Column<string>(type: "text", nullable: false),
                    Assembly1Id = table.Column<string>(type: "text", nullable: false)
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
                    Assembly2Id = table.Column<string>(type: "text", nullable: false),
                    PendingId = table.Column<string>(type: "text", nullable: false)
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
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    AssemblyId = table.Column<string>(type: "text", nullable: false),
                    PersonCount = table.Column<int>(type: "integer", nullable: false),
                    Recipe = table.Column<string>(type: "text", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Foods_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    Unit = table.Column<int>(type: "integer", nullable: false),
                    AssemblyId = table.Column<string>(type: "text", nullable: false),
                    Archived = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredients_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodEntries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    FoodId = table.Column<string>(type: "text", nullable: false),
                    Cost = table.Column<int>(type: "integer", nullable: false),
                    PayedById = table.Column<string>(type: "text", nullable: true),
                    AssemblyId = table.Column<string>(type: "text", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodEntries_Assemblies_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "Assemblies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodEntries_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodEntries_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodEntries_Users_PayedById",
                        column: x => x.PayedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodTag",
                columns: table => new
                {
                    FoodId = table.Column<string>(type: "text", nullable: false),
                    TagsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTag", x => new { x.FoodId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_FoodTag_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodTag_Tag_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientEntries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IngredientId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientEntries_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    RecipientId = table.Column<string>(type: "text", nullable: false),
                    FoodEntryId = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Persons = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_FoodEntries_FoodEntryId",
                        column: x => x.FoodEntryId,
                        principalTable: "FoodEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "FoodParticipant",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    AdditionalPersons = table.Column<int>(type: "integer", nullable: false),
                    FoodEntryId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodParticipant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodParticipant_FoodEntries_FoodEntryId",
                        column: x => x.FoodEntryId,
                        principalTable: "FoodEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodParticipant_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredientEntry",
                columns: table => new
                {
                    FoodsId = table.Column<string>(type: "text", nullable: false),
                    IngredientsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredientEntry", x => new { x.FoodsId, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_FoodIngredientEntry_Foods_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodIngredientEntry_IngredientEntries_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "IngredientEntries",
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

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntries_AssemblyId",
                table: "FoodEntries",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntries_CreatedById",
                table: "FoodEntries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntries_FoodId",
                table: "FoodEntries",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodEntries_PayedById",
                table: "FoodEntries",
                column: "PayedById");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredientEntry_IngredientsId",
                table: "FoodIngredientEntry",
                column: "IngredientsId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodParticipant_FoodEntryId",
                table: "FoodParticipant",
                column: "FoodEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodParticipant_UserId",
                table: "FoodParticipant",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_AssemblyId",
                table: "Foods",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_CreatedById",
                table: "Foods",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FoodTag_TagsId",
                table: "FoodTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientEntries_IngredientId",
                table: "IngredientEntries",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_AssemblyId",
                table: "Ingredients",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_CreatedById",
                table: "Ingredients",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_AssemblyId",
                table: "Tag",
                column: "AssemblyId");
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
                name: "Bills");

            migrationBuilder.DropTable(
                name: "FoodIngredientEntry");

            migrationBuilder.DropTable(
                name: "FoodParticipant");

            migrationBuilder.DropTable(
                name: "FoodTag");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "IngredientEntries");

            migrationBuilder.DropTable(
                name: "FoodEntries");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Assemblies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
