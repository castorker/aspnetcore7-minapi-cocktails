using AutoMapper;
using Cocktails.API.DbContexts;
using Cocktails.API.Entities;
using Cocktails.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.API.EndpointHandlers
{
    public static class CocktailsHandlers
    {
        public static async Task<Ok<IEnumerable<CocktailDto>>> GetCocktailsAsync(
            CocktailsDbContext cocktailsDbContext,
            IMapper mapper,
            ILogger<CocktailDto> logger,
            string? name)
        {
            logger.LogInformation("Getting cocktails...");

            return TypedResults.Ok(mapper.Map<IEnumerable<CocktailDto>>(
                await cocktailsDbContext.Cocktails
                .Where(c => name == null || c.Name.Contains(name))
                .ToListAsync()));
        }

        public static async Task<Results<NotFound, Ok<CocktailDto>>> GetCocktailByIdAsync(
            CocktailsDbContext cocktailsDbContext,
            IMapper mapper,
            ILogger<CocktailDto> logger,
            int cocktailId)
        {
            var cocktailEntity = await cocktailsDbContext.Cocktails
                .FirstOrDefaultAsync(c => c.Id == cocktailId);

            if (cocktailEntity == null)
            {
                logger.LogInformation(
                    $"Cocktail with id {cocktailId} was not found in the cocktails.");

                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<CocktailDto>(cocktailEntity));
        }

        public static async Task<Results<NotFound, Ok<CocktailDto>>> GetCocktailByNameAsync(
            CocktailsDbContext cocktailsDbContext,
            IMapper mapper,
            ILogger<CocktailDto> logger,
            string cocktailName)
        {
            var cocktailEntity = await cocktailsDbContext.Cocktails
                .FirstOrDefaultAsync(c => c.Name == cocktailName);

            if (cocktailEntity == null)
            {
                logger.LogInformation(
                    $"Cocktail with name {cocktailName} was not found in the cocktails.");

                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<CocktailDto>(
                await cocktailsDbContext.Cocktails
                .FirstOrDefaultAsync(c => c.Name == cocktailName)));
        }

        public static async Task<CreatedAtRoute<CocktailDto>> CreateCocktailAsync(
            CocktailsDbContext cocktailsDbContext,
            IMapper mapper,
            CocktailForCreationDto cocktailForCreationDto)
        {
            var cocktailEntity = mapper.Map<Cocktail>(cocktailForCreationDto);

                cocktailsDbContext.Add(cocktailEntity);
            await cocktailsDbContext.SaveChangesAsync();

                var cocktailToReturn = mapper.Map<CocktailDto>(cocktailEntity);

            return TypedResults.CreatedAtRoute(cocktailToReturn,
                "GetCocktail", 
                new { cocktailId = cocktailToReturn.Id
            });
        }

        public static async Task<Results<BadRequest, NotFound, NoContent>> UpdateCocktailAsync(
            CocktailsDbContext cocktailsDbContext,
            IMapper mapper,
            ILogger<CocktailDto> logger,
            int cocktailId,
            CocktailForUpdateDto cocktailForUpdateDto)
        {
            if (cocktailId != cocktailForUpdateDto.Id)
            {
                logger.LogInformation(
                    $"Cocktail Id in the query string {cocktailId} different from the " +
                    $"cocktail Id in the request body {cocktailForUpdateDto.Id}.");

                return TypedResults.BadRequest();
            }

            var cocktailEntity = await cocktailsDbContext.Cocktails
                .FirstOrDefaultAsync(c => c.Id == cocktailId);

            if (cocktailEntity == null)
            {
                logger.LogInformation(
                    $"Cocktail with id {cocktailId} was not found in the cocktails.");

                return TypedResults.NotFound();
            }

            mapper.Map(cocktailForUpdateDto, cocktailEntity);

            await cocktailsDbContext.SaveChangesAsync();

            return TypedResults.NoContent();
        }

        public static async Task<Results<NotFound, NoContent>> DeleteCocktailAsync(
            CocktailsDbContext cocktailsDbContext,
            ILogger<CocktailDto> logger,
            int cocktailId)
        {
            var cocktailEntity = await cocktailsDbContext.Cocktails
                .FirstOrDefaultAsync(c => c.Id == cocktailId);

            if (cocktailEntity == null)
            {
                logger.LogInformation(
                    $"Cocktail with id {cocktailId} was not found in the cocktails.");

                return TypedResults.NotFound();
            }

            cocktailsDbContext.Cocktails.Remove(cocktailEntity);

            await cocktailsDbContext.SaveChangesAsync();

            return TypedResults.NoContent();
        }
    }
}
