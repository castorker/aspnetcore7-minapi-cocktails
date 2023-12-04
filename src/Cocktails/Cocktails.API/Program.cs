using Cocktails.API.DbContexts;
using Cocktails.API.Extensions;
using Cocktails.API.Services;
using Microsoft.AspNetCore.Authorization;
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

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("MustBeAtLeast18YearsOldAndAdmin", policy =>
    policy
        .RequireAuthenticatedUser()
        .RequireRole("admin")
        .Requirements.Add(new MinimumAgeRequirement(18)));

builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

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
