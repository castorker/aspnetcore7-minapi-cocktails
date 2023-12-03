using Cocktails.API.EndpointHandlers;

namespace Cocktails.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterCocktailsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var cocktailsEndpoints = endpointRouteBuilder.MapGroup("/cocktails");

            var cocktailWithIntIdEndpoints = cocktailsEndpoints.MapGroup("/{cocktailId:int}");

            cocktailsEndpoints.MapGet("", CocktailsHandlers.GetCocktailsAsync);

            cocktailWithIntIdEndpoints.MapGet("", CocktailsHandlers.GetCocktailByIdAsync)
                .WithName("GetCocktail");

            cocktailsEndpoints.MapGet("/{cocktailName}", CocktailsHandlers.GetCocktailByNameAsync);

            cocktailsEndpoints.MapPost("", CocktailsHandlers.CreateCocktailAsync);

            cocktailWithIntIdEndpoints.MapPut("", CocktailsHandlers.UpdateCocktailAsync);

            cocktailWithIntIdEndpoints.MapDelete("", CocktailsHandlers.DeleteCocktailAsync);
        }

        public static void RegisterIngredientsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var ingredientsEndpoints = endpointRouteBuilder.MapGroup("/cocktails/{cocktailId:int}/ingredients");

            ingredientsEndpoints.MapGet("", IngredientsHandlers.GetIngredientsAsync);
        }

    }
}
