using AutoMapper;
using Cocktails.API.DbContexts;
using Cocktails.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// register the DbContext on the container
// getting the connection string from appSettings
builder.Services.AddDbContext<CocktailsDbContext>(o =>
    o.UseSqlite(builder.Configuration["ConnectionStrings:CocktailsDBConnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/cocktails", async (CocktailsDbContext cocktailsDbContext, 
    IMapper mapper) =>
{
    return mapper.Map<IEnumerable<CocktailDto>>(await cocktailsDbContext.Cocktails.ToListAsync());
});

app.MapGet("/cocktails/{cocktailId:int}", async (CocktailsDbContext cocktailsDbContext, 
    IMapper mapper,
    int cocktailId) =>
{
    return mapper.Map<CocktailDto>(await cocktailsDbContext.Cocktails.FirstOrDefaultAsync(c => c.Id == cocktailId));
});

app.MapGet("/cocktails/{cocktailName}", async (CocktailsDbContext cocktailsDbContext,
    IMapper mapper, 
    string cocktailName) =>
{
    return mapper.Map<CocktailDto>(await cocktailsDbContext.Cocktails.FirstOrDefaultAsync(c => c.Name == cocktailName));
});

app.MapGet("/cocktails/{cocktailId}/ingredients", async (CocktailsDbContext cocktailsDbContext,
    IMapper mapper, 
    int cocktailId) =>
{
    return mapper.Map<IEnumerable<IngredientDto>>((await cocktailsDbContext.Cocktails
        .Include(c => c.Ingredients)
        .FirstOrDefaultAsync(c => c.Id == cocktailId))?.Ingredients);
});

// recreate & migrate the database on each run, for development purposes
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CocktailsDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();
