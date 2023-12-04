using Cocktails.API.DbContexts;
using Cocktails.API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// register the DbContext on the container
// getting the connection string from appSettings
builder.Services.AddDbContext<CocktailsDbContext>(o =>
    o.UseSqlite(builder
    .Configuration["ConnectionStrings:CocktailsDBConnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.RegisterCocktailsEndpoints();
app.RegisterIngredientsEndpoints();

// recreate & migrate the database on each run, for development purposes
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CocktailsDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();
