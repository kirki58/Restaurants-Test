using System;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace Restaurants.API.Middlewares;

public class RequestTimeoutMw(IOptions<MiddlewareSettings> options, ILogger<RequestTimeoutMw> logger) : IMiddleware
{
    private double _timeoutLimit = options.Value.RequestTimeoutLimit;
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        await next.Invoke(context);
        stopwatch.Stop();

        var elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
        if(elapsedSeconds > _timeoutLimit){
            var httpVerb = context.Request.Method;
            var endpointPath = context.Request.Path;

            logger.LogWarning($"[TIMEOUT] REQUEST [{httpVerb}  {endpointPath}] TIMED OUT \n TIMEOUT LIMIT SET: {_timeoutLimit}sec | ELAPSED TIME PROCESSING REQUEST: {elapsedSeconds}sec");
        }
    }
}
