﻿using AutoMapper;
using Cocktails.API.DbContexts;
using Cocktails.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.API.EndpointHandlers
{
    public static class IngredientsHandlers
    {
        public static async Task<Results<NotFound, Ok<IEnumerable<IngredientDto>>>> GetIngredientsAsync(
            CocktailsDbContext cocktailsDbContext,
            IMapper mapper,
            int cocktailId)
        {
            var cocktailEntity = await cocktailsDbContext.Cocktails
                .FirstOrDefaultAsync(c => c.Id == cocktailId);

            if (cocktailEntity == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(mapper.Map<IEnumerable<IngredientDto>>((
                await cocktailsDbContext.Cocktails
                .Include(c => c.Ingredients)
                .FirstOrDefaultAsync(c => c.Id == cocktailId))?.Ingredients));
        }
    }
}
