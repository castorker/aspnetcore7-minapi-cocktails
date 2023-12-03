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

var cocktailsEndpoints = app.MapGroup("/cocktails");
var cocktailWithIntIdEndpoints = cocktailsEndpoints.MapGroup("/{cocktailId:int}");
var ingredientsEndpoints = cocktailWithIntIdEndpoints.MapGroup("/ingredients");

cocktailsEndpoints.MapGet("", 
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

cocktailWithIntIdEndpoints.MapGet("", 
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
}).WithName("GetCocktail");

cocktailsEndpoints.MapGet("/{cocktailName}", 
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

ingredientsEndpoints.MapGet("", 
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

cocktailsEndpoints.MapPost("", 
    async Task<CreatedAtRoute<CocktailDto>> 
    (CocktailsDbContext cocktailsDbContext, 
    IMapper mapper,
    [FromBody] CocktailForCreationDto cocktailForCreationDto
    ) =>
{
    var cocktailEntity = mapper.Map<Cocktail>(cocktailForCreationDto);

    cocktailsDbContext.Add(cocktailEntity);
    await cocktailsDbContext.SaveChangesAsync();

    var cocktailToReturn = mapper.Map<CocktailDto>(cocktailEntity);

    return TypedResults.CreatedAtRoute(cocktailToReturn, 
        "GetCocktail", 
        new { cocktailId = cocktailToReturn.Id });
});

cocktailWithIntIdEndpoints.MapPut("", 
    async Task<Results<BadRequest, NotFound, NoContent>>
    (CocktailsDbContext cocktailsDbContext, 
    IMapper mapper,
    int cocktailId,
    CocktailForUpdateDto cocktailForUpdateDto
    ) =>
{
    if (cocktailId != cocktailForUpdateDto.Id)
    {
        return TypedResults.BadRequest();
    }

    var cocktailEntity = await cocktailsDbContext.Cocktails
        .FirstOrDefaultAsync(c => c.Id == cocktailId);

    if (cocktailEntity == null)
    {
        return TypedResults.NotFound();
    }

    mapper.Map(cocktailForUpdateDto, cocktailEntity);

    await cocktailsDbContext.SaveChangesAsync();

    return TypedResults.NoContent();
});

cocktailWithIntIdEndpoints.MapDelete("", 
    async Task<Results<NotFound, NoContent>> 
    (CocktailsDbContext cocktailsDbContext, 
    int cocktailId) =>
{
    var cocktailEntity = await cocktailsDbContext.Cocktails
        .FirstOrDefaultAsync(c => c.Id == cocktailId);

    if (cocktailEntity == null)
    {
        return TypedResults.NotFound();
    }

    cocktailsDbContext.Cocktails.Remove(cocktailEntity);

    await cocktailsDbContext.SaveChangesAsync();

    return TypedResults.NoContent();
});

// recreate & migrate the database on each run, for development purposes
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CocktailsDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();
