using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Aplication layer ServiceCollection Extension
builder.Services.AddApplication();

// Infrastructure layer ServiceCollection Extension
builder.Services.AddInfraStructure();

var app = builder.Build();

// Infrastructure layer WebApplication Extension
await app.UseInfrastructureAsync();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
