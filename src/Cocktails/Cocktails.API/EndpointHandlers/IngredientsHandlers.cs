using AutoMapper;
using Cocktails.API.DbContexts;
using Cocktails.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cocktails.API.EndpointHandlers
{
    public static class IngredientsHandlers
    {
        public static async Task<Results<NotFound, Ok<IEnumerable<IngredientDto>>>> GetIngredientsAsync(
            CocktailsDbContext cocktailsDbContext,
            ClaimsPrincipal claimsPrincipal,
            IMapper mapper,
            ILogger<CocktailDto> logger,
            int cocktailId)
        {
            await Console.Out.WriteLineAsync($"User authenticated? {claimsPrincipal.Identity?.IsAuthenticated}");

            logger.LogInformation("Getting ingredients...");

            var cocktailEntity = await cocktailsDbContext.Cocktails
                .FirstOrDefaultAsync(c => c.Id == cocktailId);

            if (cocktailEntity == null)
            {
                logger.LogInformation(
                    $"Cocktail with id {cocktailId} was not found in the cocktails.");

                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<IEnumerable<IngredientDto>>((
                await cocktailsDbContext.Cocktails
                .Include(c => c.Ingredients)
                .FirstOrDefaultAsync(c => c.Id == cocktailId))?.Ingredients));
        }
    }
}
