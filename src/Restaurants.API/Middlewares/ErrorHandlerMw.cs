    
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares;

public class ErrorHandlerMw(ILogger<ErrorHandlerMw> logger) : IMiddleware
{
    /*
        Error Handler Middleware is the first middleware in the middleware pipeline.
        Since it's the first, every incoming request is wrapped in a try-catch block by it.
        Redirect the request to the next middleware, wait for exceptions and throw if there are any.
    */
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try{
            logger.LogTrace("Error Handler captured the request, handing it over to the rest of the middleware pipeline...");
            await next.Invoke(context);
            logger.LogTrace("Error Handler sucessfully re-obtained the request from the middleware pipeline, no exceptions thrown, Sending client the response...");
        }
        catch(ConflictException ex){
            logger.LogTrace("Error Handler caught an exception through the middleware pipeline. Exception thrown.");
            logger.LogWarning($"[MWWARN] {ex.Message}");

            context.Response.StatusCode = 409;
            await context.Response.WriteAsync("Couldn't create the requested resource.");
        }
        catch(NotFoundException ex){
            logger.LogTrace("Error Handler caught an exception through the middleware pipeline. Exception thrown.");
            logger.LogWarning($"[MWWARN] {ex.Message}");

            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("Requested resource doesn't exist");
        }
        catch(ForbidException ex){
            logger.LogWarning($"[MWWARN] {ex.Message}");

            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Agent Unauthorized to access this endpoint/service.");
        }
        catch(Exception ex){
            // Log error details to the serilog sink
            logger.LogTrace("Error Handler caught an exception through the middleware pipeline. Exception thrown.");
            logger.LogError($"[MWEX] {ex.Message}");

            // Return a broad message in the response body
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected error occurred.");
        }
    }
}
