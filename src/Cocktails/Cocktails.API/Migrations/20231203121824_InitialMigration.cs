using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cocktails.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cocktails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CocktailIngredient",
                columns: table => new
                {
                    CocktailsId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailIngredient", x => new { x.CocktailsId, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_CocktailIngredient_Cocktails_CocktailsId",
                        column: x => x.CocktailsId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CocktailIngredient_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cocktails",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Caipirinha" },
                    { 2, "Chrysanthemum" },
                    { 3, "Hangman's Blood" },
                    { 4, "Angel Face" },
                    { 5, "Between the sheets" },
                    { 6, "Damn the weather" },
                    { 7, "Hanky panky" },
                    { 8, "Monkey Gland" },
                    { 9, "Salty dog" },
                    { 10, "Fish house punch" },
                    { 11, "Tamagozake" },
                    { 12, "Bloody Mary" },
                    { 13, "Paloma" },
                    { 14, "Batanga" },
                    { 15, "Margarita" },
                    { 16, "Naked and famous" },
                    { 17, "Screwdriver" },
                    { 18, "Sea breeze" },
                    { 19, "Spicy Fifty" },
                    { 20, "Old pal" },
                    { 21, "Amber moon" },
                    { 22, "Farnell" },
                    { 23, "Rusty nail" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cachaça" },
                    { 2, "Sugar" },
                    { 3, "Lime" },
                    { 4, "Absinthe" },
                    { 5, "Bénédictine" },
                    { 6, "Vermouth" },
                    { 7, "Gin" },
                    { 8, "Whisky" },
                    { 9, "Rum" },
                    { 10, "Port" },
                    { 11, "Brandy" },
                    { 12, "Stout" },
                    { 13, "Champagne" },
                    { 14, "Apricot" },
                    { 15, "Calvados" },
                    { 16, "White rum" },
                    { 17, "Cognac" },
                    { 18, "Triple sec" },
                    { 19, "Lemon juice" },
                    { 20, "Sweet vermouth" },
                    { 21, "Orange juice" },
                    { 22, "Sweetener" },
                    { 23, "Fernet-Branca" },
                    { 24, "Grenadine" },
                    { 25, "Vodka" },
                    { 26, "Grapefruit" },
                    { 27, "Peach brandy" },
                    { 28, "Heated sake" },
                    { 29, "Raw egg" },
                    { 30, "Tomato juice" },
                    { 31, "Worcestershire sauce" },
                    { 32, "Hot sauces" },
                    { 33, "Garlic" },
                    { 34, "Herbs" },
                    { 35, "Horseradish" },
                    { 36, "Celery" },
                    { 37, "Olives" },
                    { 38, "Pickled vegetables" },
                    { 39, "Salt" },
                    { 40, "Black pepper" },
                    { 41, "Lime juice" },
                    { 42, "Tequila" },
                    { 43, "Grapefruit-flavored soda" },
                    { 44, "Cola" },
                    { 45, "Mezcal" },
                    { 46, "Yellow Chartreuse" },
                    { 47, "Aperol" },
                    { 48, "Cranberry juice" },
                    { 49, "Grapefruit juice" },
                    { 50, "Elderflower cordial" },
                    { 51, "Honey syrup" },
                    { 52, "Red chili pepper" },
                    { 53, "Whiskey" },
                    { 54, "French vermouth (dry)" },
                    { 55, "Campari" },
                    { 56, "Tabasco sauce" },
                    { 57, "Lemonade" },
                    { 58, "Scotch whisky" },
                    { 59, "Drambuie" }
                });

            migrationBuilder.InsertData(
                table: "CocktailIngredient",
                columns: new[] { "CocktailsId", "IngredientsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 3, 13 },
                    { 4, 7 },
                    { 4, 14 },
                    { 4, 15 },
                    { 5, 16 },
                    { 5, 17 },
                    { 5, 18 },
                    { 5, 19 },
                    { 6, 7 },
                    { 6, 20 },
                    { 6, 21 },
                    { 6, 22 },
                    { 7, 7 },
                    { 7, 20 },
                    { 7, 23 },
                    { 8, 4 },
                    { 8, 7 },
                    { 8, 21 },
                    { 8, 24 },
                    { 9, 25 },
                    { 9, 26 },
                    { 10, 9 },
                    { 10, 17 },
                    { 10, 27 },
                    { 11, 2 },
                    { 11, 28 },
                    { 11, 29 },
                    { 12, 19 },
                    { 12, 25 },
                    { 12, 30 },
                    { 12, 31 },
                    { 12, 32 },
                    { 12, 33 },
                    { 12, 34 },
                    { 12, 35 },
                    { 12, 36 },
                    { 12, 37 },
                    { 12, 38 },
                    { 12, 39 },
                    { 12, 40 },
                    { 12, 41 },
                    { 13, 41 },
                    { 13, 42 },
                    { 13, 43 },
                    { 14, 41 },
                    { 14, 42 },
                    { 14, 44 },
                    { 15, 18 },
                    { 15, 41 },
                    { 15, 42 },
                    { 16, 41 },
                    { 16, 45 },
                    { 16, 46 },
                    { 16, 47 },
                    { 17, 21 },
                    { 17, 25 },
                    { 18, 25 },
                    { 18, 48 },
                    { 18, 49 },
                    { 19, 25 },
                    { 19, 41 },
                    { 19, 50 },
                    { 19, 51 },
                    { 19, 52 },
                    { 20, 53 },
                    { 20, 54 },
                    { 20, 55 },
                    { 21, 29 },
                    { 21, 53 },
                    { 21, 56 },
                    { 22, 53 },
                    { 22, 57 },
                    { 23, 58 },
                    { 23, 59 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CocktailIngredient_IngredientsId",
                table: "CocktailIngredient",
                column: "IngredientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CocktailIngredient");

            migrationBuilder.DropTable(
                name: "Cocktails");

            migrationBuilder.DropTable(
                name: "Ingredients");
        }
    }
}
