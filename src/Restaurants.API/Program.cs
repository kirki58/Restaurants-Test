using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Custom extension method to configure infrastructure layer services.
builder.Services.AddInfraStructure();
builder.Services.AddApplication();

builder.Services.AddControllers();

var app = builder.Build();

// Seed initial data to DB
await app.UseInfrastructureAsync();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
