using Cocktails.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.API.DbContexts
{
    public class CocktailsDbContext : DbContext
    {
        public DbSet<Cocktail> Cocktails { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;

        public CocktailsDbContext(DbContextOptions<CocktailsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().HasData(

                new Ingredient(1, "Cachaça"),
                new Ingredient(2, "Sugar"),
                new Ingredient(3, "Lime"),
                new Ingredient(4, "Absinthe"),
                new Ingredient(5, "Bénédictine"),
                new Ingredient(6, "Vermouth"),
                new Ingredient(7, "Gin"),
                new Ingredient(8, "Whisky"),
                new Ingredient(9, "Rum"),
                new Ingredient(10, "Port"),

                new Ingredient(11, "Brandy"),
                new Ingredient(12, "Stout"),
                new Ingredient(13, "Champagne"),
                new Ingredient(14, "Apricot"),
                new Ingredient(15, "Calvados"),
                new Ingredient(16, "White rum"),
                new Ingredient(17, "Cognac"),
                new Ingredient(18, "Triple sec"),
                new Ingredient(19, "Lemon juice"),
                new Ingredient(20, "Sweet vermouth"),

                new Ingredient(21, "Orange juice"),
                new Ingredient(22, "Sweetener"),
                new Ingredient(23, "Fernet-Branca"),
                new Ingredient(24, "Grenadine"),
                new Ingredient(25, "Vodka"),
                new Ingredient(26, "Grapefruit"),
                new Ingredient(27, "Peach brandy"),
                new Ingredient(28, "Heated sake"),
                new Ingredient(29, "Raw egg"),
                new Ingredient(30, "Tomato juice"),

                new Ingredient(31, "Worcestershire sauce"),
                new Ingredient(32, "Hot sauces"),
                new Ingredient(33, "Garlic"),
                new Ingredient(34, "Herbs"),
                new Ingredient(35, "Horseradish"),
                new Ingredient(36, "Celery"),
                new Ingredient(37, "Olives"),
                new Ingredient(38, "Pickled vegetables"),
                new Ingredient(39, "Salt"),
                new Ingredient(40, "Black pepper"),

                new Ingredient(41, "Lime juice"),
                new Ingredient(42, "Tequila"),
                new Ingredient(43, "Grapefruit-flavored soda"),
                new Ingredient(44, "Cola"),
                new Ingredient(45, "Mezcal"),
                new Ingredient(46, "Yellow Chartreuse"),
                new Ingredient(47, "Aperol"),
                new Ingredient(48, "Cranberry juice"),
                new Ingredient(49, "Grapefruit juice"),
                new Ingredient(50, "Elderflower cordial"),

                new Ingredient(51, "Honey syrup"),
                new Ingredient(52, "Red chili pepper"),
                new Ingredient(53, "Whiskey"),
                new Ingredient(54, "French vermouth (dry)"),
                new Ingredient(55, "Campari"),
                new Ingredient(56, "Tabasco sauce"),

                new Ingredient(57, "Lemonade"),
                new Ingredient(58, "Scotch whisky"),
                new Ingredient(59, "Drambuie")
                );

            modelBuilder.Entity<Cocktail>().HasData(

                new Cocktail(1, "Caipirinha"),
                new Cocktail(2, "Chrysanthemum"),
                new Cocktail(3, "Hangman's Blood"),
                new Cocktail(4, "Angel Face"),
                new Cocktail(5, "Between the sheets"),
                new Cocktail(6, "Damn the weather"),
                new Cocktail(7, "Hanky panky"),
                new Cocktail(8, "Monkey Gland"),
                new Cocktail(9, "Salty dog"),
                new Cocktail(10, "Fish house punch"),
                new Cocktail(11, "Tamagozake"),
                new Cocktail(12, "Bloody Mary"),
                new Cocktail(13, "Paloma"),
                new Cocktail(14, "Batanga"),
                new Cocktail(15, "Margarita"),
                new Cocktail(16, "Naked and famous"),
                new Cocktail(17, "Screwdriver"),
                new Cocktail(18, "Sea breeze"),
                new Cocktail(19, "Spicy Fifty"),
                new Cocktail(20, "Old pal"),
                new Cocktail(21, "Amber moon"),
                new Cocktail(22, "Farnell"),
                new Cocktail(23, "Rusty nail")
               );

            modelBuilder
                .Entity<Cocktail>()
                .HasMany(d => d.Ingredients)
                .WithMany(i => i.Cocktails)
                .UsingEntity(e => e.HasData(

                    new { CocktailsId = 1, IngredientsId = 1 },
                    new { CocktailsId = 1, IngredientsId = 2 },
                    new { CocktailsId = 1, IngredientsId = 3 },

                    new { CocktailsId = 2, IngredientsId = 4 },
                    new { CocktailsId = 2, IngredientsId = 5 },
                    new { CocktailsId = 2, IngredientsId = 6 },

                    new { CocktailsId = 3, IngredientsId = 7 },
                    new { CocktailsId = 3, IngredientsId = 8 },
                    new { CocktailsId = 3, IngredientsId = 9 },
                    new { CocktailsId = 3, IngredientsId = 10 },
                    new { CocktailsId = 3, IngredientsId = 11 },
                    new { CocktailsId = 3, IngredientsId = 12 },
                    new { CocktailsId = 3, IngredientsId = 13 },

                    new { CocktailsId = 4, IngredientsId = 7 },
                    new { CocktailsId = 4, IngredientsId = 14 },
                    new { CocktailsId = 4, IngredientsId = 15 },

                    new { CocktailsId = 5, IngredientsId = 16 },
                    new { CocktailsId = 5, IngredientsId = 17 },
                    new { CocktailsId = 5, IngredientsId = 18 },
                    new { CocktailsId = 5, IngredientsId = 19 },

                    new { CocktailsId = 6, IngredientsId = 7 },
                    new { CocktailsId = 6, IngredientsId = 20 },
                    new { CocktailsId = 6, IngredientsId = 21 },
                    new { CocktailsId = 6, IngredientsId = 22 },

                    new { CocktailsId = 7, IngredientsId = 7 },
                    new { CocktailsId = 7, IngredientsId = 20 },
                    new { CocktailsId = 7, IngredientsId = 23 },

                    new { CocktailsId = 8, IngredientsId = 7 },
                    new { CocktailsId = 8, IngredientsId = 21 },
                    new { CocktailsId = 8, IngredientsId = 24 },
                    new { CocktailsId = 8, IngredientsId = 4 },

                    new { CocktailsId = 9, IngredientsId = 25 },
                    new { CocktailsId = 9, IngredientsId = 26 },

                    new { CocktailsId = 10, IngredientsId = 9 },
                    new { CocktailsId = 10, IngredientsId = 17 },
                    new { CocktailsId = 10, IngredientsId = 27 },

                    new { CocktailsId = 11, IngredientsId = 28 },
                    new { CocktailsId = 11, IngredientsId = 2 },
                    new { CocktailsId = 11, IngredientsId = 29 },

                    new { CocktailsId = 12, IngredientsId = 25 },
                    new { CocktailsId = 12, IngredientsId = 30 },
                    new { CocktailsId = 12, IngredientsId = 31 },
                    new { CocktailsId = 12, IngredientsId = 32 },
                    new { CocktailsId = 12, IngredientsId = 33 },
                    new { CocktailsId = 12, IngredientsId = 34 },
                    new { CocktailsId = 12, IngredientsId = 35 },
                    new { CocktailsId = 12, IngredientsId = 36 },
                    new { CocktailsId = 12, IngredientsId = 37 },
                    new { CocktailsId = 12, IngredientsId = 38 },
                    new { CocktailsId = 12, IngredientsId = 39 },
                    new { CocktailsId = 12, IngredientsId = 40 },
                    new { CocktailsId = 12, IngredientsId = 19 },
                    new { CocktailsId = 12, IngredientsId = 41 },

                    new { CocktailsId = 13, IngredientsId = 42 },
                    new { CocktailsId = 13, IngredientsId = 41 },
                    new { CocktailsId = 13, IngredientsId = 43 },

                    new { CocktailsId = 14, IngredientsId = 42 },
                    new { CocktailsId = 14, IngredientsId = 41 },
                    new { CocktailsId = 14, IngredientsId = 44 },

                    new { CocktailsId = 15, IngredientsId = 42 },
                    new { CocktailsId = 15, IngredientsId = 18 },
                    new { CocktailsId = 15, IngredientsId = 41 },

                    new { CocktailsId = 16, IngredientsId = 45 },
                    new { CocktailsId = 16, IngredientsId = 46 },
                    new { CocktailsId = 16, IngredientsId = 47 },
                    new { CocktailsId = 16, IngredientsId = 41 },

                    new { CocktailsId = 17, IngredientsId = 25 },
                    new { CocktailsId = 17, IngredientsId = 21 },

                    new { CocktailsId = 18, IngredientsId = 25 },
                    new { CocktailsId = 18, IngredientsId = 48 },
                    new { CocktailsId = 18, IngredientsId = 49 },

                    new { CocktailsId = 19, IngredientsId = 25 },
                    new { CocktailsId = 19, IngredientsId = 50 },
                    new { CocktailsId = 19, IngredientsId = 41 },
                    new { CocktailsId = 19, IngredientsId = 51 },
                    new { CocktailsId = 19, IngredientsId = 52 },

                    new { CocktailsId = 20, IngredientsId = 53 },
                    new { CocktailsId = 20, IngredientsId = 54 },
                    new { CocktailsId = 20, IngredientsId = 55 },

                    new { CocktailsId = 21, IngredientsId = 53 },
                    new { CocktailsId = 21, IngredientsId = 56 },
                    new { CocktailsId = 21, IngredientsId = 29 },

                    new { CocktailsId = 22, IngredientsId = 53 },
                    new { CocktailsId = 22, IngredientsId = 57 },

                    new { CocktailsId = 23, IngredientsId = 58 },
                    new { CocktailsId = 23, IngredientsId = 59 }
                    )
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
