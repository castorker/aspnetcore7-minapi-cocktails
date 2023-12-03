using AutoMapper;
using Cocktails.API.DbContexts;
using Cocktails.API.Entities;
using Cocktails.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// register the DbContext on the container
// getting the connection string from appSettings
builder.Services.AddDbContext<CocktailsDbContext>(o =>
    o.UseSqlite(builder
    .Configuration["ConnectionStrings:CocktailsDBConnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/cocktails", 
    async Task<Ok<IEnumerable<CocktailDto>>> 
    (CocktailsDbContext cocktailsDbContext, 
    [FromServices] IMapper mapper,
    [FromQuery] string? name) =>
{
    return TypedResults.Ok(mapper.Map<IEnumerable<CocktailDto>>(
        await cocktailsDbContext.Cocktails
        .Where(c => name == null || c.Name.Contains(name))
        .ToListAsync()));
});

app.MapGet("/cocktails/{cocktailId:int}", 
    async Task<Results<NotFound, Ok<CocktailDto>>> 
    (CocktailsDbContext cocktailsDbContext,
    [FromServices] IMapper mapper,
    [FromRoute] int cocktailId) =>
{
    var cocktailEntity = await cocktailsDbContext.Cocktails
        .FirstOrDefaultAsync(c => c.Id == cocktailId);

    if (cocktailEntity == null)
    {
        return TypedResults.NotFound();
    }

    return TypedResults.Ok(mapper.Map<CocktailDto>(cocktailEntity));
});

app.MapGet("/cocktails/{cocktailName}", 
    async Task<Results<NotFound, Ok<CocktailDto>>>
    (CocktailsDbContext cocktailsDbContext,
    [FromServices] IMapper mapper,
    [FromRoute] string cocktailName) =>
{
    var cocktailEntity = await cocktailsDbContext.Cocktails
        .FirstOrDefaultAsync(c => c.Name == cocktailName);

    if (cocktailEntity == null)
    {
        return TypedResults.NotFound();
    }

    return TypedResults.Ok(mapper.Map<CocktailDto>(
        await cocktailsDbContext.Cocktails
        .FirstOrDefaultAsync(c => c.Name == cocktailName)));
});

app.MapGet("/cocktails/{cocktailId}/ingredients", 
    async Task<Results<NotFound, Ok<IEnumerable<IngredientDto>>>>
    (CocktailsDbContext cocktailsDbContext,
    [FromServices] IMapper mapper,
    [FromRoute] int cocktailId) =>
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
});

// recreate & migrate the database on each run, for development purposes
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CocktailsDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();
