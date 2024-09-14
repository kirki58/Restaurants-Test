using Restaurants.Infrastructure.Extensions;
using Restaurants.Application.Extensions;
using Serilog;
using Serilog.Formatting.Compact;
using Restaurants.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Integrate custom settings in appsettings.json to Middlewares/MiddlewareSettings

builder.Services.Configure<MiddlewareSettings>(builder.Configuration.GetSection("MiddlewareSettings"));

// Add services to the container.

builder.Services.AddScoped<ErrorHandlerMw>(); // Register error handling middleware as a dependancy

builder.Services.AddScoped<RequestTimeoutMw>(); // Register request timeout catcher middleware as a dependancy

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();      // Aplication layer ServiceCollection Extension

builder.Services.AddInfraStructure();   // Infrastructure layer ServiceCollection Extension

// Serilog configuration done in builder.Host
builder.Host.UseSerilog((context, configuration) =>  
    configuration
        .ReadFrom.Configuration(context.Configuration)
        );

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMw>();    // It's important that ErrorHandlerMW is the first middleware in the pipeline!

app.UseMiddleware<RequestTimeoutMw>();

await app.UseInfrastructureAsync();     // Infrastructure layer WebApplication Extension

app.UseSerilogRequestLogging();         // Register HTTP Request details to serilog sink(s)

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();              // Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
