using Cocktails.API.EndpointHandlers;
using Cocktails.API.Models;

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
                .WithName("GetCocktail")
                .WithOpenApi()
                .WithSummary("Get a cocktail by providing an id.")
                .WithDescription("Cocktails are identified by a URI containing a cocktail identifier. " +
                    "This identifier is an integer. " +
                    "You can get one specific cocktail via this endpoint by providing the identifier."); ;

            cocktailsEndpoints.MapGet("/{cocktailName}", CocktailsHandlers.GetCocktailByNameAsync)
                .AllowAnonymous()
                .WithOpenApi(operation =>
                {
                    operation.Deprecated = true;
                    return operation;
                });

            cocktailsEndpoints.MapPost("", CocktailsHandlers.CreateCocktailAsync)
                .RequireAuthorization("MustBeAtLeast18YearsOldAndAdmin")
                .ProducesValidationProblem(400)
                .Accepts<CocktailForCreationDto>(
                    "application/json",
                    "application/vnd.marvin.cocktailforcreation+json");

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
