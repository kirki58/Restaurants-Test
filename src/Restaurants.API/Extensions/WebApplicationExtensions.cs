using Restaurants.API.Middlewares;
using Restaurants.Infrastructure.Extensions;
using Serilog;

namespace Restaurants.API.Extensions;

public static class WebApplicationExtensions
{
    public async static Task AddMiddleWare(this WebApplication app){
        app.UseMiddleware<ErrorHandlerMw>();    // It's important that ErrorHandlerMW is the first middleware in the pipeline!

        app.UseMiddleware<RequestTimeoutMw>();

        await app.UseInfrastructureAsync();     // Infrastructure layer WebApplication Extension

        app.UseSerilogRequestLogging();         // Register HTTP Request details to serilog sink(s)

        if(app.Environment.IsDevelopment()){
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();              // Configure the HTTP request pipeline.

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
    }
}
