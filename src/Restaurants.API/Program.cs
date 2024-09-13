using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Aplication layer ServiceCollection Extension
builder.Services.AddApplication();

// Infrastructure layer ServiceCollection Extension
builder.Services.AddInfraStructure();

// Serilog configuration done in builder.Host
builder.Host.UseSerilog((context, configuration) => 
    configuration
        .ReadFrom.Configuration(context.Configuration)
        );

var app = builder.Build();

// Infrastructure layer WebApplication Extension
await app.UseInfrastructureAsync();

// Register HTTP Request details to serilog sink(s)
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
