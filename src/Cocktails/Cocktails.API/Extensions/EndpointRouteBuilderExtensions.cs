using Cocktails.API.EndpointHandlers;

namespace Cocktails.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegisterCocktailsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var cocktailsEndpoints = endpointRouteBuilder.MapGroup("/cocktails")
                .RequireAuthorization();

            var cocktailWithIntIdEndpoints = cocktailsEndpoints.MapGroup("/{cocktailId:int}")
                .RequireAuthorization("MustBeAtLeast18YearsOldAndAdmin");

            cocktailsEndpoints.MapGet("", CocktailsHandlers.GetCocktailsAsync);

            cocktailWithIntIdEndpoints.MapGet("", CocktailsHandlers.GetCocktailByIdAsync)
                .WithName("GetCocktail");

            cocktailsEndpoints.MapGet("/{cocktailName}", CocktailsHandlers.GetCocktailByNameAsync)
                .AllowAnonymous();

            cocktailsEndpoints.MapPost("", CocktailsHandlers.CreateCocktailAsync)
                .RequireAuthorization("MustBeAtLeast18YearsOldAndAdmin");

            cocktailWithIntIdEndpoints.MapPut("", CocktailsHandlers.UpdateCocktailAsync);

            cocktailWithIntIdEndpoints.MapDelete("", CocktailsHandlers.DeleteCocktailAsync);
        }

        public static void RegisterIngredientsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var ingredientsEndpoints = endpointRouteBuilder.MapGroup("/cocktails/{cocktailId:int}/ingredients")
                .RequireAuthorization();

            ingredientsEndpoints.MapGet("", IngredientsHandlers.GetIngredientsAsync);
        }

    }
}
