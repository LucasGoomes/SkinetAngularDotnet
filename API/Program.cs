using Core;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}); // Added DbContext service passing options (connection string)

builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Added repository service - Scoped means one instance per request - live as long as the http request

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

try
{
    // Apply pending migrations and create the database if it does not exist
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();

    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred during migration: {ex.Message}");
}

app.Run();
